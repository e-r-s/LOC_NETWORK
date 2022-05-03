using System;
using System.Threading;
using LOC_SHARED.NetworkItems;
using LOC_SHARED.Util;

namespace LOC_SHARED.NetworkItems
{
    public delegate void LoginEvent(PlayerLoginResult loginData);
    public static class ClientNetworkManager
    {
        private static UDPClient client;
        private static PlayerLoginResult LoginData;
        public static bool IsReadyToSendData= false;

        static string ServerIP = "192.168.1.105";
        static int ServerPort = 8082;
        static string SelfIP = "192.168.1.105";
        static int SelfPort = 8083;


        static bool _UDPCanConnect = false;

        static Thread UDPThread;
        static Thread UDPThread2;

        public static event LoginEvent OnLoggedIn;

        /// <summary>
        /// Inits ClientNetworkManager threads and udp client.
        /// </summary>
        public static void Init()
        { 
            client = new UDPClient();
            UDPThread = new Thread(new ThreadStart(ConnectUDP));
            UDPThread.Start();

            UDPThread2 = new Thread(new ThreadStart(PollEvents));
            UDPThread2.Start();
        }

        static void PollEvents()
        {
            Thread.Sleep(1000);
            while (true)
            {
                client.PollEvents();
                Thread.Sleep(15);
            }
        }

        static void ConnectUDP()
        {
            while (!_UDPCanConnect)
            {
                System.Threading.Thread.Sleep(1000);
            }

            client.Init(ServerIP, ServerPort, SelfIP, SelfPort, LoginData.APIKey);


            //LoginData....

            byte[] clientInitPacket = new byte[] {
                    NetworkCommandType.SystemCommands.FirstConnection,
                    1,
                    123
            };


            while (!client.IsConnected)
            {
                System.Threading.Thread.Sleep(3000);
                client.ReConnect();
                client.SendToServer(clientInitPacket);
            }

            Thread.Sleep(200);

            IsReadyToSendData = true;

            Console.WriteLine("UDP Connected");

           

        }


        /// <summary>
        /// Seends byte array to serveer
        /// </summary>
        /// <param name="data"></param>
        public static void SendDataToServer(byte[] data)
        {
            client.SendToServer(data);
        }


        ///TODO ADD Client side Data file versions. 
        /// <summary>
        /// Login via HTTP.
        /// </summary>
        /// <param name="userName">Login username</param>
        /// <param name="password">Login Password</param>
        /// <returns>Login result. All player data + All other characters data + all map data(only modified part).</returns>
        public static ApiResult<PlayerLoginResult> Login(string userName, string password)
        {
            string reqString = NetworkHelper.SendRequest("http://localhost:8000/login", "uname="+ userName +"&pwd="+ password + "");
            if (String.IsNullOrEmpty(reqString))
            {
                return new ApiResult<PlayerLoginResult>();
            }
            ApiResult<PlayerLoginResult> result = NetworkHelper.JsonToObject<ApiResult<PlayerLoginResult>>(reqString);
            result.RawData = reqString;
            if (result.Success)
            {
                LoginData = result.Data;
                _UDPCanConnect = true;

                PacketManager.Init(LoginData.UID, LoginData.PrivateKey, LoginData.EncryptionKeys, LoginData.PacketHeadFirst, LoginData.PacketHeadSecond, LoginData.PacketTailFirst, LoginData.PacketTailSecond);

                if (OnLoggedIn != null)
                {
                    OnLoggedIn.Invoke(result.Data);
                }
            }
            return result;
        }


    }
}
