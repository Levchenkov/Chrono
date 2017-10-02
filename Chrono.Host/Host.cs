using System;
using Chrono.Storages;
using Chrono.Host.Services;

namespace Chrono.Host
{
    public class Host : IHostConfigurable, IHost
    {
        public IClientService ClientService
        {
            get;
            set;
        }

        public IManageService ManageService
        {
            get;
            set;
        }

        protected IStorage Storage
        {
            get;
            set;
        }

        IStorage IHostConfigurable.Storage
        {
            get
            {
                return Storage;
            }
            set
            {
                Storage = value;
            }
        }
    }
}
