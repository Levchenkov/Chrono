using System;
using Chrono.Client;
using Chrono.Storages;
using Chrono.Client.DataMappers;
using Chrono.Administration;

namespace Chrono.Host.Services
{
    public class AdministrationService : IAdministrationService
    {
        private readonly ISessionModeService sessionModeService;
        private readonly IStorage storage;
        private readonly IDataMapper<Session, ChronoSession> sessionDataMapper;
        private readonly IDataMapper<Snapshot, ChronoSnapshot> snapshotDataMapper;

        public AdministrationService(IStorage storage)
        {
            this.storage = storage;
            this.sessionDataMapper = new SessionDataMapper();
            this.snapshotDataMapper = new SnapshotDataMapper();
            this.sessionModeService = new SessionModeService();
        }

        public ChronoSession CreateSession(string sessionId)
        {
            var session = storage.CreateSession(sessionId);
            var chronoSession = sessionDataMapper.Map(session);
            chronoSession.Mode = sessionModeService.GetSessionMode(session.Id);

            return chronoSession;
        }

        public void CloseSession(string sessionId)
        {
            storage.CloseSession(sessionId);
        }

        public ChronoSession CreateSession()
        {
            var session = storage.CreateSession();
            var chronoSession = sessionDataMapper.Map(session);
            chronoSession.Mode = sessionModeService.GetSessionMode(session.Id);

            return chronoSession;
        }

        public void PlaySession(string sessionId)
        {
            sessionModeService.PlaySession(sessionId);
        }

        public void RecordSession(string sessionId)
        {
            sessionModeService.RecordSession(sessionId);
        }

        public void RemoveSession(string sessionId)
        {
            storage.RemoveSession(sessionId);
        }

        public void RemoveSnapshot(string sessionId, string snapshotId)
        {
            storage.RemoveSnapshot(sessionId, snapshotId);
        }
    }
}
