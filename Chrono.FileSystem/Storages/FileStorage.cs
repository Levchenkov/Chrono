using Chrono.Storages;
using Chrono.DataProviders;
using Chrono.FileSystem.DataProviders;

namespace Chrono.FileSystem.Storages
{
    public class FileStorage : IStorage
    {
        private readonly IStorage inMemoryStorage;
        private readonly IDataProvider dataProvider;
        private readonly StorageSettings settings;

        public FileStorage(StorageSettings settings)
        {
            inMemoryStorage = new InMemoryStorage(settings);
            dataProvider = new JsonFileDataProvider();
            this.settings = settings;
        }

        public void Add(Snapshot snapshot)
        {
            inMemoryStorage.Add(snapshot);

            if (settings.IsSessionAutoClose)
            {
                dataProvider.AddSnapshot(snapshot);
            }
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
