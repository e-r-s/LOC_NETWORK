using System;
using System.IO;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace LOC_NETWORK.Networking
{
    public class HTTPServer
    {




        public static HttpListener listener;
        public static string url = "http://localhost:8000/";
        public static int pageViews = 0;
        public static int requestCount = 0;
        public static string pageData =
            "<!DOCTYPE>" +
            "<html>" +
            "  <head>" +
            "    <title>HttpListener Example</title>" +
            "  </head>" +
            "  <body>" +
            "    <p>Page Views: {0}</p>" +
            "    <form method=\"post\" action=\"login\">" +
            "      <input type=\"text\" name=\"uname\" >" +
            "      <input type=\"password\" name=\"pwd\" >" +
            "      <input type=\"submit\" value=\"Login\" {1}>" +
            "    </form>" +
            "  </body>" +
            "</html>";


        public static async Task HandleIncomingConnections()
        {
            bool runServer = true;

            // While a user hasn't visited the `shutdown` url, keep on handling requests
            while (runServer)
            {
                // Will wait here until we hear from a connection
                HttpListenerContext ctx = await listener.GetContextAsync();

                // Peel out the requests and response objects
                HttpListenerRequest req = ctx.Request;
                HttpListenerResponse resp = ctx.Response;

                // Print out some info about the request
                Console.WriteLine("Request #: {0}", ++requestCount);
                Console.WriteLine(req.Url.ToString());
                Console.WriteLine(req.HttpMethod);
                Console.WriteLine(req.UserHostName);
                Console.WriteLine(req.UserAgent);
                Console.WriteLine();

                // If `shutdown` url requested w/ POST, then shutdown the server after serving the page
                if ((req.HttpMethod == "POST") && (req.Url.AbsolutePath == "/shutdown"))
                {
                    Console.WriteLine("Shutdown requested");
                    runServer = false;
                }

                // If `shutdown` url requested w/ POST, then shutdown the server after serving the page
                if ((req.HttpMethod == "POST") && (req.Url.AbsolutePath == "/login"))
                {
                    ProcessRequest(req, resp);
                }
                else
                {


                    // Make sure we don't increment the page views counter if `favicon.ico` is requested
                    if (req.HttpMethod == "POST" && req.Url.AbsolutePath != "/favicon.ico")
                    {
                        pageViews += 1;
                    }

                    // Write the response info
                    string disableSubmit = !runServer ? "disabled" : "";
                    byte[] data = Encoding.UTF8.GetBytes(String.Format(pageData, pageViews, disableSubmit));
                    resp.ContentType = "text/html";
                    resp.ContentEncoding = Encoding.UTF8;
                    resp.ContentLength64 = data.LongLength;
                    // Write out to the response stream (asynchronously), then close it
                    await resp.OutputStream.WriteAsync(data, 0, data.Length);
                    resp.Close();

                }


            }
        }


        public static void Init()
        {
            // Create a Http server and start listening for incoming connections
            listener = new HttpListener();
            listener.Prefixes.Add(url);
            listener.Start();
            Console.WriteLine("Listening for connections on {0}", url);

            // Handle requests
            Task listenTask = HandleIncomingConnections();
            listenTask.GetAwaiter().GetResult();

            // Close the listener
            listener.Close();
        }





        public static void ProcessRequest(HttpListenerRequest request, HttpListenerResponse response)
        {
            if (!request.HasEntityBody)
            {
                Console.WriteLine("No client data was sent with the request.");
                return;
            }
            System.IO.Stream body = request.InputStream;
            System.Text.Encoding encoding = request.ContentEncoding;
            System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
            if (request.ContentType != null)
            {
                Console.WriteLine("Client data content type {0}", request.ContentType);
            }
            Console.WriteLine("Client data content length {0}", request.ContentLength64);

            Console.WriteLine("Start of client data:");
            // Convert the data to a string and display it on the console.
            string s = reader.ReadToEnd();
            string[] parameters = s.Split('&');
            string userName = parameters[0].Replace("uname=", "");
            string password = parameters[1].Replace("pwd=", "");
            if(userName=="1" && password == "1")
            {
                string result = NetworkManager.LoginUser(userName, password, request.RemoteEndPoint.Address.ToString());
                

                using (var writer = new StreamWriter(response.OutputStream, Encoding.UTF8))
                {
                    response.StatusCode = 200;
                    response.ContentType = "application/json";
                    writer.Write(result); 
                }
                 
            }
            Console.WriteLine(s);
            Console.WriteLine("End of client data:");
            body.Close();
            reader.Close();
            // If you are finished with the request, it should be closed also.
        }



    }
}
