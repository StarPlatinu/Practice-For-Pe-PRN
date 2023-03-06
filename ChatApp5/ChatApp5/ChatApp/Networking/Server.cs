﻿using ChatClient.Networking.IO;
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
        public void connectToServer(string username) {
            if (!_client.Connected)
            {
                _client.Connect("127.0.0.1",7777);
            }
            var _packetWriter = new PacketWriter();
            _packetWriter.writeOpCode(0);
            _packetWriter.writeMessage(username);
            _client.Client.Send(_packetWriter.getPacketBytes());
        }
    }
}
