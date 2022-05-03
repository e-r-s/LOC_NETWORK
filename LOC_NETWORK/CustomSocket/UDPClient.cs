using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using LOC_NETWORK.NetworkCommands;
using LOC_NETWORK.Networking;
using LOC_SHARED.NetworkItems;

namespace LOC_NETWORK.CustomSocket
{
    public class UDPClient
    {
        public string Name = "";
        private Socket _Socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        private const int bufSize = 8 * 1024;
        private State _State = new State();


        private EndPoint _EpFrom = new IPEndPoint(IPAddress.Any, 0); 

        private ConnectedServer _ServerConnection = new ConnectedServer();
        private IPEndPoint _SelfEndPoint = null;

        private bool _IsConnected = false;
        public bool IsConnected { get { return _IsConnected; } }

        public int TotalPacketsReceived = 0;


        private AsyncCallback recv = null;

        public class State
        {
            public byte[] buffer = new byte[bufSize];
        }

        public void Init(string serverAddress, int serverPort, string clientAddress, int clientPort)
        {
            _ServerConnection.Connection = new IPEndPoint(IPAddress.Parse(serverAddress), serverPort);
            _ServerConnection.Address = serverAddress + ":"+ serverPort;
            _SelfEndPoint = new IPEndPoint(IPAddress.Any, clientPort);
             _Socket.Bind(_SelfEndPoint); 
            Console.WriteLine("UDP CLIENT INITIATED AT : "+ _Socket.LocalEndPoint.ToString());
            Console.WriteLine("UDP CLIENT CONNECTING TO : "+ _ServerConnection.Connection.ToString());

            Receive();
        }

      
        public void SendToServer(byte[] data)
        {

            _Socket.BeginSendTo(data, 0, data.Length, SocketFlags.None, _ServerConnection.Connection,
                (ar) =>
                {
                    State so = (State)ar.AsyncState;
                    int bytes = _Socket.EndSendTo(ar);
                    _ServerConnection.LatestSentData = DateTime.Now;
                  //  Console.WriteLine("SendToServer "+ _ServerConnection.Connection.ToString() );
                }, _State);
        }

        private void Receive()
        {
            Console.WriteLine("UDP CLIENT IS LISTENING");


            _Socket.BeginReceiveFrom(_State.buffer, 0, bufSize, SocketFlags.None, ref _EpFrom, recv = (ar) =>
            {
                State so = (State)ar.AsyncState;
                int bytes = _Socket.EndReceiveFrom(ar, ref _EpFrom);
                _Socket.BeginReceiveFrom(so.buffer, 0, bufSize, SocketFlags.None, ref _EpFrom, recv, so);

                TotalPacketsReceived++;

                if (_ServerConnection.Address == _EpFrom.ToString())
                {
                    _ServerConnection.DataReceived(so.buffer);
                }
                 
                if (so.buffer[0] == NetworkCommandType.ConnectionConfirmation)
                {
                    _IsConnected = true;
                }


            }, _State);
        }



       
    }
}
