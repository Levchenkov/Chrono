using System;
using Chrono.DataProviders;

namespace Chrono.Database.DataProviders
{
    public abstract class DatabaseDataProvider : IDataProvider
    {
        public void AddSession(Session session)
        {
            throw new NotImplementedException();
        }

        public void AddSnapshot(Snapshot snapshot)
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
