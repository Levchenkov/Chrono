using System;
using Chrono.Client;
using Chrono.Common;
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

        public Func<StorageSettings, IStorage> StorageProvider
        {
            get;
            set;
        }

        public Func<HostSettings> HostSettingsProvider
        {
            get;
            set;
        }

        public ChronoHostBuilder WithInMemoryStorage()
        {
            StorageProvider = settings => new InMemoryStorage(settings);

            return this;
        }

        public ChronoHostBuilder UseHostSettings(HostSettings settings)
        {
            HostSettingsProvider = () => settings;

            return this;
        }

        protected Func<StorageSettings, IStorage> DefaultStorageProvider
        {
            get
            {
                return settings => new InMemoryStorage(settings);
            }
        }

        protected Func<ChronoHostContext, IChronoClientService> ClientServiceProvider
        {
            get
            {
                return hostContext => new ChronoClientService(hostContext.Storage);
            }
        }

        protected Func<ChronoHostContext, IManageService> ManageServiceProvider
        {
            get
            {
                return hostContext => new ManageService(hostContext.Storage);
            }
        }

        public IChronoHostContext Build()
        {
            if(StorageProvider == null)
            {
                StorageProvider = DefaultStorageProvider;
            }

            var hostSettings = HostSettingsProvider();

            HostContext.Storage = StorageProvider(new StorageSettings { IsSessionAutoCreate = hostSettings.IsSessionAutoCreate });
            HostContext.ClientService = ClientServiceProvider(HostContext);
            HostContext.ManageService = ManageServiceProvider(HostContext);

            return HostContext;
        }
    }

}
