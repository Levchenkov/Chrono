using Chrono.Storages;
using Chrono.DataProviders;
using Chrono.FileSystem.DataProviders;

namespace Chrono.FileSystem.Storages
{
    public class FileStorage : IStorage
    {
        private IStorage inMemoryStorage;
        private IDataProvider dataProvider;

        public FileStorage()
        {
            inMemoryStorage = new InMemoryStorage();
            dataProvider = new JsonFileDataProvider();
        }

        public void Add(Snapshot snapshot)
        {
            inMemoryStorage.Add(snapshot);
        }

        public void CloseSession(string sessionId)
        {
            inMemoryStorage.CloseSession(sessionId);
            var session = inMemoryStorage.GetSession(sessionId);
            dataProvider.AddSession(session);
        }

        public Session CreateSession()
        {
            return inMemoryStorage.CreateSession();
        }

        public Session GetSession(string sessionId)
        {
            return inMemoryStorage.GetSession(sessionId);
        }

        public Snapshot GetSnapshot(string sessionId, string snapshotId)
        {
            return inMemoryStorage.GetSnapshot(sessionId, snapshotId);
        }

        public void RemoveSession(string sessionId)
        {
            inMemoryStorage.RemoveSession(sessionId);
        }

        public void RemoveSnapshot(string sessionId, string snapshotId)
        {
            inMemoryStorage.RemoveSnapshot(sessionId, snapshotId);
        }

        public Snapshot FindLastSnapshotByKey(string sessionId, string key)
        {
            return inMemoryStorage.FindLastSnapshotByKey(sessionId, key);
        }
    }
}
