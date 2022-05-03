using System;
using System.Collections.Generic;
using System.Net;
using LiteNetLib;
using LOC_NETWORK.GameItems;
using LOC_NETWORK.GameItems.Creature;

namespace LOC_NETWORK.Networking
{
    public class ConnectedUserCollection
    {

        List<ConnectedUser> _Clients = new List<ConnectedUser>();

        public int Count()
        {
            return _Clients.Count;
        }

 
             

        public ConnectedUser AddNew(int id, EndPoint ip, NetPeer peer, Player p)
        {
            ConnectedUser uc = new ConnectedUser();
            uc.Address = ip.ToString();
            uc.Connection = (IPEndPoint)ip;
            uc.Id = id;
            uc.peer = peer;
            uc.PlayerData = p;
            p.ConnectionReference = uc;

            uc.PrivateKey = new byte[8];
            System.Random rnd = new System.Random();
            rnd.NextBytes(uc.PrivateKey);

            bool found = false;
            for (int i = 0; i < _Clients.Count; i++)
            {
                if (_Clients[i] == null)
                {
                    _Clients[i] = uc;
                    found = true;
                }
            }

            if (!found)
            {
                _Clients.Add(uc);
            }

            return uc;

        }




        //public List<ConnectedUser> FindPlayersNearby(ConnectedUser user)
        //{
        //    List<ConnectedUser> foundUsers = new List<ConnectedUser>();

        //    var allPlayers = GameManager.GetPlayersNearBy(user.PlayerData, 200);
        //    for(int i=0; i<allPlayers.Count; i++)
        //    { 
        //        foundUsers.Add(allPlayers[i].ConnectionReference);
        //    }
             
        //    return foundUsers;
        //}



        public ConnectedUser Find(int id)
        {
            for (int i = 0; i < _Clients.Count; i++)
            {
                if (_Clients[i] == null)
                {
                    continue;
                }
                if (_Clients[i].Id == id)
                {
                    return _Clients[i];
                }
            }

            return null;
        }

        public ConnectedUser Find(EndPoint ip)
        {
            string ipString = ip.ToString();
            for (int i = 0; i < _Clients.Count; i++)
            {
                if (_Clients[i] == null)
                {
                    continue;
                }
                if (_Clients[i].Address == ipString)
                {
                    return _Clients[i];
                }
            }

            return null;
        }

        public void DataReceivedForClient(EndPoint ip, byte[] data, int startIndex, int length)
        {

            string ipString = ip.ToString();
            for (int i = 0; i < _Clients.Count; i++)
            {
                if (_Clients[i] == null)
                {
                    continue;
                }
                if (_Clients[i].Address == ipString)
                {
                    _Clients[i].DataReceived(data,   startIndex,   length);
                    return;
                }
            }

        }

         
        public bool Remove(int id)
        {
            for (int i = 0; i < _Clients.Count; i++)
            {
                if (_Clients[i] == null)
                {
                    continue;
                }
                if (_Clients[i].Id == id)
                {
                    _Clients[i].Destroy();
                    _Clients[i] = null;
                    GC.Collect();

                    return true;
                }
            }
            return false;
        }


        public bool RemoveAt(int index)
        {

            if (_Clients[index] != null)
            {
                _Clients[index].Destroy();
                _Clients[index] = null;
                GC.Collect();
                return true;
            }
            return false;
        }


        public bool Remove(ConnectedUser data)
        {
            for (int i = 0; i < _Clients.Count; i++)
            {
                if (_Clients[i] == null)
                {
                    continue;
                }
                if (_Clients[i].Id == data.Id)
                {
                    _Clients[i].Destroy();
                    _Clients[i] = null;
                    GC.Collect();

                    return true;
                }
            }
            return false;
        }


    }
}
