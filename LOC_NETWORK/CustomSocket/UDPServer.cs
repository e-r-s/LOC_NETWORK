using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using LOC_NETWORK.NetworkCommands;
using LOC_NETWORK.Networking;
using LOC_SHARED.NetworkItems;

namespace LOC_NETWORK.CustomSocket
{
    public class UDPServer
    {
        public string Name = "";
        private Socket _Socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        private const int bufSize = 8 * 1024;
        private State _State = new State();


        private EndPoint _EpFrom = new IPEndPoint(IPAddress.Any, 0);
        //private UserClient[] _Clients = new UserClient[1024];
        //private int _ClientsCount = 0;


        private ConnectedUserCollection _AllClients = new ConnectedUserCollection();


        private IPEndPoint _ServerEndPoint = null;
        private AsyncCallback recv = null;

        public class State
        {
            public byte[] buffer = new byte[bufSize];
        }


        //public UserClient GetClient(int id)
        //{
        //    for (int i = 0; i < _Clients.Length; i++)
        //    {
        //        if (_Clients[i] == null)
        //        {
        //            continue;
        //        }
        //        if (_Clients[i].Id == id)
        //        {
        //            return _Clients[i];
        //        }
        //    }

        //    return null;
        //}

        //public UserClient AddNewClient(int id, IPEndPoint ip)
        //{
        //    UserClient uc = new UserClient();
        //    uc.Address = ip.ToString();
        //    uc.Connection = ip;
        //    uc.Id = id;
        //    _Clients[_ClientsCount] = uc;
        //    _ClientsCount++;
        //    return uc;

        //}

        public void Init(string address, int port)
        {
            _Socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.ReuseAddress, true);
           /// _ServerEndPoint = new IPEndPoint(IPAddress.Parse(address), port);
            _ServerEndPoint = new IPEndPoint(IPAddress.Any, port);
            _Socket.Bind(_ServerEndPoint);

            Console.WriteLine("UDP SERVER INITIATED AT " + _Socket.LocalEndPoint.ToString());

            Receive();
        }


        public bool HasUser()
        {
            return _AllClients.Count() > 0;
        }

        public void SendToClient(byte[] data, int id)
        {
            SendToClient(data, _AllClients.Find(id));
        }

        public void SendToClient(byte[] data, ConnectedUser receiver)
        {

            _Socket.BeginSendTo(data, 0, data.Length, SocketFlags.None, receiver.Connection,
                (ar) =>
                {
                    State so = (State)ar.AsyncState;
                    int bytes = _Socket.EndSendTo(ar);
                    receiver.LatestSentData = DateTime.Now;
                    //  Logger.Log(Name + " SEND: " + data[0] + "," + data[1] + "," + data[2] + "," + data[3] + " TO " + epFrom.ToString());

                }, _State);
        }


        private void Receive()
        {
            Console.WriteLine("UDP SERVER IS LISTENING");

            _Socket.BeginReceiveFrom(_State.buffer, 0, bufSize, SocketFlags.None, ref _EpFrom, recv = (ar) =>
            {
                State so = (State)ar.AsyncState;
                int bytes = _Socket.EndReceiveFrom(ar, ref _EpFrom);
                _Socket.BeginReceiveFrom(so.buffer, 0, bufSize, SocketFlags.None, ref _EpFrom, recv, so);

               // Console.WriteLine("NetworkCommandType");

                if (so.buffer[0] == NetworkCommandType.FirstConnection)
                {
                    ConnectedUser uc = _AllClients.Find(_EpFrom);
                    if (uc == null)
                    {
                        uc = _AllClients.AddNew(0, _EpFrom);
                        uc.DataReceived(so.buffer);
                    }
                    else
                    {
                        uc.DataReceived(so.buffer);
                    }
                }
                //string receivedIp = _EpFrom.ToString();
                //bool clientFound = false;
                //for(int i=0; i<_Clients.Length; i++)
                //{
                //    if (_Clients[i] == null) {
                //        continue;
                //    }
                //    if (_Clients[i].Address==receivedIp)
                //    {
                //        _Clients[i].DataReceived( so.buffer );
                //        clientFound = true;
                //    }
                //}

                //if (!clientFound)
                //{
                //    UserClient uc = AddNewClient(_ClientsCount, (IPEndPoint)_EpFrom);
                //    uc.DataReceived(so.buffer);
                //}

            }, _State);
        }

    }
}
