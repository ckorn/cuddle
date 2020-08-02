using CrossCutting.Core.Contract.Bootstrapping;
using CrossCutting.Core.Contract.DependencyInjection;
using Logic.HttpClient.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.HttpClient
{
    public class HttpClientActivator : IComponentActivator
    {
        public void Activated()
        {
            
        }

        public void Activating()
        {
            
        }

        public void Deactivated()
        {
            
        }

        public void Deactivating()
        {
            
        }

        public void RegisterMappings(IKernel kernel)
        {
            kernel.Register<IHttpClient, Client>();
        }
    }
}
