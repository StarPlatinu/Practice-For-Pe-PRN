﻿using ChatServer.Networking;
using System.Net;
using System.Net.Sockets;
namespace ChatServer
{
    class Program
    {
        static List<Client> _users;
        static TcpListener _listener;
        static void Main(string[] args)
        {
            _users= new List<Client>();
            _listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 7777);
            _listener.Start();
            var client = new Client(_listener.AcceptTcpClient());
        }
    }
}