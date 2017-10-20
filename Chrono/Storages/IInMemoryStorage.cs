namespace Chrono.Storages
{
    public interface IInMemoryStorage : IStorage
    {
        bool DoesSessionExist(string sessionId);

        Session CreateSession(string sessionId);

        void Add(Session session);

        void Clear();
    }
}