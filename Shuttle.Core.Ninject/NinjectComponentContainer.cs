using System;
using System.Linq;
using Ninject;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Core.Ninject
{
    public class NinjectComponentContainer : IComponentContainer
    {
        private readonly StandardKernel _container;

        public NinjectComponentContainer(StandardKernel container)
        {
            Guard.AgainstNull(container, "container");

            _container = container;
        }

        public object Resolve(Type serviceType)
        {
            Guard.AgainstNull(serviceType, "serviceType");

            try
            {
                return _container.Get(serviceType);
            }
            catch (Exception ex)
            {
                throw new TypeResolutionException(ex.Message, ex);
            }
        }

        public T Resolve<T>() where T : class
        {
            return (T)Resolve(typeof(T));
        }

        public IComponentContainer Register(Type serviceType, Type implementationType, Lifestyle lifestyle)
        {
            Guard.AgainstNull(serviceType, "serviceType");
            Guard.AgainstNull(implementationType, "implementationType");

            try
            {
                switch (lifestyle)
                {
                    case Lifestyle.Thread:
                        {
                            _container.Bind(serviceType).To(implementationType).InThreadScope();

                            break;
                        }
                    case Lifestyle.Transient:
                        {
                            _container.Bind(serviceType).To(implementationType).InTransientScope();

                            break;
                        }
                    default:
                        {
                            _container.Bind(serviceType).To(implementationType).InSingletonScope();

                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                throw new TypeRegistrationException(ex.Message, ex);
            }
            return this;
        }

        public IComponentContainer Register(Type serviceType, object instance)
        {
            Guard.AgainstNull(serviceType, "serviceType");
            Guard.AgainstNull(instance, "instance");

            try
            {
                _container.Bind(serviceType).ToConstant(instance);
            }
            catch (Exception ex)
            {
                throw new TypeRegistrationException(ex.Message, ex);
            }

            return this;
        }

        public bool IsRegistered(Type serviceType)
        {
            Guard.AgainstNull(serviceType, "serviceType");

            try
            {
                var instance = _container.Get(serviceType);

                return true;
            }
            catch 
            {
                return false;
            }
        }
    }
}