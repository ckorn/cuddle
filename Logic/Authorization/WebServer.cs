using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Logic.Authorization
{
    internal class WebServer
    {
        public event EventHandler<UrlAccessedParams> UrlAccessed;

        private Thread _serverThread;
        private HttpListener _listener;
        private int _port;

        public WebServer(int port) 
        {
            _port = port;
            this.Initialize();
        }

        public void Stop()
        {
            _serverThread.Abort();
            _listener.Stop();
        }

        private void Listen()
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add($"http://*:{_port}/");
            _listener.Start();
            while (true)
            {
                try
                {
                    HttpListenerContext context = _listener.GetContext();
                    Process(context);
                }
                catch
                {

                }
            }
        }

        private void OnUrlAccessed(string url, HttpListenerContext context) 
        {
             this.UrlAccessed?.Invoke(this, new UrlAccessedParams(url, context));
        }

        private void Process(HttpListenerContext context)
        {
            OnUrlAccessed(context.Request.Url.AbsoluteUri, context);

            context.Response.StatusCode = (int)HttpStatusCode.OK;
        }

        private void Initialize()
        {
            _serverThread = new Thread(this.Listen);
            _serverThread.Start();
        }
    }
}
