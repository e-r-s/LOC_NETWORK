 
using System;
using System.Net;
using LiteNetLib;
using LOC_SHARED.NetworkCommands;
using LOC_SHARED.Util;

namespace LOC_SHARED.NetworkItems
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

        /// <summary>
        /// Data receeived from server
        /// </summary>
        /// <param name="buffer"></param>
        public void DataReceived(byte[] buffer, int startIndex, int length)
        {
           
            LatestReceivedData = DateTime.Now;
            LatestData = buffer;
            if (buffer.Length > 15 && length>15)
            {
                AllNetworkCommands.FindAndRunCommand(buffer, startIndex, length);
            }
            Logger.Log("CLIENT RECEIVED: " + buffer[0] + "," + buffer[1] + "," + buffer[2] + "," + buffer[3] + " FROM " + Address);


        }
    }
}
