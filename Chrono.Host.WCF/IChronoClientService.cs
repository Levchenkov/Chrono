using Chrono.Client;
using System.ServiceModel;

namespace Chrono.Host.WCF
{
    [ServiceContract]
    public interface IChronoClientService
    {
        [OperationContract]
        void Save(ChronoSnapshot chronoSnapshot);

        [OperationContract]
        ChronoSnapshot FindLastSnapshotByKey(string sessionId, string key);

        [OperationContract]
        ChronoSession GetSession(string sessionId);

        [OperationContract]
        ChronoSessionMode GetSessionMode(string sessionId);
    }    
}
