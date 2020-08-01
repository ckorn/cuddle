using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.HttpClient.Contracts
{
    public interface IHttpClient
    {
        byte[] DownloadData(string url);
    }
}
