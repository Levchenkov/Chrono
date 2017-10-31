using Autofac;
using Chrono.Client.Autofac;
using Chrono.Host;

namespace Chrono.SelfHosted.Autofac
{
    public class ChronoModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.Register(c => c.Resolve<IChronoHostHolder>().GetChronoHost()).As<IChronoHost>();
            builder.Register(c => c.Resolve<IChronoHost>().AdministrationService).AsImplementedInterfaces();
            builder.Register(c => c.Resolve<IChronoHost>().ClientService).AsImplementedInterfaces();
            builder.RegisterType<ChronoInterceptor>().AsSelf();
        }
    }
}
