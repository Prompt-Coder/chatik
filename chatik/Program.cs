
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace chatik
{

    class ChatMessage
    {
        public static HttpListener listener;
        public static ConcurrentDictionary<string, string> fileLinks = new ConcurrentDictionary<string, string>();
        public System.Net.CookieCollection Cookies { get; set; }
        static string path;
        static string urlAdd = "nechatik";
        static string urlHttp = "main";
        public static Mysql mainContext;


        public static void Main()
        {
            mainContext = new Mysql();
            Console.WriteLine("Current working directory: " + Directory.GetCurrentDirectory());
            var sw = Stopwatch.StartNew();
            var currentModel = new ChatikUsers {User = "test2", Password = "1234", Cookies = "test", Chats = "0" };
            /*mainContext.Users.Add(currentModel);*/
            var data = mainContext.Users.ToList();
            for (int i = 0; i < data.Count; i++)
            {
                Console.WriteLine($"Id = {data[i].Id} User = {data[i].User} Pass = {data[i].Password} Cookies = {data[i].Cookies} Chats = {data[i].Chats}");
                
            }
            

            Console.WriteLine(sw.ElapsedMilliseconds);
            mainContext.SaveChanges();
            listener = new HttpListener();
            listener.Prefixes.Add("http://*:81/");
            listener.IgnoreWriteExceptions = true;
            listener.Start();
            new Thread(Request).Start();
            
            
        }
        
        /*private static void HttpRequestsListener()
        {
            
            Request();


            while (true)
            {

                bool switcer = true;
                string message = Console.ReadLine();
                *//*var systemPath = System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);*/
                /*var complete = Path.Combine(systemPath, "files");*//*
                if (message == "change")
                {

                    path = Console.ReadLine() + ".txt";
                    urlAdd = Guid.NewGuid().ToString();
                    Console.WriteLine(urlAdd);

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



                *//*Console.WriteLine(message);*//*
                
                message = " ";

                *//*byte[] output = Encoding.ASCII.GetBytes($"{message}\n");
                context.Response.ContentEncoding = Encoding.ASCII;
                context.Response.ContentLength64 = output.Length;
                context.Response.OutputStream.Write(output, 0, output.Length);*//*


            }
            *//*while (true)
            {
                string message = Console.ReadLine();
                Console.WriteLine(message);
                if (message != null)
                {
                    
                }
                
            }*//*




        }*/
        public static void Request()
        {
            var encoded = File.ReadAllText("main.html");
            string firstPartOfHtml = "<!DOCTYPE html >\r\n\r\n<script charset=\"UTF - 8\"></script> \r\n<head>\r\n    <meta charset=\"utf-8\" />\r\n    <title></title>\r\n</head>\r\n<body>\r\n    <form action=\" http://127.0.0.1:81/message\" method =\"post\">\r\n        <ul>\r\n";
            string secondPartOfHtml = "\r\n    </form>\r\n\r\n    <style>\r\n        form {\r\n            /* Center the form on the page */\r\n            margin: 0 auto;\r\n            width: 400px;\r\n            /* Form outline */\r\n            padding: 1em;\r\n            border: 1px solid #CCC;\r\n            border-radius: 1em;\r\n        }\r\n\r\n        ul {\r\n            list-style: none;\r\n            padding: 0;\r\n            margin: 0;\r\n        }\r\n\r\n        form li + li {\r\n            margin-top: 1em;\r\n        }\r\n\r\n        label {\r\n            /* Uniform size & alignment */\r\n            display: inline-block;\r\n            width: 90px;\r\n            text-align: right;\r\n        }\r\n\r\n        input,\r\n        textarea {\r\n            /* To make sure that all text fields have the same font settings\r\n     By default, textareas have a monospace font */\r\n            font: 1em sans-serif;\r\n            /* Uniform text field size */\r\n            width: 300px;\r\n            box-sizing: border-box;\r\n            /* Match form field borders */\r\n            border: 1px solid #999;\r\n        }\r\n\r\n            input:focus,\r\n            textarea:focus {\r\n                /* Additional highlight for focused elements */\r\n                border-color: #000;\r\n            }\r\n\r\n        textarea {\r\n            /* Align multiline text fields with their labels */\r\n            vertical-align: top;\r\n            /* Provide space to type some text */\r\n            height: 5em;\r\n        }\r\n\r\n        .button {\r\n            /* Align buttons with the text fields */\r\n            padding-left: 90px; /* same size as the label elements */\r\n        }\r\n\r\n        button {\r\n            /* This extra margin represent roughly the same space as the space\r\n     between the labels and their text fields */\r\n            margin-left: .5em;\r\n        }\r\n    </style>\r\n</body>\r\n</html>";

            string previousClientIp = null;
            /*string path = 
            string urlAdd = "sosuxui";*/
            var cookieGuid = new Dictionary<string, string>();
            while (true)
            {
                
                
                HttpListenerContext context = listener.GetContext();

                var contextPath = context.Request.Url.AbsolutePath;
                var request = context.Request;
                HttpListenerResponse response = context.Response;
                
                string clientIP = context.Request.RemoteEndPoint.ToString();
                var fileCodeName = contextPath.Remove(0, 1);



                if (fileCodeName != "favicon.ico")
                {
                    string customerID = null;
                    string user_name;
                    string password = "";
                    bool cookies = true;
                    bool isDeleted = false;
                    string myCustomer;
                    bool registring = false;
                    bool access = false;
                    ChatikUsers currentClient = null;
                    Cookie cookie = request.Cookies["ID"];
                    Console.WriteLine("Awaiting for a connection..\n");
                    if (cookie != null)
                    {
                        myCustomer = cookie.Value;
                    }
                    else
                    {
                        myCustomer = null;
                    }
                    if (fileCodeName == "chatik")
                    {
                        Console.WriteLine("Main page requested");
                        string htmlPage = File.ReadAllText("chatik.html");
                        byte[] buffer = Encoding.UTF8.GetBytes(htmlPage);

                        response.ContentLength64 = buffer.Length;
                        Stream st = response.OutputStream;
                        st.Write(buffer, 0, buffer.Length);
                        /*customerID = "";
                        Cookie cook = new Cookie("ID", customerID);
                        response.AppendCookie(cook);*/
                        context.Response.Close();

                    }
                    if (fileCodeName == "message")
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
                            if (formData.TryGetValue("password", out string newPassword))
                                password = formData["password"];

                            if (formData.TryGetValue("register", out string registration))
                            {
                                registring = true;
                            }



                        }

                        if (cookies /*|| user_name == ""*/)
                        {

                            if (cookie != null)
                            {
                                customerID = cookie.Value;
                            }
                            if (customerID != null)
                            {
                                Console.WriteLine("Found the cookie!");
                            }
                            if (customerID == null /*|| (customerID.ToString() != user_name && user_name != "")*/)
                            {

                                customerID = Guid.NewGuid().ToString();
                                Cookie cook = new Cookie("ID", customerID);
                                response.AppendCookie(cook);

                            }
                            path = $"{customerID}.txt";
                        }
                        else
                        {
                            path = $"{user_name}.txt";
                        }
                        if (registring)
                        {

                            var currentUser = new ChatikUsers { User = user_name, Password = password, Cookies = customerID, Chats = "0" };
                            currentClient = currentUser;
                            mainContext.Users.Add(currentUser);
                            try
                            {
                                mainContext.SaveChanges();
                            }
                            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
                            {
                                context.Response.StatusCode = (int)HttpStatusCode.OK;
                                context.Response.OutputStream.Write(Encoding.UTF8.GetBytes($"{firstPartOfHtml}<br> Such user already exists </br>{secondPartOfHtml}"));
                                context.Response.Close();
                            }

                        }
                        else
                        {
                            bool isFound = false;
                            foreach (var item in mainContext.Users.ToList())
                            {
                                if (item.User == user_name)
                                {
                                    isFound = true;
                                    if (item.Password == password)
                                    {
                                        access = true;
                                        currentClient = item;
                                    }

                                    else
                                    {
                                        context.Response.StatusCode = (int)HttpStatusCode.OK;
                                        context.Response.OutputStream.Write(Encoding.UTF8.GetBytes($"{firstPartOfHtml}<br>Wrong password</br>{secondPartOfHtml}"));
                                        context.Response.Close();
                                    }
                                    break;
                                }
                            }
                            if (!isFound)
                            {
                                context.Response.StatusCode = (int)HttpStatusCode.OK;
                                context.Response.OutputStream.Write(Encoding.UTF8.GetBytes($"{firstPartOfHtml}<br>This user does not exist. Better register it now!</br>{secondPartOfHtml}"));
                                context.Response.Close();
                            }

                        }

                        if (fileCodeName != "favicon.ico" && access)
                        {
                            Console.WriteLine(clientIP);
                            Console.WriteLine(fileCodeName);
                            var availableChats = currentClient.Chats;
                            var sortedChats = availableChats.ToString().Split("+").ToList();
                            sortedChats.RemoveAt(sortedChats.Count - 1);
                            string myResponse = "";
                            if (availableChats[0] != '0')
                            {
                                foreach (var chat in sortedChats)
                                {
                                    myResponse += $"<li class=\"button\">\r\n                <button type=\"submit\" name = \"{chat}\">{chat}</button>\r\n            </li>\r\n  ";
                                }
                            }

                            context.Response.StatusCode = (int)HttpStatusCode.OK;
                            var differentLink = firstPartOfHtml.Replace("message", "chat");
                            context.Response.OutputStream.Write(Encoding.UTF8.GetBytes($"{differentLink}{myResponse}{secondPartOfHtml}"));
                            context.Response.Close();
                            Console.WriteLine("Connection established");


                            /* if (File.Exists(path))
                             {
                                 using (StreamReader sr = File.OpenText(path))
                                 {
                                     string myResponse = "";
                                     string s = "";
                                     while ((s = sr.ReadLine()) != null)
                                     {

                                         myResponse += $"<p>{s}</p>\n\r";

                                     }
                                     context.Response.StatusCode = (int)HttpStatusCode.OK;
                                     context.Response.OutputStream.Write(Encoding.UTF8.GetBytes($"{firstPartOfHtml}            <li class=\"input_message\">\r\n                <input type=\"text\" id=\"name\" name=\"message\" placeholder=\"write message\" />\r\n            </li>\r\n            <li class=\"button\">\r\n                <button type=\"submit\">Send your message</button>\r\n            </li>\r\n        </ul>\r\n        <p>Chat: {path}\n</p>{myResponse}{secondPartOfHtml}"));
                                     context.Response.Close();
                                     Console.WriteLine("Connection established");

                                 }

                             }
                             else
                             {
                                 if (!File.Exists(path))
                                 {
                                     // Create a file to write to.
                                     using (StreamWriter sw = File.CreateText(path))
                                     {
                                         sw.WriteLine($"test");
                                     }
                                 }
                                 context.Response.StatusCode = (int)HttpStatusCode.OK;
                                 context.Response.OutputStream.Write(Encoding.UTF8.GetBytes($"{firstPartOfHtml}<p>Chat: {path}\n</p>{secondPartOfHtml}"));
                                 context.Response.Close();
                                 Console.WriteLine("Connection established");

                             }*/
                        }
                    }
                    if (fileCodeName == "chat")
                    {
                        using (var reader = new StreamReader(request.InputStream, request.ContentEncoding))

                        {
                            string text = reader.ReadToEnd().ToString();
                            var tokens = text.Split("=");
                            if (tokens[1] != "")
                            {
                                var myChat = tokens[0];
                                var chats = mainContext.Chats.ToList();
                                var entity = mainContext.Chats.FirstOrDefault(item => item.ChatName == "firstchat+");
                                foreach (var chat in chats)
                                {
                                    if (chat.ChatName == myChat)
                                    {
                                        
                                        var messages = chat.Messages;
                                        var differentLink = firstPartOfHtml.Replace("message", "chat");
                                        context.Response.StatusCode = (int)HttpStatusCode.OK;
                                        context.Response.OutputStream.Write(Encoding.UTF8.GetBytes($"{differentLink}            <li class=\"input_message\">\r\n                <input type=\"text\" id=\"name\" name=\"{myChat}\" placeholder=\"write message\" />\r\n            </li>\r\n            <li class=\"button\">\r\n                <button type=\"submit\">Send your message</button>\r\n            </li>\r\n        </ul>\r\n        <p>Chat: {path}\n</p>{messages}{secondPartOfHtml}"));
                                        context.Response.Close();
                                        Console.WriteLine("Connection established");
                                        break;
                                    }
                                }
                            }
                                /*if (formData.TryGetValue("message", out string userMessages)) { 
                                }*/
                            
                            else
                            {
                                var myChat = text.Replace("=", "+");
                                var chats = mainContext.Chats.ToList();
                                foreach (var chat in chats)
                                {
                                    if (chat.ChatName == myChat)
                                    {
                                        var messages = chat.Messages;
                                        var differentLink = firstPartOfHtml.Replace("message", "chat");
                                        context.Response.StatusCode = (int)HttpStatusCode.OK;
                                        context.Response.OutputStream.Write(Encoding.UTF8.GetBytes($"{differentLink}            <li class=\"input_message\">\r\n                <input type=\"text\" id=\"name\" name=\"{myChat}\" placeholder=\"write message\" />\r\n            </li>\r\n            <li class=\"button\">\r\n                <button type=\"submit\">Send your message</button>\r\n            </li>\r\n        </ul>\r\n        <p>Chat: {path}\n</p>{messages}{secondPartOfHtml}"));
                                        context.Response.Close();
                                        Console.WriteLine("Connection established");
                                        break;
                                    }
                                }
                            }
                            
                            

                        }
                    }
                }
            }
        }
        public class Mysql : DbContext
        {
            public DbSet<ChatikUsers> Users { get; set; }
            public DbSet<ChatikChats> Chats { get; set; }
            public Mysql()
            {
                Database.EnsureCreated();
            }


            protected override void OnConfiguring(DbContextOptionsBuilder optBuilder)
            {
                MySqlConnectionStringBuilder csb = new MySqlConnectionStringBuilder();
                csb.Database = "chatik_data";
                csb.Server = "92.63.103.70";
                csb.UserID = "discord";
                csb.Password = "PI5oW7t5YE";
                optBuilder.UseMySQL(csb.ToString());
            }
        }
    }  
}