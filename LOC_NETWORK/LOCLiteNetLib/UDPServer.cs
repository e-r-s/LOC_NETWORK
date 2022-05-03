using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Numerics;
using System.Threading;
using LiteNetLib;
using LiteNetLib.Utils;
using LOC_NETWORK.GameItems;
using LOC_NETWORK.GameItems.Creature;
using LOC_NETWORK.NetworkCommands;
using LOC_NETWORK.Networking;

namespace LOC_NETWORK.LOCLiteNetLib
{
    public class UDPServer
    {

        private ConnectedUserCollection _AllClients = new ConnectedUserCollection();

        EventBasedNetListener listener = new EventBasedNetListener();
        NetManager server = null; // new NetManager(listener);
        public bool IsRunning = false;

        public void Init(string address, int port)
        {
            listener.ConnectionRequestEvent += Listener_ConnectionRequestEvent;
            listener.PeerConnectedEvent += Listener_PeerConnectedEvent;
            listener.NetworkReceiveEvent += Listener_NetworkReceiveEvent;
            listener.NetworkErrorEvent += Listener_NetworkErrorEvent;


            server = new NetManager(listener);
            server.Start(port);
            Console.WriteLine("UDP SERVER INITIATED AT " + port.ToString());

            IsRunning = true;
        }

        private void Listener_NetworkErrorEvent(IPEndPoint endPoint, SocketError socketError)
        {
            throw new NotImplementedException();
        }

        private void Listener_NetworkReceiveEvent(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
        {
            LOC_SHARED.Util.Logger.Log("RECEIVED DATA:");
            LOC_SHARED.Util.Logger.Log(reader.RawData, 0);

            ConnectedUser uc = _AllClients.Find(peer.EndPoint);

            if (uc != null)
            { 
                uc.DataReceived(reader.RawData, reader.UserDataOffset, reader.UserDataSize);
            }
           

        }

        private void Listener_PeerConnectedEvent(NetPeer peer)
        {
            Console.WriteLine("We got connection: {0}", peer.EndPoint); // Show peer ip
            NetDataWriter writer = new NetDataWriter();                 // Create writer class
            writer.Put(1234);                                // Put some string
            peer.Send(writer, DeliveryMethod.ReliableOrdered);             // Send with reliability

            ConnectedUser uc = _AllClients.Find(peer.EndPoint);
            if (uc == null)
            {
                //TODO USER ID NOW IS 0
                Player p = GameManager.PlayerConnected(peer, 1);
                uc = _AllClients.AddNew(1, peer.EndPoint,peer, p);
                uc.peer = peer; 
            }
            else
            {
                uc.peer = peer;
            }

        }

        private void Listener_ConnectionRequestEvent(ConnectionRequest request)
        {
            if (server.ConnectedPeersCount < 10000 /* max connections */)
                request.AcceptIfKey("SomeConnectionKey");
            else
                request.Reject();
        }


        public bool HasUser()
        {
            return _AllClients.Count() > 0;
        }



        public string Name = "";



        public void SendToClients(List<ConnectedUser> users, byte[] data)
        {
            for(int i=0; i< users.Count; i++)
            {
                SendToClient(data, users[i]);
            } 
        }



        public void SendToClient(byte[] data, int id)
        {
            SendToClient(data, _AllClients.Find(id));
        }

        public void SendToClient(byte[] data, ConnectedUser receiver)
        {
             
            receiver.peer.Send(data, DeliveryMethod.ReliableOrdered);
            receiver.LatestSentData = DateTime.Now; 
        }


        public void PollEvents()
        {
            if (IsRunning)
            {
                server.PollEvents();
            }
        }

    }
}
