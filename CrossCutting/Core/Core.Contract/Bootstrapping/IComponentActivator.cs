using CrossCutting.Core.Contract.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.Core.Contract.Bootstrapping
{
    public interface IComponentActivator
    {
        void Activating();
        void Activated();
        void Deactivating();
        void Deactivated();
        void RegisterMappings(IKernel kernel);
    }
}
