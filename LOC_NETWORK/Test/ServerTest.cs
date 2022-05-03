using System;
using System.Threading;
using LOC_NETWORK.GameItems;
using LOC_NETWORK.Networking;
using LOC_SHARED.NetworkCommands;
using LOC_SHARED.Util;
using System.Diagnostics;
using System.Net.Sockets; 
using LOC_NETWORK.NetworkCommands;
using LOC_SHARED.NetworkItems;

namespace LOC_NETWORK.Test
{

    public delegate void ConnectionEvent();

    public class ServerTest
    {

        public static event ConnectionEvent OnConnected;

        static string ServerIP = "192.168.1.105";
        static int ServerPort = 8082;
        static string SelfIP = "192.168.1.105";
        static int SelfPort = 8083;



        public static void InitServer()
        {
            GameManager.Init();

            Logger.Init();

          
            ////Thread thHTTPServer = new Thread(new ThreadStart(HTTPServer.Init));
            ////thHTTPServer.Start(); 
            ////Thread thTest2 = new Thread(new ThreadStart(RunServer));
            ////thTest2.Start(); 
            ////Thread.Sleep(3000);
            //Thread thTest3 = new Thread(new ThreadStart(ServerPollEvents));
            //thTest3.Start();

            //Thread thTest4 = new Thread(new ThreadStart(ClientCheck));
            //thTest4.Start();
        }

        public static void ClientCheck()
        {
            while (true)
            {
                if (LOC_SHARED.NetworkItems.ClientNetworkManager.IsReadyToSendData)
                {
                    //if (OnConnected != null)
                    //{
                    //    OnConnected.Invoke();
                    //}
                    ServerTest.ClientSendData1();
                    return;
                }
            }
        
        }
        public static void InitClient()
        {
            LOC_SHARED.NetworkItems.ClientNetworkManager.Init();
        }

        public static void ClientLogin()
        { 
            LOC_SHARED.NetworkItems.ClientNetworkManager.Login("1", "1"); 
        }

        public static void ClientSendData1()
        {
            AllNetworkCommands.PlayerActionCommands.PlayerData_Position.SendData(510, -1, new UnityEngine.Vector3(510f, 20f, 1010f), new UnityEngine.Quaternion(1f, 1f, 1f, 1f));
        }
         
        public static void ClientSendData2()
        {
            AllNetworkCommands.PlayerActionCommands.PlayerData_Position.SendData(1, -1, new UnityEngine.Vector3(1, 1, 1), new UnityEngine.Quaternion(1, 1, 1, 1));
        }


        //static LOCLiteNetLib.UDPServer LiteNetServer;

        //public static void RunServer()
        //{
        //    LiteNetServer = new LOCLiteNetLib.UDPServer();
        //    LiteNetServer.Init(ServerIP, ServerPort);


        //    Console.WriteLine("SERVER: WAITING USERS TO CONNECT");

        //    while (!LiteNetServer.HasUser())
        //    {
        //        System.Threading.Thread.Sleep(1000);
        //        Console.Write(".");
        //    }

        //    //Console.WriteLine("----------------------------------------------");
        //    //Console.WriteLine("USER CONNECTED. SERVER WILL SEND CONFIRMATION MESSAGE.");

        //    //byte[] clientInitPacket = new byte[] {
        //    //        NetworkCommandType.SystemCommands.ConnectionConfirmation,
        //    //        1,
        //    //        123
        //    //};

        //    //LiteNetServer.SendToClient(clientInitPacket, 0);
        //    System.Threading.Thread.Sleep(500);
        // //   LiteNetServer.SendToClient(clientInitPacket, 0);
        //    Console.WriteLine("CLIENT CONNECTION CONFIRMATION SENT");

        //    Console.WriteLine("----------------------------------------------");
        //    //Console.WriteLine("STARTING TO SEND 4 BYTES PACKET");

        //    //Stopwatch sw = new Stopwatch();
        //    //swServerSendToReceive.Start();
        //    //sw.Start();
        //    //int i = 0;
        //    //for (i = 0; i < 100000; i++)
        //    //{
        //    //    LiteNetServer.SendToClient(WriteBytes(i), 0);
        //    //}

        //    //long ms = sw.ElapsedMilliseconds;

        //    //Console.WriteLine("TOTAL TIME TICKS:" + sw.ElapsedTicks.ToString());
        //    // Console.WriteLine("TOTAL TIME MS:" + ms.ToString());
        //    //Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
        //    //Console.WriteLine("TOTAL " + i.ToString() + " PACKETS SENT TO THE CLIENT IN " + ms.ToString() + " miliseconds");

        //    //TestNetwork.TestServerLiteNetLib(ServerIP, ServerPort);

        //}

        //public static void ServerPollEvents()
        //{
        //    Thread.Sleep(100);
        //    while (LiteNetServer==null)
        //    {
        //        Thread.Sleep(1000);
        //    }

        //    while (true)
        //    { 
        //        LiteNetServer.PollEvents();
               
        //    }
        //}


    }
}
