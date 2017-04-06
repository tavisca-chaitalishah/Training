using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalWebServer
{
    public class Request
    {
        public string Type { get; set; }
        public string URL { get; set;}
        public string Host { get; set; }
        public string Referer { get; set; }
        public Request( string Type, string URL, string Host , string Referer)
        {
            this.Type = Type;
            this.URL = URL;
            this.Host = Host;
            this.Referer = Referer;
        }
        public static Request GetRequest(string request)
        {
            if (String.IsNullOrEmpty(request)) return null;
            String[] tokens = request.Split(' ', '\n');
            string type = tokens[0];
            string url = tokens[1];
            string host = tokens[4];
            string referer = "";
 for(int i =0; i< tokens.Length; i++)
            {
                if(tokens[i] == "refer:")
                {
                    referer = tokens[i + 1];
                    break;
                }
            }
            return new Request(type, url, host,referer);
        }
    }
}
