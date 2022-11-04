using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient.Net.IO
{
    
    class PacketBuilder
    {
        MemoryStream _ms;
        public PacketBuilder()
        {
            _ms = new MemoryStream();
        }
        public void WriteOpCode(byte opcode)
        {
            _ms.WriteByte(opcode);
        }
        public void WriteString(string message)
        {
            var messageLength = message.Length;
            _ms.Write(BitConverter.GetBytes(messageLength));
            _ms.Write(Encoding.ASCII.GetBytes(message));
        }
        public byte[] GetPacketBytes()
        {
            return _ms.ToArray();
        }
    }
}
