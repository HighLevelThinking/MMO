using UnityEngine;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using Newtonsoft.Json;

public class ClientScript : MonoBehaviour
{
    public string serverIP = "127.0.0.1"; // Set this to your server's IP address.
    public int serverPort = 1948;             // Set this to your server's port.

    private TcpClient client;
    private NetworkStream stream;
    private Thread clientReceiveThread;

    [SerializeField] GameObject player;

    void Start()
    {
        ConnectToServer();
    }

    private void Update()
    {
        List<string> keypresses = new List<string> { };
        foreach (char c in Input.inputString)
        {
            keypresses.Add(c.ToString());
        }
        List<int> mouseclicks = new List<int> { };
        for (int i = 0; i < 5; i++)
        {
            if (Input.GetMouseButton(i)) mouseclicks.Add(i);
        }
        InputData inputs = new InputData { Keypresses = keypresses, MouseClicks = mouseclicks, };
        Message message = new Message {Type = "input", Data = inputs};
        SendMessageToServer(JsonConvert.SerializeObject(message));
        ListenForData();
    }

    void ConnectToServer()
    {
        try
        {
            client = new TcpClient(serverIP, serverPort);
            stream = client.GetStream();
            Debug.Log("Connected to server.");

            clientReceiveThread = new Thread(new ThreadStart(ListenForData));
            clientReceiveThread.IsBackground = true;
            clientReceiveThread.Start();
        }
        catch (SocketException e)
        {
            Debug.LogError("SocketException: " + e.ToString());
        }
    }

    private void ListenForData()
    {
        try
        {
            byte[] bytes = new byte[1024];
            while (true)
            {
                // Check if there's any data available on the network stream
                if (stream.DataAvailable)
                {
                    int length;
                    // Read incoming stream into byte array.
                    while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        var incomingData = new byte[length];
                        Array.Copy(bytes, 0, incomingData, 0, length);
                        // Convert byte array to string message.
                        string serverMessage = Encoding.UTF8.GetString(incomingData);
                        Message data = Deserializer.DeserializeInput(serverMessage);
                    }
                }
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("Socket exception: " + socketException);
        }
    }

    public void SendMessageToServer(string message)
    {
        if (client == null || !client.Connected)
        {
            Debug.LogError("Client not connected to server.");
            return;
        }

        byte[] data = Encoding.UTF8.GetBytes(message);
        stream.Write(data, 0, data.Length);
        Debug.Log("Sent message to server: " + message);
    }

    void OnApplicationQuit()
    {
        if (stream != null)
            stream.Close();
        if (client != null)
            client.Close();
        if (clientReceiveThread != null)
            clientReceiveThread.Abort();
    }
}
