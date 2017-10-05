using Chrono.Client;
using System;
using System.ServiceModel;

namespace Chrono.Host.Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            var baseAddress = new Uri("http://localhost:12345/ChronoAdministrationService");

            //todo: why ServiceHost supports only classes and ChannelFactory supports only interfaces?
            using (ServiceHost host = new ServiceHost(typeof(ChronoAdministrationService), baseAddress))
            {
                host.Open();

                System.Console.WriteLine("The service is ready at {0}", baseAddress);
                System.Console.WriteLine("Press <Enter> to stop the service.");
                System.Console.ReadLine();

                // Close the ServiceHost.
                host.Close();
            }
        }
    }

    public static class ChronoHostConfigurator
    {
        public static void ConfigureHost()
        {
            if (ChronoHostContext.Current == null)
            {
                ChronoHostContext.Current = new ChronoHostBuilder().WithInMemoryStorage().Build();
            }
        }
    }

    [ServiceContract]
    public interface IChronoAdministrationService
    {
        [OperationContract]
        void RecordSession(string sessionId);

        [OperationContract]
        void PlaySession(string sessionId);

        [OperationContract]
        ChronoSession CreateSession();

        [OperationContract]
        void CloseSession(string sessionId);
    }

    public class ChronoAdministrationService : IChronoAdministrationService
    {
        public ChronoAdministrationService()
        {
            ChronoHostConfigurator.ConfigureHost();
        }

        public void CloseSession(string sessionId)
        {
            ChronoHostContext.Current.AdministrationService.CloseSession(sessionId);
        }

        public ChronoSession CreateSession()
        {
            var result = ChronoHostContext.Current.AdministrationService.CreateSession();
            return result;
        }

        public void PlaySession(string sessionId)
        {
            ChronoHostContext.Current.AdministrationService.PlaySession(sessionId);
        }

        public void RecordSession(string sessionId)
        {
            ChronoHostContext.Current.AdministrationService.RecordSession(sessionId);
        }
    }
}
