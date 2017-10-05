using Chrono.Client;
using System.ServiceModel;

namespace Chrono.Host.WCF
{
    [ServiceContract]
    public interface IChronoAdministrationService
    {
        [OperationContract]
        void RecordSession(string sessionId);

        [OperationContract]
        void PlaySession(string sessionId);

        [OperationContract]
        ChronoSession CreateSession();

        [OperationContract]
        void CloseSession(string sessionId);
    }
}
