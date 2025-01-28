using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

TcpListener server = null;
TcpClient[] clients;
Dictionary<TcpClient, int> playerIdByClient;
Dictionary<int, int[]> playerPosById;
Dictionary<TcpClient, NetworkStream> clientStream;
Thread thread;

thread = new Thread(new ThreadStart(SetupServer));
thread.Start();

void SetupServer()
{
    try
    {
        IPAddress localAddr = IPAddress.Parse("127.0.0.1");
        server = new TcpListener(localAddr, 8080);

        if (server == null)
        {
            Console.WriteLine("Exception: Serve Didn't Start");
            return;
        }

        server.Start();

        byte[] buffer = new byte[1024];
        string message = null;

        while (true)
        {
            Console.WriteLine("Waiting for connection...");
            TcpClient tempClient = server.AcceptTcpClient();
            Console.WriteLine("Connected!");

            message = null;
            clientStream[tempClient] = tempClient.GetStream();

            int i;

            foreach (TcpClient client in clients)
            {
                while ((i = clientStream[client].Read(buffer, 0, buffer.Length)) != 0)
                {
                    message = Encoding.UTF8.GetString(buffer, 0, i);
                    Console.WriteLine("Received: " + message);

                    //classify
                    var data = DeserializeInput(message);

                    Console.WriteLine(data);


                }
            }
        }
    }
    catch (SocketException e)
    {
        Console.WriteLine("SocketException: " + e);
    }
    finally
    {
        server.Stop();
    }
}



void SendMessageToClient(string message, NetworkStream stream)
{
    byte[] msg = Encoding.UTF8.GetBytes(message);
    stream.Write(msg, 0, msg.Length);
    Console.WriteLine("Message Sent");
}

void updatePlayers(TcpClient client)
{
    Message response = new Message
    {
        Type = "player",
        Data = new PlayerData
        {
            Name = "Player " + playerIdByClient[client].ToString(),
            Pos = playerPosById[playerIdByClient[client]],
            Id = playerIdByClient[client]
        }
    };

    SendMessageToClient(JsonConvert.SerializeObject(response), clientStream[client]);
}
