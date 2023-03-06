using ChatServer.Networking;
using ChatServer.Networking.IO;
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
            while (true)
            {
                var client = new Client(_listener.AcceptTcpClient());
                _users.Add(client);
                broadCastConnection();
            }
            
        }
        public static void broadCastConnection()
        {
            foreach (var user in _users)
            {
                foreach (var usr in _users)
                {
                    var broadPacket = new PacketWriter();
                    broadPacket.writeOpCode(1);
                    broadPacket.writeMessage(usr.username);
                    broadPacket.writeMessage(usr.UID.ToString());
                    user._clientSocket.Client.Send(broadPacket.getPacketBytes());
                }
            }

        }
        public static void broadCastMessage(string message)
        {
            foreach (var user in _users)
            {
                var messsagePacket = new PacketWriter();
                messsagePacket.writeOpCode(2);
                messsagePacket.writeMessage(message);
                user._clientSocket.Client.Send(messsagePacket.getPacketBytes());
            }
        }
    }
}