using Chrono.Client;
using Chrono.Host.Services;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Chrono.Functional.Tests
{
    [TestClass]
    public class SessionModeServiceTests
    {
        private SessionModeService subject;

        [TestMethod]
        public void GetSessionMode_SessionIdDoesNotExist_ShouldBeRecord()
        {
            subject = new SessionModeService();

            var result = subject.GetSessionMode("qwe");

            result.Should().Be(ChronoSessionMode.Record);
        }

        [TestMethod]
        public void GetSessionMode_PlaySession_ShouldBePlay()
        {
            subject = new SessionModeService();
            subject.PlaySession("qwe");

            var result = subject.GetSessionMode("qwe");

            result.Should().Be(ChronoSessionMode.Play);
        }

        [TestMethod]
        public void GetSessionMode_RecordSession_ShouldBeRecord()
        {
            subject = new SessionModeService();
            subject.RecordSession("qwe");

            var result = subject.GetSessionMode("qwe");

            result.Should().Be(ChronoSessionMode.Record);
        }
    }
}
