using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LocalWebServer
{


    public class HttpServer
    {
        public const string MSG_Dir = "/root/msg";
        public const string WEB_DIR = "/root/web";
        public bool running = false;
        public TcpListener listener;
        public const string Version = "Http/1.1";
        public const string Name = "HTTP Server v 0.1.1";

        public HttpServer(int port)
        {
            listener = new TcpListener(IPAddress.Any, port);
        }
        public void Start()
        {
            Thread startServer = new Thread(new ThreadStart(Run));
            startServer.Start();
        }

        public void Run()
        {
            running = true;

            listener.Start();
            while (running)
            {
                Console.WriteLine("Waiting for connections");
                TcpClient client = listener.AcceptTcpClient();
                Console.WriteLine("Client Connected");
                handleClient(client);
                client.Close();
            }
            running = false;
            listener.Stop();

        }

        public void handleClient(TcpClient client)
        {
            StreamReader Reader = new StreamReader(client.GetStream());
            string msg = "";
            while (Reader.Peek() != -1)
            {
                msg += Reader.ReadLine() + "\n";
            }
            Debug.WriteLine("Request : \n" + msg);
            Request req = Request.GetRequest(msg);
            Response resp = Response.From(req);
            resp.Post(client.GetStream());
        }
    }
}
