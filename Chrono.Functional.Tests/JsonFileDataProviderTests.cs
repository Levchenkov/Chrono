using System;
using System.IO;
using Chrono.FileSystem.DataProviders;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Chrono.Functional.Tests
{
    [TestClass]
    public class JsonFileDataProviderTests
    {
        private JsonFileDataProvider subject;

        [TestInitialize]
        public void Initialize()
        {
            subject = new JsonFileDataProvider();
        }

        [TestMethod]
        public void AddSnapshot_NotEmptySnapshot_FileShouldBeCreated()
        {
            var snapshot = new Snapshot
            {
                SessionId = "SessionId",
                Begin = DateTime.Now,
                End = DateTime.Now,
                Id = "SnapshotId",
                Key = "Key",
                Value = "Value",
                Parameters = "param1, param2"
            };

            subject.AddSnapshot(snapshot);

            var filePath = Path.Combine(Environment.CurrentDirectory, "Sessions", $"Session.{snapshot.SessionId}", $"Snapshot.{snapshot.Id}.txt");
            File.Exists(filePath).Should().BeTrue();
        }

        [TestMethod]
        public void AddSession_NotEmptySession_FileShouldBeCreated()
        {
            var snapshot = new Snapshot
            {
                Begin = DateTime.Now,
                End = DateTime.Now,
                Id = "SnapshotId",
                Key = "Key",
                Value = "Value",
                Parameters = "param1, param2"
            };

            var session = new Session
            {
                Begin = DateTime.Now,
                End = DateTime.Now,
                Id = "SessionId"
            };

            session.AddSnapshot(snapshot);

            subject.AddSession(session);

            var filePath = Path.Combine(Environment.CurrentDirectory, "Sessions", $"Session.{session.Id}", $"Session.{session.Id}.txt");
            File.Exists(filePath).Should().BeTrue();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetSnapshot_SessionIdIsNull_ExpectedException()
        {
            subject.GetSnapshot(null, "some id");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetSnapshot_SnapshotIdIsNull_ExpectedException()
        {
            subject.GetSnapshot("some id", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetSession_SessionIdIsNull_ExpectedException()
        {
            subject.GetSession(null);
        }

        [TestMethod]
        public void GetSnapshot_AfterAddSnapshot_ShouldExist()
        {
            var snapshot = new Snapshot
            {
                SessionId = "SessionId",
                Begin = DateTime.Now,
                End = DateTime.Now,
                Id = "SnapshotId",
                Key = "Key",
                Value = "Value",
                Parameters = "param1, param2",
                Session = new Session()
            };

            subject.AddSnapshot(snapshot);

            var result = subject.GetSnapshot(snapshot.SessionId, snapshot.Id);

            result.Should().NotBeNull();
            result.SessionId.Should().Be(snapshot.SessionId);
            result.Id.Should().Be(snapshot.Id);
            result.Session.Should().BeNull();
        }

        [TestMethod]
        public void GetSession_AfterAddSession_ShouldExist()
        {
            var snapshot = new Snapshot
            {
                Begin = DateTime.Now,
                End = DateTime.Now,
                Id = "SnapshotId",
                Key = "Key",
                Value = "Value",
                Parameters = "param1, param2"
            };

            var session = new Session
            {
                Begin = DateTime.Now,
                End = DateTime.Now,
                Id = "SessionId"
            };

            session.AddSnapshot(snapshot);

            subject.AddSession(session);

            var result = subject.GetSession(session.Id);

            result.Should().NotBeNull();
            result.Id.Should().Be(session.Id);
            result.Snapshots.Should().NotBeEmpty();

            result.Snapshots.ContainsKey(snapshot.Id).Should().BeTrue();
            var resultSnapshot = result.Snapshots[snapshot.Id];
            resultSnapshot.Should().NotBeNull();
            resultSnapshot.Id.Should().Be(snapshot.Id);
            resultSnapshot.SessionId.Should().Be(session.Id);
            resultSnapshot.Session.Should().NotBeNull();
            resultSnapshot.Session.Id.Should().Be(session.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RemoveSnapshot_SessionIdIsNull_ExpectedException()
        {
            subject.RemoveSnapshot(null, "some id");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RemoveSnapshot_SnapshotIdIsNull_ExpectedException()
        {
            subject.RemoveSnapshot("some id", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RemoveSession_SessionIdIsNull_ExpectedException()
        {
            subject.RemoveSession(null);
        }

        [TestMethod]
        public void RemoveSnapshot_NotEmptySnapshotId_FileShouldNotExist()
        {
            var snapshot = new Snapshot
            {
                SessionId = "SessionToBeRemoved",
                Begin = DateTime.Now,
                End = DateTime.Now,
                Id = "SnapshotToBeRemoved",
                Key = "Key",
                Value = "Value",
                Parameters = "param1, param2",
                Session = new Session()
            };

            subject.AddSnapshot(snapshot);
            subject.RemoveSnapshot(snapshot.SessionId, snapshot.Id);

            var filePath = Path.Combine(Environment.CurrentDirectory, "Sessions", $"Session.{snapshot.SessionId}", $"Snapshot.{snapshot.Id}.txt");
            File.Exists(filePath).Should().BeFalse();
        }

        [TestMethod]
        public void RemoveSession_NotEmptySnapshotId_FileShouldNotExist()
        {
            var session = new Session
            {
                Begin = DateTime.Now,
                End = DateTime.Now,
                Id = "SessionToBeRemoved"
            };

            subject.AddSession(session);
            subject.RemoveSession(session.Id);

            var filePath = Path.Combine(Environment.CurrentDirectory, "Sessions", $"Session.{session.Id}", $"Session.{session.Id}.txt");
            File.Exists(filePath).Should().BeFalse();
        }
    }
}