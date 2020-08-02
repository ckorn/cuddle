using Logic.HttpClient.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Logic.HttpClient
{
    internal class Client : IHttpClient
    {
        public byte[] DownloadData(string url) 
        {
            byte[] response = new WebClient().DownloadData(url);
            return response;
        }
    }
}
