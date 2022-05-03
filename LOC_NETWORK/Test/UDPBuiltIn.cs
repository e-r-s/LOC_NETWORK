using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace LOC_NETWORK.Test
{
    public class UDPBuiltIn
    {

        private static UdpClient server;
        private static UdpClient client;
        static IPEndPoint ClientIp = null;
        static IPEndPoint ServerIp = null;
        public static bool IsClientConnected = false;
        public static bool IsServerConnected = false;


        public static void Init(int clientPort, string serverIp, int serverPort)
        {
            server = new UdpClient(serverPort);
            client = new UdpClient(clientPort);

            ServerIp = new IPEndPoint(IPAddress.Parse(serverIp), serverPort);

            Thread thReadServer = new Thread(new ThreadStart(ServerReceive));
            thReadServer.Start();

            Thread thReadClient = new Thread(new ThreadStart(ClientReceive));
            thReadClient.Start();

            Console.WriteLine("SERVER AND CLIENT INITIATED");

        }


        protected static void ServerReceive()
        {
            while (true)
            {
                try
                {
                    string message = Encoding.ASCII.GetString(server.Receive(ref ClientIp));
                    Console.WriteLine(message);
                    IsServerConnected = true;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                
            }
        }

        public static void ServerSend(string message)
        {
            if (!IsServerConnected)
            {
                return;
            }
            server.Send(Encoding.ASCII.GetBytes(message), message.Length, ClientIp);
        }


        protected static void ClientReceive()
        {
            while (true)
            {
                try
                {
                    string message = Encoding.ASCII.GetString(client.Receive(ref ServerIp));
                    Console.WriteLine(message);
                    IsClientConnected = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

            }
        }

        public static void ClientSend(string message)
        {
            //if (!IsClientConnected)
            //{
            //    return;
            //}
            Console.WriteLine("Client is Sending: "+message + " TO " + ServerIp.ToString());
            client.Send(Encoding.ASCII.GetBytes(message), message.Length, ServerIp);
        }


    }
}
