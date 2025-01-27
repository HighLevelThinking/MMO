using System;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class TCPclient : MonoBehaviour
{
    private TcpClient client;
    private NetworkStream stream;

    public string serverIP = "127.0.0.1"; // Change this to the server's IP if needed
    public int port = 5000;

    void Start()
    {
        try
        {
            client = new TcpClient(serverIP, port);
            stream = client.GetStream();
            Debug.Log("Connected to server.");
            ReceiveMessage();
        }
        catch (Exception e)
        {
            Debug.LogError($"Error connecting to server: {e.Message}");
        }
    }

    private void ReceiveMessage()
    {
        byte[] buffer = new byte[1024];
        int bytesRead = stream.Read(buffer, 0, buffer.Length);
        string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
        Debug.Log($"Message from server: {message}");

        // Clean up
        stream.Close();
        client.Close();
    }
}
