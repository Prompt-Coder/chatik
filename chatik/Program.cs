
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace chatik
{
    /*class LoginData
    {
        public string path;
        public string udlAdd;
        public LoginData(string x, string y)
        {
            path = x;
            udlAdd = y;
            
        }
        
    }
*/
    class ChatMessage
    {
        public static HttpListener listener;
        public static ConcurrentDictionary<string, string> fileLinks = new ConcurrentDictionary<string, string>();
        static string path;
        static string urlAdd;


        public static void Main()
        {
            
            Console.WriteLine("Current working directory: " + Directory.GetCurrentDirectory());
            listener = new HttpListener();
            string link = ("http://*:81/");
            listener.Prefixes.Add($"{link}");
            listener.Start();
            new Thread(HttpRequestsListener).Start();
            
            
        }

        private static void HttpRequestsListener()
        {
            Console.WriteLine("Enter your nickname");
            path = Console.ReadLine() + ".txt";
            urlAdd = Guid.NewGuid().ToString();
            fileLinks.TryAdd(urlAdd, path);
            Console.WriteLine($"http://prompt.modeller.fvds.ru/" + urlAdd);
            /*LoginData loginData = new LoginData(path, urlAdd);*/

            if (path as string != null)
            {
                new Thread(Request).Start();
            }
            /*var context = listener.GetContext();
            var contextPath = context.Request.Url.AbsolutePath;*/

            while (true)
            {

                bool switcer = true;
                string message = Console.ReadLine();
                /*var systemPath = System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);*/
                /*var complete = Path.Combine(systemPath, "files");*/
                if (message == "delete")
                {
                    File.Delete(path);
                    if (File.Exists(path))
                    {
                        Console.WriteLine("error\nTrying another time...");
                        File.Delete(path);
                        if (!File.Exists(path))
                        {
                            Console.WriteLine("error again, try another time");
                        }
                    }
                    else
                    {
                        Console.WriteLine("deleted");
                    }
                }
                if (message != "send" && message != "delete")
                {
                    
                    // This text is added only once to the file.
                    if (!File.Exists(path))
                    {
                        // Create a file to write to.
                        using (StreamWriter sw = File.CreateText(path))
                        {
                            sw.WriteLine($"{message}");
                        }
                    }

                    // This text is always added, making the file longer over time
                    // if it is not deleted.
                    else if (File.Exists(path))
                    {
                        using (StreamWriter sw = File.AppendText(path))
                        {
                            sw.WriteLine($"{message}");
                        }
                    }
                   

                    // Open the file to read from.
                    
                }



                /*Console.WriteLine(message);*/
                
                message = " ";

                /*byte[] output = Encoding.ASCII.GetBytes($"{message}\n");
                context.Response.ContentEncoding = Encoding.ASCII;
                context.Response.ContentLength64 = output.Length;
                context.Response.OutputStream.Write(output, 0, output.Length);*/


            }
            /*while (true)
            {
                string message = Console.ReadLine();
                Console.WriteLine(message);
                if (message != null)
                {
                    
                }
                
            }*/




        }
        public static void Request()
        {

            /*string path = 
            string urlAdd = "sosuxui";*/
            while (true)
            {
                Console.WriteLine("sending..\n");
                HttpListenerContext context = listener.GetContext();
                var contextPath = context.Request.Url.AbsolutePath;
                HttpListenerResponse response = context.Response;
                string clientIP = context.Request.RemoteEndPoint.ToString();
                Console.WriteLine(clientIP);
                var fileCodeName = contextPath.Remove(0, 1);
                Console.WriteLine(fileCodeName);
                if (fileCodeName == urlAdd)
                {
                    if (File.Exists(path))
                    {
                        using (StreamReader sr = File.OpenText(path))
                        {
                            string myResponse = "";
                            string s = "";
                            while ((s = sr.ReadLine()) != null)
                            {

                                myResponse += $"{s}\n";

                            }
                            context.Response.StatusCode = (int)HttpStatusCode.OK;
                            context.Response.OutputStream.Write(Encoding.UTF8.GetBytes($"{clientIP} - {myResponse}"));
                            context.Response.Close();
                            Console.WriteLine("sent");

                        }

                    }
                    else
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.OK;
                        context.Response.OutputStream.Write(Encoding.UTF8.GetBytes($"This is the start of the conversation"));
                        context.Response.Close();
                        Console.WriteLine("sent");
                    }
                }
            }
        }
    }  
}