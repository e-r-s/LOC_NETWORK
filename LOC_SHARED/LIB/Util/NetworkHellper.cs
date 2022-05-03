using System;
using System.IO;
using System.Net;
using System.Text; 
using UnityEngine;
using UnityEngine.Bindings;


namespace LOC_SHARED.Util
{
    public class NetworkHelper
    {


        public static string GetIPAddress()
        {
            String address = "";
            WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
            using (WebResponse response = request.GetResponse())
            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            {
                address = stream.ReadToEnd();
            }

            int first = address.IndexOf("Address: ") + 9;
            int last = address.LastIndexOf("</body>");
            address = address.Substring(first, last - first);

            return address;
        }

        public static string SendRequest(string url, string postData)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            WebRequest request = WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            using (WebResponse response = request.GetResponse())
            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            {
                Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                Console.WriteLine(responseFromServer);
                reader.Close();
                dataStream.Close();
                response.Close();
                return responseFromServer;
            }
 
             

        }

        public static string ObjectToJson(object data)
        {

#if UNITY_EDITOR
            return JsonUtility.ToJson(data);
#else
            System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
            return ser.Serialize(data);
#endif
            // UnityEngine


            //JavaScriptSerializer ser = new JavaScriptSerializer();
            //return ser.Serialize(data);
        }

        public static T JsonToObject<T>(string data)
        {

#if UNITY_EDITOR
            return JsonUtility.FromJson<T>(data);
#else
            System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
            return ser.Deserialize<T>(data);
#endif
             
        }

        

    }
}
