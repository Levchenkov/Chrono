namespace Chrono.Storages
{
    public interface IStorage
    {
        Session CreateSession();

        void CloseSession(string sessionId);

        void Add(Snapshot snapshot);

        Session GetSession(string sessionId);

        Snapshot GetSnapshot(string sessionId, string snapshotId);

        Snapshot FindLastSnapshotByKey(string sessionId, string key);

        void RemoveSession(string sessionId);

        void RemoveSnapshot(string sessionId, string snapshotId);
    }
}
