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

        protected Func<HostSettings> DefaultHostSettingsProvider
        {
            get
            {
                return () => new HostSettings
                {
                    IsEnabledFileCache = true,
                    IsSessionAutoCreate = false,
                    IsSessionAutoClose = false
                };
            }
        }

        protected Func<ChronoHostContext, IClientService> ClientServiceProvider
        {
            get
            {
                return hostContext => new ClientService(hostContext.Storage);
            }
        }

        protected Func<ChronoHostContext, IAdministrationService> ManageServiceProvider
        {
            get
            {
                return hostContext => new AdministrationService(hostContext.Storage);
            }
        }

        public IChronoHostContext Build()
        {
            if(StorageProvider == null)
            {
                StorageProvider = DefaultStorageProvider;
            }

            var hostSettings = DefaultHostSettingsProvider();
            if (HostSettingsProvider != null)
            {
                var customSettings = HostSettingsProvider();
                hostSettings = MergeProperties(hostSettings, customSettings);
            }

            var storageSettings = new StorageSettings
            {
                IsSessionAutoCreate = hostSettings.IsSessionAutoCreate,
                IsSessionAutoClose = hostSettings.IsSessionAutoClose,
                IsEnabledFileCache = hostSettings.IsEnabledFileCache
            };

            HostContext.Storage = StorageProvider(storageSettings);
            HostContext.ClientService = ClientServiceProvider(HostContext);
            HostContext.AdministrationService = ManageServiceProvider(HostContext);

            return HostContext;
        }

        private TType MergeProperties<TType>(TType defaultValues, TType customValues) where TType : new()
        {
            var result = new TType();

            var properties = typeof (TType).GetProperties();
            foreach (var property in properties)
            {
                var defaultValue = property.GetValue(defaultValues);
                var customValue = property.GetValue(customValues);

                if (defaultValue == customValue)
                {
                    property.SetValue(result, defaultValue);
                }
                else
                {
                    property.SetValue(result, customValue);
                }
            }

            return result;
        }
    }

}
