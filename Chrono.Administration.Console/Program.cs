using Chrono.Host.Console;

namespace Chrono.Administration.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            ChronoAdministrationConfigurator.Configure<IChronoAdministrationService>();            

            var processor = new Processor();
            processor.Run();
        }        
    }
}
