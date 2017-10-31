using Chrono.Client;

namespace Chrono.Administration
{
    public interface IChronoSessionManager
    {
        ChronoSession CreateSession(string sessionId);

        void CloseSession(string sessionId);
    }
}