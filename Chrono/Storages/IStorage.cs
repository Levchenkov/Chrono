namespace Chrono.Storages
{
    public interface IStorage
    {
        Session CreateSession();

        Session CreateSession(string sessionId);

        void CloseSession(string sessionId);

        void Add(Snapshot snapshot);

        Session GetSession(string sessionId);

        Snapshot GetSnapshot(string sessionId, string snapshotId);

        FuncResult<Session> GetSessionSave(string sessionId);

        FuncResult<Snapshot> GetSnapshotSave(string sessionId, string snapshotId);

        Snapshot FindLastSnapshotByKey(string sessionId, string key);

        void RemoveSession(string sessionId);

        void RemoveSnapshot(string sessionId, string snapshotId);
    }
}
