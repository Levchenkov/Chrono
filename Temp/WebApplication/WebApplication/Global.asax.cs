using Autofac;
using Autofac.Core;
using Autofac.Extras.DynamicProxy;
using Autofac.Integration.Mvc;
using Autofac.Integration.Wcf;
using System.Reflection;
using System.ServiceModel;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebApplication.ChronoService;
using WebApplication.Infrastucture;
using WebApplication.Services;

namespace WebApplication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var builder = new ContainerBuilder();

            builder.RegisterType<ChronoInterceptor>().AsSelf();
            builder.RegisterType<Services.Service>().As<IService>().EnableInterfaceInterceptors().InterceptedBy(typeof(ChronoInterceptor));

            //mvc
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterModule<AutofacWebTypesModule>();
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();
            builder.RegisterFilterProvider();

            //wcf
            builder.Register(c => new ChannelFactory<IStateService>(new BasicHttpBinding(), new EndpointAddress("http://localhost:50899/Service.svc"))).SingleInstance();

            builder
              .Register(c => c.Resolve<ChannelFactory<IStateService>>().CreateChannel())
              .As<IStateService>()
              .UseWcfSafeRelease();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
