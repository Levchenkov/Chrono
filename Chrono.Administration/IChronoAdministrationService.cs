using Chrono.Client;

namespace Chrono.Administration
{
    public interface IChronoAdministrationService
    {
        void RecordSession(string sessionId);

        void PlaySession(string sessionId);

        ChronoSession CreateSession();

        void CloseSession(string sessionId);

        void RemoveSession(string sessionId);

        void RemoveSnapshot(string sessionId, string snapshotId);
    }
}
