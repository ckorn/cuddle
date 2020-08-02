using CrossCutting.Core.Contract.Bootstrapping;
using CrossCutting.Core.Contract.DependencyInjection;
using CrossCutting.Logging.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.Logging
{
    public class LoggingActivator : IComponentActivator
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
            kernel.Register<ILogger, Logger>();
        }
    }
}
