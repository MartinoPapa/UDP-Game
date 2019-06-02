using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Client
{
    private UdpClient udpServer;
    private IPEndPoint sender;
    private byte[] data = new byte[1024];

    public Client(string serverIp, int serverPort)
    {
        udpServer = new UdpClient(serverIp, serverPort);
        sender = new IPEndPoint(IPAddress.Any, 0);
    }

    public string GetServerAddress()
    {
        return sender.ToString();
    }

    public void SendMessage(string message)
    {
        data = Encoding.ASCII.GetBytes(message);
        udpServer.Send(data, data.Length);
    }

    public string Listen()
    {
        data = udpServer.Receive(ref sender);
        return Encoding.ASCII.GetString(data, 0, data.Length);
    }

    public void Close()
    {
        udpServer.Close();
    }
}
