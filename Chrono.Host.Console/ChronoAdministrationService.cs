using Chrono.Client;

namespace Chrono.Host.Console
{
    public class ChronoAdministrationService : IChronoAdministrationService
    {
        public ChronoAdministrationService()
        {
            ChronoHostConfigurator.ConfigureHost();
        }

        public void CloseSession(string sessionId)
        {
            ChronoHostContext.Current.AdministrationService.CloseSession(sessionId);
        }

        public ChronoSession CreateSession()
        {
            var result = ChronoHostContext.Current.AdministrationService.CreateSession();
            return result;
        }

        public void PlaySession(string sessionId)
        {
            ChronoHostContext.Current.AdministrationService.PlaySession(sessionId);
        }

        public void RecordSession(string sessionId)
        {
            ChronoHostContext.Current.AdministrationService.RecordSession(sessionId);
        }
    }
}