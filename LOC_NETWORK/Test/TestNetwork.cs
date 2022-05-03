using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading;
 using LOC_NETWORK.NetworkCommands;
using LOC_SHARED.NetworkItems;

namespace LOC_NETWORK.Test
{
   public class TestNetwork
    {
       //static UDPSocket s = new UDPSocket();
       // static UDPSocket c = new UDPSocket();
        //public static void Client()
        //{ 
        //    c.Client("192.168.1.105", 27000);
        //    c.Name = "CLIENT";
        //    c.SendToServer(new byte[] { 0,1,2,3 });
             
        //}

        //public static void Server()
        //{ 
        //    s.Name = "SERVER";
        //    s.Server("192.168.1.105", 27000); 
        //}


        public static unsafe byte[] WriteBytes(int variable)
        {
            byte[] data = new byte[4];
           

            byte* bytes = ((byte*)&variable);
            data[0] = bytes[0];
            data[1] = bytes[1];
            data[2] = bytes[2];
            data[3] = bytes[3];
            return data;
        }

        //public static void StartSends()
        //{
        //    Stopwatch sw = new Stopwatch();
        //    sw.Start();
        //    for (int i = 0; i < 300; i++)
        //    {
        //        s.SendToClient(WriteBytes(i));
        //        c.SendToServer(WriteBytes(i)); 
        //    }
        //     Console.WriteLine(sw.ElapsedTicks.ToString());
        //    Console.WriteLine(sw.ElapsedMilliseconds.ToString());
        //    Console.WriteLine("----------------------------------------------");
        //}



        //public static void NewTest()
        //{
        //    UDPClient client = new UDPClient();
        //    UDPServer server = new UDPServer();
        //    server.Init("192.168.1.105", 27000);
        //    client.Init("192.168.1.105", 27000, "192.168.1.105", 27001);

        //    byte[] data = new byte[] {
        //            NetworkCommandType.FirstConnection,
        //            1,
        //            123
        //    };

        //    client.SendToServer(data);
        //    System.Threading.Thread.Sleep(1000);
        //    server.SendToClient(WriteBytes(14), 0);


        //    Stopwatch sw = new Stopwatch();
        //    sw.Start();
        //    for (int i = 0; i < 100; i++)
        //    {
        //        server.SendToClient(WriteBytes(i), 0);
        //    }
        //    Console.WriteLine(sw.ElapsedTicks.ToString());
        //    Console.WriteLine(sw.ElapsedMilliseconds.ToString());
        //    Console.WriteLine("----------------------------------------------");
        //}


        static Stopwatch swServerSendToReceive = new Stopwatch();

        //public static void TestServer(string serverIP, int serverPort)
        //{ 
        //    UDPServer server = new UDPServer();
        //    server.Init(serverIP, serverPort);


        //    Console.WriteLine("SERVER: WAITING USERS TO CONNECT");

        //    while (!server.HasUser())
        //    {
        //        System.Threading.Thread.Sleep(1000);
        //        Console.Write(".");
        //    }

        //    Console.WriteLine("----------------------------------------------");
        //    Console.WriteLine("USER CONNECTED. SERVER WILL SEND CONFIRMATION MESSAGE.");

        //    byte[] clientInitPacket = new byte[] {
        //            NetworkCommandType.ConnectionConfirmation,
        //            1,
        //            123
        //    };
        //    server.SendToClient(clientInitPacket, 0);
        //    System.Threading.Thread.Sleep(500);
        //    server.SendToClient(clientInitPacket, 0);
        //    Console.WriteLine("CLIENT CONNECTION CONFIRMATION SENT");

        //    Console.WriteLine("----------------------------------------------");
        //    Console.WriteLine("STARTING TO SEND 4 BYTES PACKET");

        //    Stopwatch sw = new Stopwatch();
        //    swServerSendToReceive.Start();
        //    sw.Start();
        //    int i = 0;
        //    for (  i = 0; i < 100000; i++)
        //    {
        //        server.SendToClient(WriteBytes(i), 0);
        //    }

        //    long ms = sw.ElapsedMilliseconds;

        //    //Console.WriteLine("TOTAL TIME TICKS:" + sw.ElapsedTicks.ToString());
        //   // Console.WriteLine("TOTAL TIME MS:" + ms.ToString());
        //    Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
        //    Console.WriteLine("TOTAL "+i.ToString()+" PACKETS SENT TO THE CLIENT IN " + ms.ToString() + " miliseconds"); 
        //}



        //public static void TestClient(string serverIP, int serverPort, string clientSelfIP, int clientSelfPort)
        //{
        //    UDPClient client = new UDPClient();
        //    client.Init(serverIP, serverPort, clientSelfIP, clientSelfPort);


        //    byte[] clientInitPacket = new byte[] {
        //            NetworkCommandType.FirstConnection,
        //            1,
        //            123
        //    };
             

        //    Console.WriteLine("CLIENT: SENDING CONNECT DATA AND WAITING SERVER TO REPLY");

        //    while (!client.IsConnected)
        //    {
        //        System.Threading.Thread.Sleep(1000);
        //        client.SendToServer(clientInitPacket);
        //        Console.Write("*");
        //    }

        //    Console.WriteLine("----------------------------------------------");
        //    Console.WriteLine("CONNECTED TO SERVER");
        //    Console.WriteLine("----------------------------------------------");
        //    Console.WriteLine("STARTING TO SEND 4 BYTES PACKET");

        //    Stopwatch sw = new Stopwatch();
        //    sw.Start();

