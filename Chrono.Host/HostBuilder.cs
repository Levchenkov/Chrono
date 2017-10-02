using Chrono.Host.Services;
using Chrono.Storages;

namespace Chrono.Host
{
    public class HostBuilder
    {
        internal IHostConfigurable Host
        {
            get;
            set;
        }

        public HostBuilder()
        {
            Host = new Host();         
        }

        public HostBuilder WithInMemoryStorage()
        {
            Host.Storage = new InMemoryStorage();

            return this;
        }

        public IHost Build()
        {
            if(Host.Storage == null)
            {
                WithInMemoryStorage();
            }
            var result = (IHost)Host;

            result.ClientService = new ClientService(Host.Storage);
            result.ManageService = new ManageService(Host.Storage);

            return result;
        }
    }
}
