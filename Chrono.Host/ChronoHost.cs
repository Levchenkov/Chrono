using Chrono.Administration;
using Chrono.Client;

namespace Chrono.Host
{
    public class ChronoHost : IChronoHost
    {
        public IClientService ClientService
        {
            get;
            set;
        }

        public IAdministrationService AdministrationService
        {
            get;
            set;
        }
    }
}