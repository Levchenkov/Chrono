using Chrono.Client;

namespace Chrono.Host.Services
{
    public interface IManageService
    {
        void RecordSession(string sessionId);

        void PlaySession(string sessionId);

        ChronoSession CreateSession();

        void CloseSession(string sessionId);

        void RemoveSession(string sessionId);

        void RemoveSnapshot(string sessionId, string snapshotId);
    }
}
