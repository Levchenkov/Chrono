using Chrono.Host.Services;

namespace Chrono.Host
{
    public interface IHost
    {
        IClientService ClientService
        {
            get;
            set;
        }

        IManageService ManageService
        {
            get;
            set;
        }
    }
}
