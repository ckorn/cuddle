using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.Core.Contract.DependencyInjection.DataClasses
{
    public class ConstructorParameter
    {
        public string Name { get; }
        public object Value { get; }

        public ConstructorParameter(string name, object value)
        {
            Name = name;
            Value = value;
        }
    }
}
