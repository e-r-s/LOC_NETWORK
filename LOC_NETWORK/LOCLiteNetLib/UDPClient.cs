using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using LiteNetLib;
using LiteNetLib.Utils;
using LOC_NETWORK.NetworkCommands;
using LOC_NETWORK.Networking;

namespace LOC_NETWORK.LOCLiteNetLib
{
    public class UDPClient
    {


        EventBasedNetListener listener = new EventBasedNetListener();
        NetManager client = null;

      
        public string Name = "";
       

        private ConnectedServer _ServerConnection = new ConnectedServer();
        private IPEndPoint _SelfEndPoint = null;

        private bool _IsConnected = false;
        public bool IsConnected { get { return _IsConnected; } }

        public int TotalPacketsReceived = 0;

         
        public void Init(string serverAddress, int serverPort, string clientAddress, int clientPort)
        {

            listener.NetworkReceiveEvent += Listener_NetworkReceiveEvent;
            listener.NetworkErrorEvent += Listener_NetworkErrorEvent;

            client = new NetManager(listener);
            client.Start();

            client.Connect(serverAddress, serverPort, "SomeConnectionKey");

            _ServerConnection.Connection = new IPEndPoint(IPAddress.Parse(serverAddress), serverPort);
            _ServerConnection.Address = serverAddress + ":" + serverPort;
            _SelfEndPoint = new IPEndPoint(IPAddress.Any, clientPort);
             
            Console.WriteLine("UDP CLIENT INITIATED AT : " + _SelfEndPoint.ToString());
            Console.WriteLine("UDP CLIENT CONNECTING TO : " + _ServerConnection.Connection.ToString());
            
            
        }

        private void Listener_NetworkErrorEvent(IPEndPoint endPoint, SocketError socketError)
        {
            throw new NotImplementedException();
        }

        int counterInCase = 0;
        private void Listener_NetworkReceiveEvent(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
        {

            TotalPacketsReceived++;

            if (_ServerConnection.Address == peer.EndPoint.ToString())
            {
                _ServerConnection.DataReceived(reader.RawData);
                _ServerConnection.peer = peer;
                _IsConnected = true;


                int dataint = 0;
                counterInCase++;


                if (reader.TryGetInt(out dataint))
                {
                    if (dataint > 99990)
                    {
                        Console.WriteLine(dataint);
                    }
                }
                else
                {
                    if (counterInCase > 100000)
                    {
                        Console.WriteLine("100000 data sent");
                    }
                }

            }
             
            reader.Recycle();
        }

        public void SendToServer(byte[] data)
        {
            if (!_IsConnected)
            {
                return;
            }
            NetDataWriter writer = new NetDataWriter();
            writer.Put(data);
            _ServerConnection.peer.Send(writer, DeliveryMethod.ReliableSequenced);
            _ServerConnection.LatestSentData = DateTime.Now;

        }


        public void ReConnect( )
        {
            if (IsConnected)
            {
                return;
            }
            client.Connect(_ServerConnection.Connection.Address.ToString(), _ServerConnection.Connection.Port, "SomeConnectionKey");
        }

        public void PollEvents()
        {
            client.PollEvents();
        }

    }
}
