using Chrono.Storages;
using System;
using System.Collections.Generic;

namespace Chrono.Database.Storages
{
    public abstract class DatabaseStorage : IStorage
    {
        public void Add(Snapshot snapshot)
        {
            throw new NotImplementedException();
        }

        public void CloseSession(string sessionId)
        {
            throw new NotImplementedException();
        }

        public Session CreateSession()
        {
            throw new NotImplementedException();
        }

        public FuncResult<Snapshot> GetSnapshotSave(string sessionId, string snapshotId)
        {
            throw new NotImplementedException();
        }

        public Snapshot FindLastSnapshotByKey(string sessionId, string key)
        {
            throw new NotImplementedException();
        }

        public Session GetSession(string sessionId)
        {
            throw new NotImplementedException();
        }

        public Snapshot GetSnapshot(string sessionId, string snapshotId)
        {
            throw new NotImplementedException();
        }

        public FuncResult<Session> GetSessionSave(string sessionId)
        {
            throw new NotImplementedException();
        }

        public void RemoveSession(string sessionId)
        {
            throw new NotImplementedException();
        }

        public void RemoveSnapshot(string sessionId, string snapshotId)
        {
            throw new NotImplementedException();
        }
    }
}