        //    int i = 0;
        //    for ( i = 0; i < 10; i++)
        //    {
        //        client.SendToServer(WriteBytes(i));
        //    }
        //    long ms = sw.ElapsedMilliseconds;
        //    //Console.WriteLine("TOTAL TIME TICKS:" + sw.ElapsedTicks.ToString());
        //    //Console.WriteLine("TOTAL TIME MS:" + ms.ToString());
        //    Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
        //    Console.WriteLine("TOTAL  " + i.ToString() + " PACKETS SENT TO THE SERVER IN " + ms.ToString()+" miliseconds");
        //    while (client.TotalPacketsReceived<99990)
        //    {
        //        Thread.Sleep(1);
        //    }

             
        //    Console.WriteLine("TOTAL  " + client.TotalPacketsReceived.ToString() + " PACKETS RECEIVED IN "+ swServerSendToReceive.ElapsedMilliseconds.ToString()+" miliseconds");
            

        //}


        //public static void BuiltInTest(string serverIP, int serverPort, string clientSelfIP, int clientSelfPort)
        //{
        //    UDPBuiltIn.Init(clientSelfPort, serverIP, serverPort);

        //    while (!UDPBuiltIn.IsServerConnected)
        //    {
        //        System.Threading.Thread.Sleep(1000);
        //        UDPBuiltIn.ClientSend("From Client");
        //        UDPBuiltIn.ServerSend("From Server");
        //    }


        //}













        public static void TestServerLiteNetLib(string serverIP, int serverPort)
        {
            LiteNetServer = new LOCLiteNetLib.UDPServer();
            LiteNetServer.Init(serverIP, serverPort);


            Console.WriteLine("SERVER: WAITING USERS TO CONNECT");

            while (!LiteNetServer.HasUser())
            {
                System.Threading.Thread.Sleep(1000);
                Console.Write(".");
            }

            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("USER CONNECTED. SERVER WILL SEND CONFIRMATION MESSAGE.");

            byte[] clientInitPacket = new byte[] {
                    NetworkCommandType.SystemCommands.ConnectionConfirmation,
                    1,
                    123
            };
            LiteNetServer.SendToClient(clientInitPacket, 0);
            System.Threading.Thread.Sleep(500);
            LiteNetServer.SendToClient(clientInitPacket, 0);
            Console.WriteLine("CLIENT CONNECTION CONFIRMATION SENT");

            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("STARTING TO SEND 4 BYTES PACKET");

            Stopwatch sw = new Stopwatch();
            swServerSendToReceive.Start();
            sw.Start();
            int i = 0;
            for (i = 0; i < 100000; i++)
            {
                LiteNetServer.SendToClient(WriteBytes(i), 0);
            }

            long ms = sw.ElapsedMilliseconds;

            //Console.WriteLine("TOTAL TIME TICKS:" + sw.ElapsedTicks.ToString());
            // Console.WriteLine("TOTAL TIME MS:" + ms.ToString());
            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
            Console.WriteLine("TOTAL " + i.ToString() + " PACKETS SENT TO THE CLIENT IN " + ms.ToString() + " miliseconds");
        }

        static LOC_SHARED.NetworkItems.UDPClient LiteNetClient;
        static LOCLiteNetLib.UDPServer LiteNetServer;

        public static void TestClientLiteNetLib(string serverIP, int serverPort, string clientSelfIP, int clientSelfPort)
        {
            //LiteNetClient = new LOC_SHARED.NetworkItems.UDPClient();
            //LiteNetClient.Init(serverIP, serverPort, clientSelfIP, clientSelfPort);


            //byte[] clientInitPacket = new byte[] {
            //        NetworkCommandType.FirstConnection,
            //        1,
            //        123
            //};


            //Console.WriteLine("CLIENT: SENDING CONNECT DATA AND WAITING SERVER TO REPLY");

            //while (!LiteNetClient.IsConnected)
            //{
            //    System.Threading.Thread.Sleep(1000);
            //    LiteNetClient.ReConnect();
            //    LiteNetClient.SendToServer(clientInitPacket);
            //    Console.Write("*");
            //}

            //Console.WriteLine("----------------------------------------------");
            //Console.WriteLine("CONNECTED TO SERVER");
            //Console.WriteLine("----------------------------------------------");
            //Console.WriteLine("STARTING TO SEND 4 BYTES PACKET");

            //Stopwatch sw = new Stopwatch();
            //sw.Start();

            //int i = 0;
            //for (i = 0; i < 10; i++)
            //{
            //    LiteNetClient.SendToServer(WriteBytes(i));
            //}
            //long ms = sw.ElapsedMilliseconds;
            ////Console.WriteLine("TOTAL TIME TICKS:" + sw.ElapsedTicks.ToString());
            ////Console.WriteLine("TOTAL TIME MS:" + ms.ToString());
            //Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
            //Console.WriteLine("TOTAL  " + i.ToString() + " PACKETS SENT TO THE SERVER IN " + ms.ToString() + " miliseconds");
            //while (LiteNetClient.TotalPacketsReceived < 99999)
            //{
            //    Thread.Sleep(1);
            //}


            //Console.WriteLine("TOTAL  " + LiteNetClient.TotalPacketsReceived.ToString() + " PACKETS RECEIVED IN " + swServerSendToReceive.ElapsedMilliseconds.ToString() + " miliseconds");


        }

        public static void PollEvents()
        {
            //LiteNetClient.PollEvents(); 

            LiteNetServer.PollEvents();
            Thread.Sleep(15);

        }








        }
}
