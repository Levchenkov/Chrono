using Chrono.Client;

namespace Chrono.Host.WCF
{
    public class ChronoClientService : IChronoClientService
    {
        public ChronoClientService()
        {
            ChronoHostConfigurator.ConfigureHost();
        }

        public ChronoSnapshot FindLastSnapshotByKey(string sessionId, string key)
        {
            var result = ChronoHostContext.Current.ClientService.FindLastSnapshotByKey(sessionId, key);
            return result;
        }

        public ChronoSession GetSession(string sessionId)
        {
            var result = ChronoHostContext.Current.ClientService.GetSession(sessionId);
            return result;
        }

        public ChronoSessionMode GetSessionMode(string sessionId)
        {
            var result = ChronoHostContext.Current.ClientService.GetSessionMode(sessionId);
            return result;
        }

        public void Save(ChronoSnapshot chronoSnapshot)
        {
            ChronoHostContext.Current.ClientService.Save(chronoSnapshot);
        }
    }
}
