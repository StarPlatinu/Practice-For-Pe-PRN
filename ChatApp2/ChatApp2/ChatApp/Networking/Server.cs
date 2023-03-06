using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient.Networking
{
    class Server
    {
        TcpClient _client;
        public Server() { 
            _client= new TcpClient();
        }
        public void connectToServer() {
            if (!_client.Connected)
            {
                _client.Connect("127.0.0.1",7777);
            }
        }
    }
}
