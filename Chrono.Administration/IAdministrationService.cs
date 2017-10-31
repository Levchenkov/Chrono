using Chrono.Client;

namespace Chrono.Administration
{
    public interface IAdministrationService : IChronoSessionManager
    {
        void RecordSession(string sessionId);

        void PlaySession(string sessionId);

        ChronoSession CreateSession();

        void RemoveSession(string sessionId);

        void RemoveSnapshot(string sessionId, string snapshotId);
    }
}
