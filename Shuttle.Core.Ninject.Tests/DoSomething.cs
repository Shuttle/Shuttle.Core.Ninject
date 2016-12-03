namespace Shuttle.Core.Ninject.Tests
{
    public class DoSomething : IDoSomething
    {
        public ISomeDependency SomeDependency {
            get { return null; }
        }
    }
}