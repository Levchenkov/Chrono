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
}
