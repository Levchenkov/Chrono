using System;
using Chrono.Client;
using Chrono.Storages;
using Chrono.Host.Services;
using Chrono.Administration;

namespace Chrono.Host
{
    public class ChronoHostContext : IChronoHostContext
    {
        public static IChronoHostContext Current
        {
            get;
            set;
        }

        public IClientService ClientService
        {
            get;
            set;
        }

        public IAdministrationService AdministrationService
        {
            get;
            set;
        }

        public IStorage Storage
        {
            get;
            set;
        }
    }
}
