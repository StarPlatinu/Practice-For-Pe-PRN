using ChatServer.Networking.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Networking
{
    class Client
    {
        public string username { get; set; }
        public Guid UID { get; set; }
        public TcpClient _clientSocket { get; set; }
        PacketReader _packetReader;
        public Client(TcpClient clientSocket)
        {
            _clientSocket = clientSocket;
            UID = Guid.NewGuid();
            _packetReader = new PacketReader(_clientSocket.GetStream());
            var opcode = _packetReader.ReadByte();
            username= _packetReader.readMessage();

            Console.WriteLine($"{DateTime.Now}: Client has connected with user name: {username} ");
            Task.Run( () => Process());
        }
        public void Process() 
        { 
            while(true)
            {
                try
                {
                    var opcode = _packetReader.ReadByte();
                    switch (opcode)
                    {
                        case 2:
                            var msg = _packetReader.readMessage();
                            Console.WriteLine($"{DateTime.Now} message received: {msg}");
                            Program.broadCastMessage($"[{DateTime.Now}] : [{username}] : {msg}");
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}
