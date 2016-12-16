﻿using System;
using System.Collections.Generic;
using Ninject;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Core.Ninject
{
    public class NinjectComponentContainer : IComponentRegistry, IComponentResolver
    {
        private readonly StandardKernel _container;

        public NinjectComponentContainer(StandardKernel container)
        {
            Guard.AgainstNull(container, "container");

            _container = container;
        }

        public IComponentRegistry Register(Type serviceType, Type implementationType, Lifestyle lifestyle)
        {
            Guard.AgainstNull(serviceType, "serviceType");
            Guard.AgainstNull(implementationType, "implementationType");

            try
            {
                switch (lifestyle)
                {
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

        public IComponentRegistry Register(string name, Type serviceType, Type implementationType, Lifestyle lifestyle)
        {
            Guard.AgainstNullOrEmptyString(name, "name");
            Guard.AgainstNull(serviceType, "serviceType");
            Guard.AgainstNull(implementationType, "implementationType");

            try
            {
                switch (lifestyle)
                {
                    case Lifestyle.Transient:
                    {
                        _container.Bind(serviceType).To(implementationType).InTransientScope().Named(name);

                        break;
                    }
                    default:
                    {
                        _container.Bind(serviceType).To(implementationType).InSingletonScope().Named(name);

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

        public IComponentRegistry Register(Type serviceType, object instance)
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

        public object Resolve(string name, Type serviceType)
        {
            Guard.AgainstNullOrEmptyString(name, "name");
            Guard.AgainstNull(serviceType, "serviceType");

            try
            {
                return _container.Get(serviceType, name);
            }
            catch (Exception ex)
            {
                throw new TypeResolutionException(ex.Message, ex);
            }
        }

        public IEnumerable<object> ResolveAll(Type serviceType)
        {
            Guard.AgainstNull(serviceType, "serviceType");

            try
            {
                return _container.GetAll(serviceType);
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