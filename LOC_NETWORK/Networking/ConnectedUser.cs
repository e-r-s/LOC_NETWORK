using System;
using System.Net;
using LiteNetLib;
using LOC_NETWORK.GameItems;
using LOC_NETWORK.GameItems.Creature;
using LOC_SHARED.NetworkCommands;
using LOC_SHARED.Util;

namespace LOC_NETWORK.Networking
{
    public class ConnectedUser
    {
        public IPEndPoint Connection = new IPEndPoint(IPAddress.Any, 0);
        public string Address = "";
        public DateTime LatestReceivedData;
        public DateTime LatestSentData;
        public byte[] LatestData;
        public int Id;

        public NetPeer peer;

        public Player PlayerData;

        public byte[] PrivateKey;


        public void DataReceived(byte[] buffer, int startIndex, int length)
        {
            LatestReceivedData = DateTime.Now;
            LatestData = buffer;

            Logger.Log("SERVER RECEIVED TOTAL BYTES",buffer.Length.ToString());
            if (buffer.Length > 1000 && length > 15)
            {
                Logger.Log("SERVER RECEIVED MANY");
            }
            if (buffer.Length > 15 && length>15)
            {
                AllNetworkCommands.FindAndRunCommand(buffer,   startIndex,   length);
            }
        }

        public void Destroy()
        {
            Connection = null;
            LatestData = new byte[0];

        }
    }
}
