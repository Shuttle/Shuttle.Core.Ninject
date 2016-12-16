using Ninject;
using NUnit.Framework;
using Shuttle.Core.ComponentContainer.Tests;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Core.Ninject.Tests
{
    [TestFixture]
    public class NinjectComponentContainerFixture : ComponentContainerFixture
    {
        [Test]
        public void Should_be_able_to_register_and_resolve_a_singleton()
        {
            var container = new NinjectComponentContainer(new StandardKernel());

            RegisterSingleton(container);
            ResolveSingleton(container);
        }

        [Test]
        public void Should_be_able_to_register_and_resolve_transient_components()
        {
            var container = new NinjectComponentContainer(new StandardKernel());

            RegisterTransient(container);
            ResolveTransient(container);
        }

        [Test]
        public void Should_be_able_to_register_and_resolve_a_named_singleton()
        {
            var container = new NinjectComponentContainer(new StandardKernel());

            RegisterNamedSingleton(container);
            ResolveNamedSingleton(container);
        }

        [Test]
        public void Should_be_able_to_register_and_resolve_named_transient_components()
        {
            var container = new NinjectComponentContainer(new StandardKernel());

            RegisterNamedTransient(container);
            ResolveNamedTransient(container);
        }

        [Test]
        public void Should_be_able_resolve_all_instances()
        {
            var container = new NinjectComponentContainer(new StandardKernel());

            RegisterMultipleInstances(container);
            ResolveMultipleInstances(container);
        }
    }
}