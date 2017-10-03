using Chrono.Administration;
using Chrono.Client;
using Chrono.Storages;

namespace Chrono.Host
{
    public interface IChronoHostContext
    {
        IChronoClientService ClientService
        {
            get;
        }

        IChronoAdministrationService AdministrationService
        {
            get;
        }

        IStorage Storage
        {
            get;
        }
    }
}
