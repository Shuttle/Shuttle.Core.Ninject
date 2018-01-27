using System;
using System.Collections.Generic;
using Ninject;
using Shuttle.Core.Container;
using Shuttle.Core.Contract;

namespace Shuttle.Core.Ninject
{
    public class NinjectComponentContainer : ComponentRegistry, IComponentResolver
    {
        private readonly StandardKernel _container;

        public NinjectComponentContainer(StandardKernel container)
        {
            Guard.AgainstNull(container, "container");

            _container = container;
        }

        public override IComponentRegistry Register(Type dependencyType, Type implementationType, Lifestyle lifestyle)
        {
            Guard.AgainstNull(dependencyType, "dependencyType");
            Guard.AgainstNull(implementationType, "implementationType");

	        base.Register(dependencyType, implementationType, lifestyle);

            try
            {
                switch (lifestyle)
                {
                    case Lifestyle.Transient:
                    {
                        _container.Bind(dependencyType).To(implementationType).InTransientScope();

                        break;
                    }
                    default:
                    {
                        _container.Bind(dependencyType).To(implementationType).InSingletonScope();

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

        public override IComponentRegistry RegisterCollection(Type dependencyType, IEnumerable<Type> implementationTypes, Lifestyle lifestyle)
        {
            Guard.AgainstNull(dependencyType, "dependencyType");
            Guard.AgainstNull(implementationTypes, "implementationTypes");

	        base.RegisterCollection(dependencyType, implementationTypes, lifestyle);

            try
            {
                switch (lifestyle)
                {
                    case Lifestyle.Transient:
                        {
                            foreach (var implementationType in implementationTypes)
                            {
                                _container.Bind(dependencyType).To(implementationType).InTransientScope();
                            }

                            break;
                        }
                    default:
                        {
                            foreach (var implementationType in implementationTypes)
                            {
                                _container.Bind(dependencyType).To(implementationType).InSingletonScope();
                            }

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

        public override IComponentRegistry RegisterInstance(Type dependencyType, object instance)
        {
            Guard.AgainstNull(dependencyType, "dependencyType");
            Guard.AgainstNull(instance, "instance");

	        base.RegisterInstance(dependencyType, instance);

            try
            {
                _container.Bind(dependencyType).ToConstant(instance);
            }
            catch (Exception ex)
            {
                throw new TypeRegistrationException(ex.Message, ex);
            }

            return this;
        }

        public override IComponentRegistry RegisterOpen(Type dependencyType, Type implementationType, Lifestyle lifestyle)
        {
            return Register(dependencyType, implementationType, lifestyle);
        }

        public object Resolve(Type dependencyType)
        {
            Guard.AgainstNull(dependencyType, "dependencyType");

            try
            {
                return _container.Get(dependencyType);
            }
            catch (Exception ex)
            {
                throw new TypeResolutionException(ex.Message, ex);
            }
        }

        public IEnumerable<object> ResolveAll(Type dependencyType)
        {
            Guard.AgainstNull(dependencyType, "dependencyType");

            try
            {
                return _container.GetAll(dependencyType);
            }
            catch (Exception ex)
            {
                throw new TypeResolutionException(ex.Message, ex);
            }
        }

        public T Resolve<T>() where T : class
        {
            return (T) Resolve(typeof (T));
        }
    }
}