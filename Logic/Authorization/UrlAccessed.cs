using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Authorization
{
    internal class UrlAccessedParams : EventArgs
    {
        public string Url { get; }
        public HttpListenerContext HttpListenerContext { get; }

        public UrlAccessedParams(string url, HttpListenerContext httpListenerContext)
        {
            Url = url;
            HttpListenerContext = httpListenerContext;
        }
    }
}
