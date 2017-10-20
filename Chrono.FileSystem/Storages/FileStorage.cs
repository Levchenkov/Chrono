using Chrono.Storages;
using Chrono.DataProviders;
using Chrono.Exceptions;
using Chrono.FileSystem.DataProviders;

namespace Chrono.FileSystem.Storages
{
    public class FileStorage : IStorage
    {
        private readonly IInMemoryStorage inMemoryStorage;
        private readonly IDataProvider dataProvider;
        private readonly StorageSettings settings;

        public FileStorage(StorageSettings settings)
        {
            inMemoryStorage = new InMemoryStorage(settings);
            dataProvider = new JsonFileDataProvider();
            this.settings = settings;
        }

        public FileStorage(IInMemoryStorage inMemoryStorage, StorageSettings settings)
        {
            this.inMemoryStorage = inMemoryStorage;
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
            var result = GetSessionSave(sessionId);

            if (result.IsSuccessful)
            {
                return result.Value;
            }

            throw new SessionNotFoundException();
        }

        public Snapshot GetSnapshot(string sessionId, string snapshotId)
        {
            var session = GetSession(sessionId);
            var snapshot = session.GetSnapshot(snapshotId);

            return snapshot;
        }

        public FuncResult<Session> GetSessionSave(string sessionId)
        {
            if (settings.IsEnabledFileCache && inMemoryStorage.DoesSessionExist(sessionId))
            {
                return inMemoryStorage.GetSessionSave(sessionId);
            }

            var resultFromFile = dataProvider.GetSessionSave(sessionId);

            if (resultFromFile.IsSuccessful)
            {
                inMemoryStorage.Add(resultFromFile.Value);
                return resultFromFile;
            }

            if (settings.IsSessionAutoCreate)
            {
                var session = inMemoryStorage.CreateSession(sessionId);
                return session.AsFuncResult();
            }

            return FuncResult.Failed<Session>();
        }

        public FuncResult<Snapshot> GetSnapshotSave(string sessionId, string snapshotId)
        {
            var sessionResult = GetSessionSave(sessionId);

            if (sessionResult.IsSuccessful)
            {
                var session = sessionResult.Value;

                var snapshotResult = session.GetSnapshotSave(snapshotId);
                return snapshotResult;
            }

            return FuncResult<Snapshot>.Failed();
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
