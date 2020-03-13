﻿using System;
using KaymakNetwork.Network;
namespace UnityServer
{
    internal static class NetworkConfig
    {
        private static Server _socket;

        internal static Server socket
        {
            get { return _socket; }
            set
            {
                if(_socket!=null)
                {
                    _socket.ConnectionReceived -= Socket_ConnectionReceived;
                    _socket.ConnectionLost -= Socket_ConnectionLost;
                }
                _socket = value;
                if(_socket!= null)
                {
                    _socket.ConnectionReceived += Socket_ConnectionReceived;
                    _socket.ConnectionLost += Socket_ConnectionLost;
                }

            }
        }


        internal static void InitNetwork()
        {
            if (socket != null) return;
            socket = new Server(100)
            {
                BufferLimit = 2048000,
                PacketAcceptLimit = 100,
                PacketDisconnectCount = 150,
            };
            NetworkReceive.PacketRouter();

        }

        internal static void Socket_ConnectionReceived(int connectionID)
        {
            Console.WriteLine("Connection received on index[" + connectionID + "]");
            NetworkSend.WelcomeMsg(connectionID, "Welcome to server!");
            //NetworkSend.InstantiateNetworkPlayer(connectionID);
        }
        internal static void Socket_ConnectionLost(int connectionID)
        {
            Console.WriteLine("Connection lost on index[" + connectionID + "]");
        }
    }
}
