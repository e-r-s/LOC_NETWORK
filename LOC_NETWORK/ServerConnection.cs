using System;
using System.Net;
using LOC_NETWORK.Util;

namespace LOC_NETWORK
{
    public class _ServerConnection
    {
        public IPEndPoint Connection = new IPEndPoint(IPAddress.Any, 0);
        public string Address = "";
        public DateTime LatestReceivedData;
        public DateTime LatestSentData;
        public byte[] LatestData;
        public int Id;
        
        public void DataReceived(byte[] buffer)
        {
            LatestReceivedData = DateTime.Now;
            LatestData = buffer;
            Logger.Log("CLIENT RECEIVED: " + buffer[0] + "," + buffer[1] + "," + buffer[2] + "," + buffer[3] + " FROM " + Address);


        }
    }
}
