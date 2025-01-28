using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

TcpListener server = null;
TcpClient client = null;
NetworkStream stream = null;
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
            client = server.AcceptTcpClient();
            Console.WriteLine("Connected!");

            message = null;
            stream = client.GetStream();

            int i;

            while ((i = stream.Read(buffer, 0, buffer.Length)) != 0)
            {
                message = Encoding.UTF8.GetString(buffer, 0, i);
                Console.WriteLine("Received: " + message);

                //classify
                var data = DeserializeInput(message);

                Console.WriteLine(data);

                string response = "Server response: " + message.ToString();
                SendMessageToClient(message: response);
            }
            client.Close();
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

void OnApplicationQuit()
{
    stream.Close();
    client.Close();
    server.Stop();
    thread.Abort();
}

void SendMessageToClient(string message)
{
    byte[] msg = Encoding.UTF8.GetBytes(message);
    stream.Write(msg, 0, msg.Length);
    Console.WriteLine("Message Sent");
}

static Message DeserializeInput(string jsonString)
{
    var temp = JObject.Parse(jsonString);
    var type = temp["type"].ToString();

    Message input = new Message
    {
        Type = type,
        Data = type switch
        {
            "input" => temp["data"].ToObject<InputData>(),
            "player" => temp["data"].ToObject<PlayerData>(),
            _ => null
        }
    };

    return input;
}

public interface MessageData
{
    // You can define common methods or properties if needed
}


public class InputData : MessageData
{
    public List<string>? Keypresses { get; set; }
    public List<int>? MouseClicks { get; set; }
}

public class PlayerData : MessageData
{
    public string? Name { get; set; }
    public int? Hp { get; set; }
    public int? ColorId { get; set; } //change eventually
}

public class Message
{
    public string? Type { get; set; }
    public MessageData? Data { get; set; }
}
