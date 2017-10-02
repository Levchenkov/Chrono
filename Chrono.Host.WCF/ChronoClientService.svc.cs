using System;
using Chrono.Client;
using Chrono.Host.Services;

namespace Chrono.Host.WCF
{
    public class ChronoClientService : IChronoClientService
    {
        public ChronoClientService()
        {
            HostHolder.SetHostIfHostIsNull(() => new HostBuilder().WithInMemoryStorage().Build());
        }

        public ChronoSnapshot FindLastSnapshotByKey(string sessionId, string key)
        {
            var result = HostHolder.Host.ClientService.FindLastSnapshotByKey(sessionId, key);
            return result;
        }

        public ChronoSession GetSession(string sessionId)
        {
            var result = HostHolder.Host.ClientService.GetSession(sessionId);
            return result;
        }

        public ChronoSessionMode GetSessionMode(string sessionId)
        {
            var result = HostHolder.Host.ClientService.GetSessionMode(sessionId);
            return result;
        }

        public void Save(ChronoSnapshot chronoSnapshot)
        {
            HostHolder.Host.ClientService.Save(chronoSnapshot);
        }
    }
}
