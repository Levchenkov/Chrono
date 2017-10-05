using Chrono.Client;
using Chrono.Client.DataMappers;
using Chrono.Storages;

namespace Chrono.Host.Services
{
    public class ClientService : IClientService
    {
        private readonly ISessionModeService sessionModeService;
        private readonly IStorage storage;
        private readonly IDataMapper<Session, ChronoSession> sessionDataMapper;
        private readonly IDataMapper<Snapshot, ChronoSnapshot> snapshotDataMapper;

        public ClientService(IStorage storage)
        {
            
            this.storage = storage;
            this.sessionDataMapper = new SessionDataMapper();
            this.snapshotDataMapper = new SnapshotDataMapper();
            this.sessionModeService = new SessionModeService();
        }

        public ChronoSnapshot FindLastSnapshotByKey(string sessionId, string key)
        {
            var snapshot = storage.FindLastSnapshotByKey(sessionId, key);
            var chronoSnapshot = snapshotDataMapper.Map(snapshot);

            return chronoSnapshot;
        }

        public ChronoSession GetSession(string sessionId)
        {
            var session = storage.GetSession(sessionId);
            var chronoSession = sessionDataMapper.Map(session);
            chronoSession.Mode = GetSessionMode(sessionId);
            return chronoSession;
        }

        public ChronoSessionMode GetSessionMode(string sessionId)
        {
            var mode = sessionModeService.GetSessionMode(sessionId);

            return mode;
        }

        public void Save(ChronoSnapshot chronoSnapshot)
        {
            var snapshot = snapshotDataMapper.Map(chronoSnapshot);
            storage.Add(snapshot);
        }
    }
}
