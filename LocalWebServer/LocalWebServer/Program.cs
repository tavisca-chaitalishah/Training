using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace LocalWebServer
{
    class Program
    {
        public static void Main()
        {
            Console.WriteLine("Starting server at 8080");
            HttpServer server = new HttpServer(8080);
            server.Start();
            Console.ReadLine();

        }

    }
}