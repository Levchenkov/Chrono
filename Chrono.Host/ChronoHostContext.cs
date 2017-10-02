using System;
using Chrono.Client;
using Chrono.Storages;
using Chrono.Host.Services;

namespace Chrono.Host
{
    public class ChronoHostContext : IChronoHostContext
    {
        public static IChronoHostContext Current
        {
            get;
            set;
        }

        public IChronoClientService ClientService
        {
            get;
            set;
        }

        public IManageService ManageService
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
