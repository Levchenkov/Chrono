namespace Chrono.Storages
{
    public interface IInMemoryStorage : IStorage
    {
        bool DoesSessionExist(string sessionId);

        void Add(Session session);

        void Clear();
    }
}