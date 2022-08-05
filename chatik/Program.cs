
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
        public System.Net.CookieCollection Cookies { get; set; }
        static string path;
        static string urlAdd = "nechatik";
        static string urlHttp = "main";


        public static void Main()
        {

            
            Console.WriteLine("Current working directory: " + Directory.GetCurrentDirectory());
            listener = new HttpListener();
            listener.Prefixes.Add("http://*:81/");
            listener.IgnoreWriteExceptions = true;
            listener.Start();
            /*new Thread(HtmlPage).Start();*/
            new Thread(HttpRequestsListener).Start();
            
            
        }
        /*public static void HtmlPage()
        {
            listener = new HttpListener();
            string link = ("http://localhost:2002/");
            listener.Prefixes.Add($"{link}");
            listener.IgnoreWriteExceptions = true;
            listener.Start();
            while (true)
            {
                HttpListenerContext context = listener.GetContext();
                HttpListenerResponse response = context.Response;
                string htmlPage = File.ReadAllText("chatik.html");
                byte[] buffer = Encoding.UTF8.GetBytes(htmlPage);

                response.ContentLength64 = buffer.Length;
                Stream st = response.OutputStream;
                st.Write(buffer, 0, buffer.Length);

                context.Response.Close();
            }
        }*/
        private static void HttpRequestsListener()
        {
            
            Request();

            /*Console.WriteLine("Enter your nickname");*/
            /*path = Console.ReadLine() + ".txt";*/
            Console.WriteLine($"http://prompt.modeller.fvds.ru/" + urlAdd);
            /*LoginData loginData = new LoginData(path, urlAdd);*/

           /* if (path as string != null)
            {
                new Thread(Request).Start();
            }*/
            /*var context = listener.GetContext();
            var contextPath = context.Request.Url.AbsolutePath;*/

            while (true)
            {

                bool switcer = true;
                string message = Console.ReadLine();
                /*var systemPath = System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);*/
                /*var complete = Path.Combine(systemPath, "files");*/
                if (message == "change")
                {

                    path = Console.ReadLine() + ".txt";
                    urlAdd = Guid.NewGuid().ToString();
                    Console.WriteLine(urlAdd);

                }
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
                if (message != "send" && message != "delete" && message != "change")
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
                
                Console.WriteLine("Awaiting for a connection..\n");
                HttpListenerContext context = listener.GetContext();

                var contextPath = context.Request.Url.AbsolutePath;
                var request = context.Request;
                HttpListenerResponse response = context.Response;
                string clientIP = context.Request.RemoteEndPoint.ToString();
                var fileCodeName = contextPath.Remove(0, 1);
                
                string customerID = null;
                string user_name;
                bool cookies = false;
                Cookie cookie = request.Cookies["ID"];

                if (cookie == null || fileCodeName == "chatik")
                {
                    Console.WriteLine("Main page requested");
                    string htmlPage = File.ReadAllText("chatik.html");
                    byte[] buffer = Encoding.UTF8.GetBytes(htmlPage);

                    response.ContentLength64 = buffer.Length;
                    Stream st = response.OutputStream;
                    st.Write(buffer, 0, buffer.Length);

                    context.Response.Close();
                    
                }
                else
                {
                    using (var reader = new StreamReader(request.InputStream, request.ContentEncoding))

                    {
                        string text = reader.ReadToEnd().ToString();
                        var tokens = text.Split("&");
                        var formData = new Dictionary<string, string>();
                        for (var i = 0; i < tokens.Count(); i++)
                        {
                            if (tokens[i] == "")
                                break;
                            var group = tokens[i].ToString().Split("=");

                            var item = group[0].ToString();
                            var value = group[1].ToString();

                            if (!formData.ContainsKey(item))
                                formData.Add(item, value);
                        }

                        if (formData.TryGetValue("user_name", out string username))
                            user_name = formData["user_name"];
                        else
                        {
                            user_name = "";
                        }
                        if (formData.TryGetValue("cookies", out string cookies_switcher))
                        {
                            cookies = true;
                        }

                    }

                    if (cookies || user_name == "")
                    {
                        
                        if (cookie != null)
                        {
                            customerID = cookie.Value;
                        }
                        if (customerID != null)
                        {
                            Console.WriteLine("Found the cookie!");
                        }
                        if (customerID == null || (customerID.ToString() != user_name && user_name != ""))
                        {
                            customerID = user_name;
                            Cookie cook = new Cookie("ID", customerID);
                            response.AppendCookie(cook);

                        }
                        path = $"{customerID}.txt";
                    }
                    else
                    {
                        path = $"{user_name}.txt";
                    }


                    /*urlAdd = Guid.NewGuid().ToString();
                    fileLinks.TryAdd(urlAdd, path);*/
                    /*if (user_name == "chatik")
                    {
                        string htmlPage = File.ReadAllText("chatik.html");
                        byte[] buffer = Encoding.UTF8.GetBytes(htmlPage);

                        response.ContentLength64 = buffer.Length;
                        Stream st = response.OutputStream;
                        st.Write(buffer, 0, buffer.Length);

                        context.Response.Close();
                    }*/
                    if (fileCodeName != "favicon.ico")
                    {
                        Console.WriteLine(clientIP);
                        Console.WriteLine(fileCodeName);
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
                                Console.WriteLine("Connection established");

                            }

                        }
                        else
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.OK;
                            context.Response.OutputStream.Write(Encoding.UTF8.GetBytes($"<html><head><meta charset='utf8'></head><body>This is the start of conversation</body></html>"));
                            context.Response.Close();
                            Console.WriteLine("sent");
                        }
                    }
                }
                
                
            }
        }
    }  
}