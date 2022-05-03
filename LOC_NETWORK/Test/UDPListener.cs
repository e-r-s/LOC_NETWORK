using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace LOC_NETWORK.Test
{

    public class UDPListener
    {
        UdpClient clientData;
        int portData = 50505;
        public int receiveBufferSize = 120000;

        public bool showDebug = false;
        IPEndPoint ipEndPointData;
        private object obj = null;
        private System.AsyncCallback AC;
        byte[] receivedBytes;



        public void InitializeUDPListener()
        {
            ipEndPointData = new IPEndPoint(IPAddress.Any, portData);
            clientData = new UdpClient();
            clientData.Client.ReceiveBufferSize = receiveBufferSize;
            clientData.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, optionValue: true);
            clientData.ExclusiveAddressUse = false;
            clientData.EnableBroadcast = true;
            clientData.Client.Bind(ipEndPointData);
            clientData.DontFragment = true;
            if (showDebug) Console.WriteLine("BufSize: " + clientData.Client.ReceiveBufferSize);
            AC = new System.AsyncCallback(ReceivedUDPPacket);
            clientData.BeginReceive(AC, obj);
            Console.WriteLine("UDP - Start Receiving..");
        }

        void ReceivedUDPPacket(System.IAsyncResult result)
        {
            //stopwatch.Start();
            receivedBytes = clientData.EndReceive(result, ref ipEndPointData);

            ParsePacket();

            clientData.BeginReceive(AC, obj);

            //stopwatch.Stop();
            //Debug.Log(stopwatch.ElapsedTicks);
            //stopwatch.Reset();
        } // ReceiveCallBack

        void ParsePacket()
        {
            // work with receivedBytes
            Console.WriteLine("receivedBytes len = " + receivedBytes.Length);
        }

        void OnDestroy()
        {
            if (clientData != null)
            {
                clientData.Close();
            }

        }
    }

}