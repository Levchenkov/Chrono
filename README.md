# Chrono
Chrono is a [NuGet library](https://www.nuget.org/packages/Chrono/) that help you to log any method invocations.

Chrono.Autofac is a [NuGet library](https://www.nuget.org/packages/Chrono.Autofac/) that contains few helpful classes for your project in case if you are using Autofac as IoC container.

Features
---
* Log method invocation.
* Log method invocation with it parameters and result.

Get Started
---
- Implement interface **IChronoHostProvider** and configure **ChronoHost** as you need using **ChronoHostBuilder**. It implementation will provide an instance of **ChronoHost**. 

```C#
public class ChronoHostProvider : IChronoHostProvider
{
    private static IChronoHost chronoHost;

    public IChronoHost GetChronoHost()
    {
       if (chronoHost == null)
       {
           chronoHost = new ChronoHostBuilder().WithFileStorage().Build();
       }
       return chronoHost;
   }
}
```

- Implement interface **IChronoSesssionIdProvider**. It implementation will provide current **ChronoSession** identifier. 

```C#
public class ChronoSesssionIdProvider : IChronoSesssionIdProvider
{
    public string GetSessionId()
    {
        if (HttpContext.Current == null)
        {
            return null;
        }

        var sessionId = (string) HttpContext.Current.Items[ApplicationConstants.ChronoSessionIdKey];
        return sessionId;
    }
}
```

- Create **ChronoSession** using **IChronoSessionManager** on ***Application_BeginRequest*** as _Example_. 

  If you use [Chrono.Autofac](https://www.nuget.org/packages/Chrono.Autofac/) library, you can resolve **IChronoSessionManager** by Autofac DI. 

  If you don't use Chrono.Autofac library, you can use **AdministrationService** propery from **IChronoHost** as **IChronoSessionManager**. 

- Log method invocation.

  If you use [Chrono.Autofac](https://www.nuget.org/packages/Chrono.Autofac/):

  You can intercept your methods by **ChronoInterceptor**.

  If you don't use Chrono.Autofac library:

  You need to create **ChronoSnapshot** and save it by **ClientService** propery from **IChronoHost**. ChronoSnapshot.Id should be unique. ChronoSnapshot.SessionId should be current ChronoSession.Id.

- Close **ChronoSession** using **IChronoSessionManager** on ***Application_EndRequest*** as _Example_.

    If you use [Chrono.Autofac](https://www.nuget.org/packages/Chrono.Autofac/) library, you can resolve **IChronoSessionManager** by Autofac DI. 

    If you don't use Chrono.Autofac library, you can use **AdministrationService** propery from **IChronoHost** as **IChronoSessionManager**. 

Release Notes
---
#### 0.1.1
Changed output directory for sessions.
#### 0.1.0
First release.
