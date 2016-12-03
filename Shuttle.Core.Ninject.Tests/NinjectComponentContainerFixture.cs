using Ninject;
using NUnit.Framework;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Core.Ninject.Tests
{
    [TestFixture]
    public class NinjectComponentContainerFixture
    {
        [Test]
        public void Should_be_able_to_register_and_resolve_a_type()
        {
            var container = new NinjectComponentContainer(new StandardKernel());
            var serviceType = typeof(IDoSomething);
            var implementationType = typeof(DoSomething);

            container.Register(serviceType, implementationType, Lifestyle.Singleton);

            Assert.NotNull(container.Resolve(serviceType));
            Assert.AreEqual(implementationType, container.Resolve(serviceType).GetType());
            Assert.Throws<TypeResolutionException>(() => container.Resolve(typeof(INotRegistered)));
        }

        [Test]
        public void Should_be_able_to_use_constructor_injection()
        {
            var container = new NinjectComponentContainer(new StandardKernel());
            var serviceType = typeof(IDoSomething);
            var implementationType = typeof(DoSomethingWithDependency);

            container.Register(serviceType, implementationType, Lifestyle.Singleton);

            Assert.Throws<TypeResolutionException>(() => container.Resolve(serviceType));
            Assert.Throws<TypeResolutionException>(() => container.Resolve<IDoSomething>());

            var someDependency = new SomeDependency();

            container.Register(typeof(ISomeDependency), someDependency);

            Assert.AreSame(someDependency, container.Resolve<IDoSomething>().SomeDependency);
        }
    }
}