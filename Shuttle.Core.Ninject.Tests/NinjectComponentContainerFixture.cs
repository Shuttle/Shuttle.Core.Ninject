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
        public void Should_be_able_resolve_all_instances()
        {
            var container = new NinjectComponentContainer(new StandardKernel());

            RegisterCollection(container);
            ResolveCollection(container);
        }
    }
}