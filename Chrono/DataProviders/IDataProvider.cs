namespace Chrono.DataProviders
{
    public interface IDataProvider
    {
        void AddSession(Session session);

        void AddSnapshot(Snapshot snapshot);

        Session GetSession(string sessionId);

        Snapshot GetSnapshot(string sessionId, string snapshotId);

        void RemoveSession(string sessionId);

        void RemoveSnapshot(string sessionId, string snapshotId);
    }
}
