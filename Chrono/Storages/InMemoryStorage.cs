using Chrono.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using Chrono.Exceptions;

namespace Chrono.Storages
{
    public class InMemoryStorage : IInMemoryStorage
    {
        private readonly IDictionary<string, Session> sessions;
        private readonly StorageSettings settings;

        public InMemoryStorage(StorageSettings settings)
        {
            this.settings = settings;

            sessions = new Dictionary<string, Session>();
        }

        public void Add(Snapshot snapshot)
        {
            Contract.NotNull<ArgumentNullException>(snapshot.SessionId);

            var session = GetSession(snapshot.SessionId);
            session.AddSnapshot(snapshot);

            if (settings.IsSessionAutoClose)
            {
                CloseSessionInternal(session);
            }
        }

        public void Add(Session session)
        {
            if (sessions.ContainsKey(session.Id))
            {
                sessions.Remove(session.Id);
            }

            sessions.Add(session.Id, session);
        }

        public void Clear()
        {
            sessions.Clear();
        }

        public Session CreateSession()
        {
            var session = CreateSession(null);

            return session;
        }

        public Session CreateSession(string sessionId)
        {
            var session = CreateSessionInternal(sessionId);
            sessions.Add(session.Id, session);

            return session;
        }

        public void CloseSession(string sessionId)
        {
            var session = GetSession(sessionId);
            CloseSessionInternal(session);
        }

        private void CloseSessionInternal(Session session)
        {
            session.End = DateTime.Now;
        }

        public Session GetSession(string sessionId)
        {
            var sessionResult = GetSessionSave(sessionId);

            if (sessionResult.IsSuccessful)
            {
                return sessionResult.Value;
            }

            throw new SessionNotFoundException();
        }

        public FuncResult<Session> GetSessionSave(string sessionId)
        {
            var result = GetSessionInternal(sessionId);
            if (result.IsSuccessful)
            {
                return result;
            }

            if (settings.IsSessionAutoCreate)
            {
                var session = CreateSession(sessionId);
                return session.AsFuncResult();
            }

            return FuncResult.Failed<Session>();
        }

        public Snapshot GetSnapshot(string sessionId, string snapshotId)
        {
            var snapshotResult = GetSnapshotSave(sessionId, snapshotId);

            if (snapshotResult.IsSuccessful)
            {
                return snapshotResult.Value;
            }

            throw new SnapshotNotFoundException();
        }

        public FuncResult<Snapshot> GetSnapshotSave(string sessionId, string snapshotId)
        {
            var sessionResult = GetSessionSave(sessionId);
            if (sessionResult.IsSuccessful)
            {
                var snapshotResult = sessionResult.Value.GetSnapshotSave(snapshotId);
                return snapshotResult;
            }
            
            return FuncResult.Failed<Snapshot>();
        }

        public void RemoveSession(string sessionId)
        {
            if (sessions.ContainsKey(sessionId))
            {
                sessions.Remove(sessionId);
            }
        }

        public void RemoveSnapshot(string sessionId, string snapshotId)
        {
            var result = GetSessionSave(sessionId);
            if (result.IsSuccessful)
            {
                result.Value.RemoveSnapshot(snapshotId);
            }
        }

        public bool DoesSessionExist(string sessionId)
        {
            return sessions.ContainsKey(sessionId);
        }

        public Snapshot FindLastSnapshotByKey(string sessionId, string key)
        {
            var session = GetSession(sessionId);
            var snapshot = session.Snapshots.Values.LastOrDefault(x => x.Key == key);

            Contract.NotNull<ArgumentException>(snapshot);

            return snapshot;
        }

        protected FuncResult<Session> GetSessionInternal(string sessionId)
        {
            if (!sessions.ContainsKey(sessionId))
            {
                return FuncResult.Failed<Session>();
            }

            return sessions[sessionId].AsFuncResult();
        }

        private Session CreateSessionInternal(string sessionId = null)
        {
            if (sessionId == null)
            {
                sessionId = Guid.NewGuid().ToString();
            }

            var session = new Session
            {
                Id = sessionId,
                Begin = DateTime.Now
            };

            return session;
        }
    }
}
