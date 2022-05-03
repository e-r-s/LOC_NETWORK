using System;
using System.Collections.Generic;
using System.Net;

namespace LOC_NETWORK
{
    public class _UserClientCollection
    {
        public UserClientCollection()
        {

        }


        List<UserClient> _Clients = new List<UserClient>();

        public int Count()
        {
            return _Clients.Count;
        }


        public UserClient AddNew(int id, EndPoint ip)
        {
            UserClient uc = new UserClient();
            uc.Address = ip.ToString();
            uc.Connection = (IPEndPoint)ip;
            uc.Id = id;

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

        public UserClient Find(int id)
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

        public UserClient Find(EndPoint ip)
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

        public void DataReceivedForClient(EndPoint ip, byte[] data)
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
                    _Clients[i].DataReceived(data);
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


        public bool Remove(UserClient data)
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
