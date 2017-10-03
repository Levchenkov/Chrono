using System;
using Chrono.Client;
using Chrono.Common;
using Chrono.Host.Services;
using Chrono.Storages;
using Chrono.Administration;

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

        internal Func<StorageSettings, IStorage> StorageProvider
        {
            get;
            set;
        }

        internal Func<HostSettings> HostSettingsProvider
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

        protected Func<ChronoHostContext, IChronoAdministrationService> ManageServiceProvider
        {
            get
            {
                return hostContext => new ChronoAdministrationService(hostContext.Storage);
            }
        }

        public IChronoHostContext Build()
        {
            if(StorageProvider == null)
            {
                StorageProvider = DefaultStorageProvider;
            }

            var hostSettings = HostSettingsProvider();
            var storageSettings = new StorageSettings
            {
                IsSessionAutoCreate = hostSettings.IsSessionAutoCreate,
                IsSessionAutoClose = hostSettings.IsSessionAutoClose
            };

            HostContext.Storage = StorageProvider(storageSettings);
            HostContext.ClientService = ClientServiceProvider(HostContext);
            HostContext.AdministrationService = ManageServiceProvider(HostContext);

            return HostContext;
        }
    }

}
