using Chrono.Client;
using Chrono.Host.Services;
using Chrono.Storages;

namespace Chrono.Host
{
    public class ChronoHostBuilder
    {
        internal ChronoHostContext HostContext
        {
            get;
        }

        public ChronoHostBuilder()
        {
            HostContext = new ChronoHostContext();         
        }

        public ChronoHostBuilder WithInMemoryStorage()
        {
            HostContext.Storage = new InMemoryStorage();

            return this;
        }

        public IChronoHostContext Build()
        {
            if(HostContext.Storage == null)
            {
                WithInMemoryStorage();
            }

            HostContext.ClientService = new ChronoClientService(HostContext.Storage);
            HostContext.ManageService = new ManageService(HostContext.Storage);

            return HostContext;
        }
    }
}
