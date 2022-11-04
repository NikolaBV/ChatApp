using System;
using System.Net;
using System.Net.Sockets;

namespace ChatServer
{
    class Program
    {
        static List<Client> _users;
        static TcpListener _listner;
        static void Main(string[] args)
        {
            _users = new List<Client>();
            _listner = new TcpListener(IPAddress.Parse("192.168.0.12"), 7891);
            _listner.Start();

            while (true)
            {
                var client = new Client(_listner.AcceptTcpClient());
                _users.Add(client);
                /* Broadcast the connection to everyone on the server */
            }

        }
    }
}