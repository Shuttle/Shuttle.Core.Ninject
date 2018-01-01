using Ninject;
using NUnit.Framework;
using Shuttle.Core.Container.Tests;

namespace Shuttle.Core.Ninject.Tests
{
    [TestFixture]
    public class Fixture : ContainerFixture
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

		[Test]
		public void Should_be_able_to_register_and_resolve_a_multiple_singleton()
		{
			var container = new NinjectComponentContainer(new StandardKernel());

			RegisterMultipleSingleton(container);
			ResolveMultipleSingleton(container);
		}

		[Test]
		public void Should_be_able_to_register_and_resolve_multiple_transient_components()
		{
			var container = new NinjectComponentContainer(new StandardKernel());

			RegisterMultipleTransient(container);
			ResolveMultipleTransient(container);
		}
	}
}