using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDP
{
    public class Server
    {
        private byte[] data = new byte[1024];
        private IPEndPoint ipep;
        private UdpClient server;
        private IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);

        public Server(int port)
        {
            ipep = new IPEndPoint(IPAddress.Any, port);
            server = new UdpClient(ipep);
        }

        public string Listen(out string clientAddress)
        {
            data = server.Receive(ref sender);
            clientAddress = sender.ToString();
            return Encoding.ASCII.GetString(data, 0, data.Length);
        }

        public void SendMessage(string message)
        {
            data = Encoding.ASCII.GetBytes(message);
            server.Send(data, data.Length, sender);
        }

        public void Close()
        {
            server.Close();
        }

        static public bool NewPlayer(string message, out string nome)
        {
            bool isNew = false;
            nome = "";
            if (message.Contains("nome"))
            {
                nome = message.Split(':')[1];
                isNew = true;
            }
            return isNew;
        }
    }

}


