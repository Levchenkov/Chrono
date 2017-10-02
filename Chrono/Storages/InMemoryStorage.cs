using Chrono.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chrono.Storages
{
    public class InMemoryStorage : IStorage
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
        }

        public Session CreateSession()
        {
            var session = CreateSession(null);

            return session;
        }

        protected Session CreateSession(string sessionId)
        {
            var session = CreateSessionInternal(sessionId);
            sessions.Add(session.Id, session);

            return session;
        }

        public void CloseSession(string sessionId)
        {
            var session = GetSession(sessionId);
            session.End = DateTime.Now;
        }

        public Session GetSession(string sessionId)
        {
            if (!sessions.ContainsKey(sessionId))
            {
                if (settings.IsSessionAutoCreate)
                {
                    var session = CreateSession(sessionId);
                    return session;
                }

                throw new KeyNotFoundException();
            }

            return sessions[sessionId];
        }

        public Snapshot GetSnapshot(string sessionId, string snapshotId)
        {
            var session = GetSession(sessionId);
            var snapshot = session.GetSnapshot(snapshotId);
            return snapshot;
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

        public void RemoveSession(string sessionId)
        {
            if (!sessions.ContainsKey(sessionId))
            {
                throw new KeyNotFoundException();
            }
            sessions.Remove(sessionId);
        }

        public void RemoveSnapshot(string sessionId, string snapshotId)
        {
            var session = GetSession(sessionId);
            session.RemoveSnapshot(snapshotId);
        }

        public Snapshot FindLastSnapshotByKey(string sessionId, string key)
        {
            var session = GetSession(sessionId);
            var snapshot = session.Snapshots.Values.LastOrDefault(x => x.Key == key);

            Contract.NotNull<ArgumentException>(snapshot);

            return snapshot;
        }
    }
}
