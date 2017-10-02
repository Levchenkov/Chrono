using Chrono.Storages;

namespace Chrono.Host
{
    internal interface IHostConfigurable
    {
        IStorage Storage
        {
            get;
            set;
        }
    }    
}
