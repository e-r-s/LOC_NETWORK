using System;
using System.Diagnostics;
using System.Numerics;
using System.Threading;
using LiteNetLib;
using LiteNetLib.Utils;
using LOC_NETWORK.GameItems;
using LOC_NETWORK.NetworkCommands;
using LOC_NETWORK.Networking;
using LOC_NETWORK.Test;
using LOC_SHARED.NetworkCommands;
using LOC_SHARED.NetworkItems;
using LOC_SHARED.Util;

namespace LOC_NETWORK
{
    class MainClass
    {

        static string ServerIP = "192.168.1.105";
        static int ServerPort = 8082;
        static string SelfIP = "192.168.1.105";
        static int SelfPort = 8083;
        public static void Main(string[] args)
        {

            ServerTest.InitServer();
            Thread.Sleep(2000);
            //ServerTest.InitClient();
            //Thread.Sleep(2000);
            //ServerTest.ClientLogin();

            ServerTest.OnConnected += ServerTest_OnConnected;
        


            //GameManager.Init();

            //Logger.Init();

            //SelfIP = NetworkHelper.GetIPAddress();
            //ServerIP = NetworkHelper.GetIPAddress();


            //Thread thServer = new Thread(new ThreadStart(TestClient));
            //thServer.Start();
            //TestNetwork.TestServer(ServerIP, ServerPort);


            //Thread thHTTPServer = new Thread(new ThreadStart(HTTPServer.Init));
            //thHTTPServer.Start();

            //Thread thTest2 = new Thread(new ThreadStart(TestServerLiteNetLib));
            //thTest2.Start();
            //Thread thTest = new Thread(new ThreadStart(TestClientLiteNetLib));
            //thTest.Start();
            //Thread.Sleep(3000);
            //Thread thTest3 = new Thread(new ThreadStart(TestLiteNetLib));
            //thTest3.Start();


            //LOC_SHARED.NetworkItems.ClientNetworkManager.Init();
            //var loginRes = LOC_SHARED.NetworkItems.ClientNetworkManager.Login("1","1"); 
            //AllNetworkCommands.PlayerActionCommands.PlayerData_Position.SendData(1, 1, new UnityEngine.Vector3(1,1,1), new UnityEngine.Quaternion(1,1,1,1));


            //Test.BuiltInTest(ServerIP, ServerPort, SelfIP, SelfPort);


            //new UDPListener().InitializeUDPListener();

            //UDPer.UDPer.ServerIP = ServerIP;
            //UDPer.UDPer.Init();

            //Console.WriteLine("Litenet lib test");


            //EventBasedNetListener listener = new EventBasedNetListener();
            //NetManager server = new NetManager(listener);
            //server.Start(8084 /* port */);

            //listener.ConnectionRequestEvent += request =>
            //{
            //    if (server.ConnectedPeersCount < 10 /* max connections */)
            //        request.AcceptIfKey("SomeConnectionKey");
            //    else
            //        request.Reject();
            //};

            //listener.PeerConnectedEvent += peer =>
            //{
            //    Console.WriteLine("We got connection: {0}", peer.EndPoint); // Show peer ip
            //    NetDataWriter writer = new NetDataWriter();                 // Create writer class
            //    writer.Put("Hello client!");                                // Put some string
            //    peer.Send(writer, DeliveryMethod.Unreliable);             // Send with reliability


            //    Vector3 coordinates = new Vector3(10f, 20f, 30f);
            //    Quaternion rotation = new Quaternion(1f, 20f, 30f, 30f);

            //    Vector3 coordinates2 = new Vector3(10f, 20f, 30f);
            //    Quaternion rotation2 = new Quaternion(1f, 20f, 30f, 30f);

            //    sw.Start();
            //    for (int i = 0; i < 1000; i++)
            //    {

            //        NetDataWriter writer2 = new NetDataWriter();                 // Create writer class
            //        writer2.Put(i);                              
            //        writer2.Put(100000-i);                              
            //        writer2.Put(NetworkCommandType.ConnectionConfirmation);                              
            //        writer2.Put(NetworkCommandType.PlayerData_Crouch);                              
            //        writer2.Put(coordinates.X);                              
            //        writer2.Put(coordinates.Y);                              
            //        writer2.Put(coordinates.Z);
            //        writer2.Put(rotation.X);                              
            //        writer2.Put(rotation.Y);                              
            //        writer2.Put(rotation.Z);                              
            //        writer2.Put(rotation.W);
            //        writer2.Put((100000 - i) / 1.2f);
            //        writer2.Put(coordinates2.X);
            //        writer2.Put(coordinates2.Y);
            //        writer2.Put(coordinates2.Z);
            //        writer2.Put(rotation2.X);
            //        writer2.Put(rotation2.Y);
            //        writer2.Put(rotation2.Z);
            //        writer2.Put(rotation2.W);
            //        peer.Send(writer2, DeliveryMethod.Sequenced); // Send with reliability
            //    }


            //};

            //Thread thLNClient = new Thread(new ThreadStart(LiteNetClient));
            //thLNClient.Start();

            //while (!Console.KeyAvailable)
            //{
            //    server.PollEvents();
            //    Thread.Sleep(15);
            //}

            //server.Stop();

            //  LOC_SHARED.NetworkItems.NetworkManager.Login();

        }

        private static void ServerTest_OnConnected()
        {
            throw new NotImplementedException();
        }

        //static Stopwatch sw = new Stopwatch();
        //static int totalReceived = 0;
        //public static void LiteNetClient()
        //{
        //    EventBasedNetListener listener = new EventBasedNetListener();
        //    NetManager client = new NetManager(listener);
        //    client.Start();
        //    client.Connect("192.168.1.105" /* host ip or name */, 8084 /* port */, "SomeConnectionKey" /* text key or NetDataWriter */);
        //    listener.NetworkReceiveEvent += (fromPeer, dataReader, deliveryMethod) =>
        //    {
        //        totalReceived++;
        //        if (totalReceived > 999)
        //        {
        //            Console.WriteLine("TOTAL TIME TICKS:" + sw.ElapsedTicks.ToString());
        //            Console.WriteLine("TOTAL TIME MS:" + sw.ElapsedMilliseconds.ToString());
        //            Console.WriteLine("----------------------------------------------");
        //            Console.WriteLine("TOTAL 10000 PACKETS SENT TO THE CLIENT");
        //            Console.WriteLine("ALL DATA RECEIVED");
        //        }
        //       // Console.WriteLine("We got: {0}", dataReader.GetString(100 /* max length of string */));
        //        dataReader.Recycle();
        //    };

        //    while (!Console.KeyAvailable)
        //    {
        //        client.PollEvents();
        //        Thread.Sleep(15);
        //    }

        //    client.Stop();
        //}

        public static void TestClient()
        {
            //  TestNetwork.TestClient(ServerIP, ServerPort, SelfIP, SelfPort);
        }

        public static void TestClientLiteNetLib()
        {
            TestNetwork.TestClientLiteNetLib(ServerIP, ServerPort, SelfIP, SelfPort);
        }
        public static void TestServerLiteNetLib()
        {
            TestNetwork.TestServerLiteNetLib(ServerIP, ServerPort);
            
        }

        public static void TestLiteNetLib()
        {

            Thread.Sleep(100);
            while (true)
            {
                TestNetwork.PollEvents();
            }
        }


    }
}
