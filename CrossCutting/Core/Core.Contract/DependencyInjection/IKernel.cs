using CrossCutting.Core.Contract.Bootstrapping;
using CrossCutting.Core.Contract.DependencyInjection.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.Core.Contract.DependencyInjection
{
    public interface IKernel
    {
        void Register<TContract, TImplementation>(RegisterScope scope = RegisterScope.PerInject)
            where TImplementation : TContract;

        void Register(Type contract, Type implementation, RegisterScope scope = RegisterScope.PerInject);

        void RegisterToSelf<TImplementation>(RegisterScope scope = RegisterScope.PerInject);

        void RegisterComponent<TComponent>()
            where TComponent : IComponentActivator;

        TContract Get<TContract>();
        TContract Get<TContract>(params ConstructorParameter[] parameters);

        object Get(Type contractType);
        object Get(Type contractType, params ConstructorParameter[] parameters);
    }
}
