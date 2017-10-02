using Chrono.Client;
using Chrono.Host.Services;
using Chrono.Storages;

namespace Chrono.Host
{
    public interface IChronoHostContext
    {
        IChronoClientService ClientService
        {
            get;
        }

        IManageService ManageService
        {
            get;
        }

        IStorage Storage
        {
            get;
        }
    }
}
