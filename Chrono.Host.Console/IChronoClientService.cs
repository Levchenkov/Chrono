using System.ServiceModel;
using Chrono.Client;

namespace Chrono.Host.Console
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
