using System;
using System.Net;
using LiteNetLib; 
using LOC_SHARED.Util;

namespace LOC_NETWORK.Networking
{
    public class ConnectedServer
    {
        public IPEndPoint Connection = new IPEndPoint(IPAddress.Any, 0);
        public string Address = "";
        public DateTime LatestReceivedData;
        public DateTime LatestSentData;
        public byte[] LatestData;
        public int Id;

        public NetPeer peer;

        public void DataReceived(byte[] buffer)
        {
            LatestReceivedData = DateTime.Now;
            LatestData = buffer;
            Logger.Log("CLIENT RECEIVED: " + buffer[0] + "," + buffer[1] + "," + buffer[2] + "," + buffer[3] + " FROM " + Address);


        }
    }
}
