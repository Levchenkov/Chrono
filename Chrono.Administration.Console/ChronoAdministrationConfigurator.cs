using System.ServiceModel;

namespace Chrono.Administration.Console
{
    public class ChronoAdministrationConfigurator
    {
        public static void Configure<TAdminService>()
        {
            if (ChronoAdministrationContext<TAdminService>.Current == null)
            {
                var administrationService = GetAdministrationService<TAdminService>();
                ChronoAdministrationContext<TAdminService>.Current = new ChronoAdministrationBuilder<TAdminService>().With(administrationService).Build();
            }
        }

        private static TAdminService GetAdministrationService<TAdminService>()
        {
            //var baseAddress = "http://localhost:59567/ChronoAdministrationService.svc";
            var baseAddress = "http://localhost:12345/ChronoAdministrationService";
            var factory = new ChannelFactory<TAdminService>(new BasicHttpBinding(), new EndpointAddress(baseAddress));
            var service = factory.CreateChannel();
            return service;
        }

        //public static void Configure()
        //{
        //    if(ChronoAdministrationContext.Current == null)
        //    {
        //        var administrationService = GetAdministrationService();
        //        ChronoAdministrationContext.Current = new ChronoAdministrationBuilder().With(administrationService).Build();
        //    }            
        //}

        //private static IAdministrationService GetAdministrationService()
        //{
        //    var baseAddress = "http://localhost:59567/ChronoAdministrationService.svc";
        //    var factory = new ChannelFactory<IAdministrationService>(new BasicHttpBinding(), new EndpointAddress(baseAddress));
        //    var service = factory.CreateChannel();
        //    return service;
        //}
    }
}
