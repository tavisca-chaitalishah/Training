using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace LocalWebServer
{
    class Response
    {
        private Byte[] data = null;
        private string status;
        private string mine;

        public Response(string Status, string mine, Byte[] data)
        {
            this.data = data;
            this.status = Status;
            this.mine = mine;
        }
        public static Response From(Request request)
        {
            if (request == null)
                return MakeNullRequest();

            if (request.Type == "GET")
            {
                string file = Environment.CurrentDirectory + HttpServer.WEB_DIR + request.URL;
                FileInfo f = new FileInfo(file);
                if (f.Exists && f.Extension.Contains("."))
                {
                    return MakeFromFile(f);
                }
                else
                {
                    DirectoryInfo di = new DirectoryInfo(f + "/");
                    if (!di.Exists)
                        return MakePageNotFound();
                    FileInfo[] files = di.GetFiles();
                    foreach (FileInfo ff in files)
                    {
                        string n = ff.Name;
                        if (n.Contains("default.html") || n.Contains("default.htm") || n.Contains("index.htm") || n.Contains("index.html"))

                            f = ff;
                        return MakeFromFile(ff);
                    }
                }
                if (!f.Exists)
                {
                    return MakePageNotFound();
                }

            }
            else
            {
                return MakeMethodNotAllowed();
            }
            return MakePageNotFound();
        }

        private static Response MakeFromFile(FileInfo f)
        {
            FileStream fs = f.OpenRead();
            BinaryReader reader = new BinaryReader(fs);
            Byte[] d = new Byte[fs.Length];
            reader.Read(d, 0, d.Length);
            fs.Close();
            return new Response("200 Ok ", "html/text", d);
        }

        private static Response MakeNullRequest()
        {
            string file = Environment.CurrentDirectory + HttpServer.MSG_Dir + "/400.html";
            FileInfo fi = new FileInfo(file);
            FileStream fs = fi.OpenRead();
            BinaryReader reader = new BinaryReader(fs);
            Byte[] d = new Byte[fs.Length];
            reader.Read(d, 0, d.Length);
            fs.Close();
            return new Response("400 Bad Request ", "html/text", d);
        }
        private static Response MakeMethodNotAllowed()
        {
            string file = Environment.CurrentDirectory + HttpServer.MSG_Dir + "405.html";
            FileInfo fi = new FileInfo(file);
            FileStream fs = fi.OpenRead();
            BinaryReader reader = new BinaryReader(fs);
            Byte[] d = new Byte[fs.Length];
            reader.Read(d, 0, d.Length);
            fs.Close();
            return new Response("400 Bad Request ", "html/text", d);
        }
        private static Response MakePageNotFound()
        {
            string file = Environment.CurrentDirectory + HttpServer.MSG_Dir + "/404.html";
            FileInfo fi = new FileInfo(file);
            FileStream fs = fi.OpenRead();
            BinaryReader reader = new BinaryReader(fs);
            Byte[] d = new Byte[fs.Length];
            reader.Read(d, 0, d.Length);
            fs.Close();
            return new Response("404 Page Not Found ", "html/text", d);
        }

        public void Post(NetworkStream stream)
        {
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine(string.Format("", HttpServer.Version, HttpServer.Name, mine, data.Length));
            stream.Write(data, 0, data.Length);
        }
    }
}
