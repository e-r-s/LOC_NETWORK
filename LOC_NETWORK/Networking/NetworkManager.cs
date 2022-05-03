using System;
using System.Collections.Generic;
using System.Threading;
using System.Web.Script.Serialization;
using LOC_NETWORK.GameItems;
using LOC_NETWORK.GameItems.Creature;
using LOC_NETWORK.LOCLiteNetLib;
using LOC_SHARED.NetworkItems;

namespace LOC_NETWORK.Networking
{
    public class NetworkManager
    {

        static string ServerIP = "192.168.1.105";
        static int ServerPort = 8082;

        public static byte PacketHeadFirst;
        public static byte PacketHeadSecond;
        public static byte PacketTailFirst;
        public static byte PacketTailSecond;


        public static List<EncryptionKey> EncryptionKeys = new List<EncryptionKey>();

        public static void Init()
        {
            for(int i=0; i<13; i++)
            {
                EncryptionKey k = new EncryptionKey();
                k.Id = (byte)i;
                EncryptionKeys.Add(k);
            }

            PacketHeadFirst = (byte)new Random().Next(50, 250);
            PacketHeadSecond = (byte)new Random().Next(50, 250);
            PacketTailFirst = (byte)new Random().Next(50, 250);
            PacketTailSecond = (byte)new Random().Next(50, 250);

            Server = new UDPServer();
            Server.Init(ServerIP, ServerPort);


            Thread polEventsThread = new Thread(new ThreadStart(ServerPollEvents));
            polEventsThread.Start();

            Thread thHTTPServer = new Thread(new ThreadStart(HTTPServer.Init));
            thHTTPServer.Start();

            PacketManager.Init(123123, 1, EncryptionKeys, PacketHeadFirst, PacketHeadSecond, PacketTailFirst, PacketTailSecond);



        }

        public static string LoginUser(string userName, string password, string ip)
        {
            int uid = 0;
            if(userName=="1" && password == "1")
            {
                uid = 1;
                Player p = GameManager.GetPlayerByUID(uid);
                
                ApiResult<PlayerLoginResult> result = new ApiResult<PlayerLoginResult>();
                result.Data = GameManager.PlayerLogedIn(p, ip);
                result.Error = "";
                result.RequiestId = 1;
                result.Success = true;
                result.Time = DateTime.Now.Ticks;
                JavaScriptSerializer ser = new JavaScriptSerializer();
                return ser.Serialize(result);
                 
            }
            return string.Empty;
            
        }

        public static UDPServer Server;

        public static void SendDataToUsers(List<ConnectedUser> users, byte[] data)
        {
            Server.SendToClients(users, data);
        }
        public static void SendDataToUser(ConnectedUser user, byte[] data)
        {
            Server.SendToClient( data, user );
        }


        public static void ServerPollEvents()
        {
            Thread.Sleep(100);
            
            while (true)
            {
                Server.PollEvents(); 
            }
        }


    }
}
