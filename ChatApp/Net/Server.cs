﻿using ChatClient.Net.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient.Net
{
    class Server
    {
        TcpClient _client; //
        PacketBuilder _packetBuilder;
        public Server() //Instantiete new instance to TCP Client
        {
            _client = new TcpClient();
        }
        public void ConnectToServer(string username) //Connecting client to server, public because its going to be exposed to the ViewModel
        {
            if (!_client.Connected)
            {
                _client.Connect("192.168.0.12", 7891);
                var connectPacket = new PacketBuilder();
                connectPacket.WriteOpCode(0);
                connectPacket.WriteString(username);
                _client.Client.Send(connectPacket.GetPacketBytes());
            }
        }
    }
}
