using CrossCutting.Core.Contract.Bootstrapping;
using CrossCutting.Core.Contract.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.Core.Bootstrapping
{
    public class Bootstrapper : IBootstrapper
    {
        private readonly List<IComponentActivator> _components;

        public Bootstrapper(IComponentActivator[] components)
        {
            _components = components.ToList();
        }

        public void ActivatingAll() => _components.ForEach(ca => ca.Activating());
        public void ActivatedAll() => _components.ForEach(ca => ca.Activated());
        public void DeactivatedAll() => _components.ForEach(ca => ca.Deactivated());
        public void DeactivatingAll() => _components.ForEach(ca => ca.Deactivating());
        public void RegisterAllMappings(IKernel kernel) => _components.ForEach(ca => ca.RegisterMappings(kernel));
    }
}
