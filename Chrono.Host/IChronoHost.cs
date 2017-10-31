using Chrono.Administration;
using Chrono.Client;

namespace Chrono.Host
{
    public interface IChronoHost
    {
        IClientService ClientService
        {
            get;
        }

        IAdministrationService AdministrationService
        {
            get;
        }
    }
}
