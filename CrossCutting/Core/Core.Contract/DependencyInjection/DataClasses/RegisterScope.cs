using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.Core.Contract.DependencyInjection.DataClasses
{
    public enum RegisterScope
    {
        PerInject,
        PerContext,
        Unique
    }
}
