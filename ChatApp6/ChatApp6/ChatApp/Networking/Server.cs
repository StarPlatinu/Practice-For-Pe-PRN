using ChatClient.Networking.IO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient.Networking
{
    class Server
    {
        TcpClient _client;
        public PacketReader packetReader;
        public event Action connectedEvent;
        public Server()
        {
            _client = new TcpClient();
        }
        public void connectToServer(string username)
        {
            if (!_client.Connected)
            {
                _client.Connect("127.0.0.1", 7777);
                packetReader = new PacketReader(_client.GetStream());
                if (!string.IsNullOrEmpty(username))
                {
                    var _packetWriter = new PacketWriter();
                    _packetWriter.writeOpCode(0);
                    _packetWriter.writeMessage(username);
                    _client.Client.Send(_packetWriter.getPacketBytes());
                }
                ReadPackets();
            }
        }
        private void ReadPackets()
        {
            Task.Run(() =>
            {
                while (true) 
                { 
                    var opcode = packetReader.ReadByte();
                    switch(opcode) 
                    {
                        case 1:
                            connectedEvent?.Invoke();
                            break;
                        default:
                            Console.WriteLine("FPT Hola");
                            break;
                    }
                }
            });
        }
    }
}
