using System.ServiceModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Chrono.Functional.Tests
{
    [TestClass]
    public class HostTests
    {
        [TestInitialize]
        public void Initialize()
        {
            //var baseAddress = "http://localhost:12345/ChronoAdministrationService";
            //var factory = new ChannelFactory<TAdminService>(new BasicHttpBinding(), new EndpointAddress(baseAddress));
            //var service = factory.CreateChannel();
        }

        [TestMethod]
        public void CreateSnapshotInFileStorage()
        {
            
        }
    }
}
