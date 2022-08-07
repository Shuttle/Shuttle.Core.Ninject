# Shuttle.Core.Ninject

> **Warning**
> This package has been deprecated in favour of [.NET Dependency Injection](https://docs.microsoft.com/en-us/dotnet/core/extensions/dependency-injection).

```
PM> Install-Package Shuttle.Core.Ninject
```

The `NinjectComponentContainer` implements both the `IComponentRegistry` and `IComponentResolver` interfaces.  

~~~c#
var container = new NinjectComponentContainer(new StandardKernel());
~~~

