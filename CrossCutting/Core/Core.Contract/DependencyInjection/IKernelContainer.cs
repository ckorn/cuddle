using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.Core.Contract.DependencyInjection
{
    public interface IKernelContainer
    {
        IKernel Kernel { get; }
    }
}
