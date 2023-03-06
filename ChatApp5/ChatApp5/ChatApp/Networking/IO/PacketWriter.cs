using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient.Networking.IO
{
    class PacketWriter
    {
        MemoryStream _ms;
        public PacketWriter() 
        { 
            _ms= new MemoryStream();
        }
        public void writeOpCode(byte opcode)  //ReadByte
        { 
            _ms.WriteByte(opcode); 
        }
        public void writeMessage(string msg) //ReadMessage
        {
            var msgLength = msg.Length;
            _ms.Write(BitConverter.GetBytes(msgLength));
            _ms.Write(Encoding.ASCII.GetBytes(msg));
        }
        public byte[] getPacketBytes() 
        { 
            return _ms.ToArray();
        }
    }
}
