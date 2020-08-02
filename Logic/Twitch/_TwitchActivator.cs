using CrossCutting.Core.Contract.Bootstrapping;
using CrossCutting.Core.Contract.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitch.Contracts;

namespace Logic.Twitch
{
    public class TwitchActivator : IComponentActivator
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
            kernel.Register<IBot, Bot>();
        }
    }
}
