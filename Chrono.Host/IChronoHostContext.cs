using Chrono.Administration;
using Chrono.Client;
using Chrono.Storages;

namespace Chrono.Host
{
    public interface IChronoHostContext
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
