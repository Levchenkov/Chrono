using Chrono.Client;
using System.ServiceModel;

namespace Chrono.Host.WCF
{
    //todo: we shouldn't create a copy of this interface at WCF client side. We will use references. See project modules.
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
