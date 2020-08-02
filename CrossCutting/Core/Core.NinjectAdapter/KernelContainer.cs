using CrossCutting.Core.Contract.DependencyInjection;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.Core.NinjectAdapter
{
    public class KernelContainer : IKernelContainer
    {
        private static Contract.DependencyInjection.IKernel _innerKernel;
        private static readonly object _lock = new object();

        public Contract.DependencyInjection.IKernel Kernel
        {
            get
            {
                lock (_lock)
                {
                    if (_innerKernel == null)
                    {
                        _innerKernel = new KernelAdapter(new StandardKernel());
                        _innerKernel.Register<Contract.DependencyInjection.IKernel, KernelAdapter>();
                    }

                    return _innerKernel;
                }
            }
        }
    }
}
