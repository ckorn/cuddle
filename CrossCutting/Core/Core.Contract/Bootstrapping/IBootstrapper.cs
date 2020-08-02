using CrossCutting.Core.Contract.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.Core.Contract.Bootstrapping
{
    public interface IBootstrapper
    {
        void ActivatingAll();
        void ActivatedAll();
        void DeactivatedAll();
        void DeactivatingAll();
        void RegisterAllMappings(IKernel kernel);
    }
}
