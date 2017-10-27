using Chrono.Client;

namespace Chrono.Administration
{
    public interface IAdministrationService
    {
        void RecordSession(string sessionId);

        void PlaySession(string sessionId);

        ChronoSession CreateSession();

        ChronoSession CreateSession(string sessionId);

        void CloseSession(string sessionId);

        void RemoveSession(string sessionId);

        void RemoveSnapshot(string sessionId, string snapshotId);
    }
}
