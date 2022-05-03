using System;
using System.Net;

namespace LOC_NETWORK.Networking
{
    public class LoggedInUser
    {
         
        public string IPAddress = "";
        public DateTime LoginTime; 
        public int UID;
        public byte PrivateKey { get; set; }
        public string APIKey { get; set; }

       

    }
}
