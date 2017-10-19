using System;
using System.ServiceModel;

namespace Chrono.Host.Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            var administrationServiceAddress = new Uri("http://localhost:12345/ChronoAdministrationService");
            var clientServiceAddress = new Uri("http://localhost:12345/ChronoClientService");

            //todo: why ServiceHost supports only classes and ChannelFactory supports only interfaces?
            using (ServiceHost clientServiceHost = new ServiceHost(typeof(ChronoClientService), clientServiceAddress))
            {
                clientServiceHost.Open();
                using (ServiceHost administrationServiceHost = new ServiceHost(typeof(ChronoAdministrationService), administrationServiceAddress))
                {
                    administrationServiceHost.Open();

                    System.Console.WriteLine("The service is ready at {0}", administrationServiceAddress);
                    System.Console.WriteLine("Press <Enter> to stop the service.");
                    System.Console.ReadLine();

                    // Close the ServiceHost.
                    administrationServiceHost.Close();
                }
                clientServiceHost.Close();
            }
        }
    }
}
