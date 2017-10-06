using System.ServiceModel;
using Chrono.Client;

namespace Chrono.Host.Console
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