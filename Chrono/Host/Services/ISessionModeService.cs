using Chrono.Client;

namespace Chrono.Host.Services
{
    public interface ISessionModeService
    {
        ChronoSessionMode GetSessionMode(string sessionId);

        void RecordSession(string sessionId);

        void PlaySession(string sessionId);
    }
}
