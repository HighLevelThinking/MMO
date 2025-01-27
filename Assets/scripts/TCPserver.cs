using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class TCPserver
{
    private TcpListener server;

    public TCPserver(int port)
    {
        server = new TcpListener(IPAddress.Any, port);
    }

    public void Start()
    {
        server.Start();
        Console.WriteLine("Server started...");

        while (true)
        {
            TcpClient client = server.AcceptTcpClient();
            Console.WriteLine("Client connected.");
            Thread clientThread = new Thread(() => HandleClient(client));
            clientThread.Start();
        }
    }

    private void HandleClient(TcpClient client)
    {
        NetworkStream stream = client.GetStream();
        byte[] message = Encoding.UTF8.GetBytes("Hello from server!");
        stream.Write(message, 0, message.Length);
        Console.WriteLine("Message sent to client.");

        client.Close();
    }
}

// To run the server
public class Program
{
    public static void Main(string[] args)
    {
        TCPserver server = new TCPserver(5000);
        server.Start();
    }
}
