namespace Chrono.Client
{
    public interface IClientService
    {
        void Save(ChronoSnapshot chronoSnapshot);

        ChronoSnapshot FindLastSnapshotByKey(string sessionId, string key);

        ChronoSession GetSession(string sessionId);

        ChronoSessionMode GetSessionMode(string sessionId);
    }
}
