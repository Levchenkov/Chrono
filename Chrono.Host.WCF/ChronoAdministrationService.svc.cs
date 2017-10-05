using System;
using Chrono.Administration;
using Chrono.Client;

namespace Chrono.Host.WCF
{
    public class ChronoAdministrationService : IChronoAdministrationService, IAdministrationService
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

        public void RemoveSession(string sessionId)
        {
            throw new NotImplementedException();
        }

        public void RemoveSnapshot(string sessionId, string snapshotId)
        {
            throw new NotImplementedException();
        }
    }
}
