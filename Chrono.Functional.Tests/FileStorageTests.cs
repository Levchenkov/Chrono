using System;
using Chrono.Exceptions;
using Chrono.FileSystem.DataProviders;
using Chrono.FileSystem.Storages;
using Chrono.Storages;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Chrono.Functional.Tests
{
    [TestClass]
    public class FileStorageTests
    {
        private FileStorage subject;
        private JsonFileDataProvider dataProvider;
        private IInMemoryStorage inMemoryStorage;

        [TestInitialize]
        public void Initialize()
        {
            var settings = new StorageSettings
            {
                IsSessionAutoClose = false,
                IsSessionAutoCreate = false,
                IsEnabledFileCache = true
            };

            dataProvider = new JsonFileDataProvider();
            inMemoryStorage = new InMemoryStorage(settings);
            subject = new FileStorage(inMemoryStorage, settings);
        }

        [TestMethod]
        public void CreateSession_NoParameters_SessionShouldBeCreated()
        {
            var result = subject.CreateSession();

            result.Should().NotBeNull();
            result.Id.Should().NotBeNullOrEmpty();
            result.Snapshots.Should().BeEmpty();
        }

        [TestMethod]
        public void GetSession_SessionDoesNotExistInMemory_SessionShouldBeReadFromFile()
        {
            var sessionId = "SessionId";
            var session = new Session
            {
                Begin = DateTime.Now,
                End = DateTime.Now,
                Id = sessionId
            };

            dataProvider.AddSession(session);
            inMemoryStorage.Clear();

            var result = subject.GetSession(sessionId);

            result.Should().NotBeNull();
            result.Id.Should().Be(sessionId);
        }

        [TestMethod]
        public void GetSession_SessionExistInMemory_SessionShouldBeReadFromMemory()
        {
            var sessionId = "SessionId2";
            var session = new Session
            {
                Begin = DateTime.Now,
                End = DateTime.Now,
                Id = sessionId
            };

            inMemoryStorage.Clear();
            inMemoryStorage.Add(session);

            var result = subject.GetSession(sessionId);

            result.Should().NotBeNull();
            result.Id.Should().Be(sessionId);
        }

        [TestMethod]
        public void GetSession_SessionExistInMemoryAndFileCacheIsNotEnabled_SessionShouldBeReadFromFile()
        {
            var settings = new StorageSettings
            {
                IsEnabledFileCache = false
            };
            subject = new FileStorage(inMemoryStorage, settings);

            var sessionId = "SessionId";
            var sessionIdMemory = new Session
            {
                Begin = DateTime.Now.Date,
                Id = sessionId
            };
            inMemoryStorage.Add(sessionIdMemory);

            var sessionInFile = new Session
            {
                Begin = DateTime.Now.Date.AddDays(1),
                End = DateTime.Now,
                Id = sessionId
            };

            dataProvider.AddSession(sessionInFile);

            var result = subject.GetSession(sessionId);

            result.Should().NotBeNull();
            result.Id.Should().Be(sessionId);
            result.Begin.Should().Be(sessionInFile.Begin);
        }

        [TestMethod]
        public void GetSession_SessionDoesNotExistInFileAndSessionAutoCreateIsTrue_SessionShouldBeCreated()
        {
            var settings = new StorageSettings
            {
                IsSessionAutoCreate = true
            };

            subject = new FileStorage(settings);
            var sessionId = "SessionId3";

            var result = subject.GetSession(sessionId);

            result.Should().NotBeNull();
            result.Id.Should().Be(sessionId);
        }

        [TestMethod]
        [ExpectedException(typeof(SessionNotFoundException))]
        public void GetSession_SessionDoesNotExistInFileAndSessionAutoCreateIsFalse_ExcepctedException()
        {
            var sessionId = "SessionId4";

            subject.GetSession(sessionId);
        }

        [TestMethod]
        [ExpectedException(typeof(SessionNotFoundException))]
        public void GetSnapshot_SessionDoesNotExist_ExpectedException()
        {
            subject.GetSnapshot("qwe", "asd");
        }

        [TestMethod]
        [ExpectedException(typeof(SnapshotNotFoundException))]
        public void GetSnapshot_SessionExistAndSnapshotDoesnotExist_ExpectedException()
        {
            var sessionId = "SessionId";
            var session = new Session
            {
                Begin = DateTime.Now,
                End = DateTime.Now,
                Id = sessionId
            };

            dataProvider.AddSession(session);

            subject.GetSnapshot(sessionId, "asd");
        }

        [TestMethod]
        public void GetSnapshot_SessionAndSnapshotExist_SnapshotShouldNotBeEmpty()
        {
            var sessionId = "SessionId";
            var session = new Session
            {
                Begin = DateTime.Now,
                End = DateTime.Now,
                Id = sessionId
            };

            var snapshotId = "SnapshotId";
            var snapshot = new Snapshot
            {
                Id = snapshotId
            };
            session.AddSnapshot(snapshot);

            dataProvider.AddSession(session);

            var result = subject.GetSnapshot(sessionId, snapshotId);

            result.Should().NotBeNull();
            result.Id.Should().Be(snapshotId);
            result.SessionId.Should().Be(sessionId);
        }

        [TestMethod]
        public void AddSnapshot_SessionIsNotAutoClose_SnapshotShouldBeSavedInMemory()
        {
            var snapshot = new Snapshot
            {
                Id = "NewSnapshotId",
                SessionId = "SessionId"
            };

            var session  = new Session
            {
                Id = "SessionId"
            };

            inMemoryStorage.Clear();
            inMemoryStorage.Add(session);

            subject.Add(snapshot);

            var result = inMemoryStorage.GetSnapshot(snapshot.SessionId, snapshot.Id);

            result.Should().NotBeNull();
        }

        [TestMethod]
        public void AddSnapshot_SessionIsAutoClose_SnapshotShouldBeSavedInMemoryAndSavedInFile()
        {
            var settings = new StorageSettings
            {
                IsSessionAutoClose = true
            };

            subject = new FileStorage(inMemoryStorage, settings);

            var snapshot = new Snapshot
            {
                Id = "NewSnapshotId",
                SessionId = "SessionId"
            };

            var session = new Session
            {
                Id = "SessionId"
            };

            inMemoryStorage.Clear();
            inMemoryStorage.Add(session);

            dataProvider.RemoveSnapshot(snapshot.SessionId, snapshot.Id);
            subject.Add(snapshot);

            var resultFromMemory = inMemoryStorage.GetSnapshot(snapshot.SessionId, snapshot.Id);
            var resultFromFile = dataProvider.GetSnapshot(snapshot.SessionId, snapshot.Id);

            resultFromFile.Should().NotBeNull();
            resultFromMemory.Should().NotBeNull();
        }

        public void CloseSession()
        {
            
        }

        [TestMethod]
        public void RemoveSession_SessionIdProvided_SessionShouldBeRemovedFromMemoryAndFile()
        {
            var sessionId = "SessionId";
            var session = new Session
            {
                Id = sessionId
            };

            inMemoryStorage.Clear();
            inMemoryStorage.Add(session);
            dataProvider.AddSession(session);

            subject.RemoveSession(sessionId);

            var resultFromMemory = inMemoryStorage.GetSessionSave(sessionId);
            resultFromMemory.IsSuccessful.Should().BeFalse();

            var resultFromFile = dataProvider.GetSessionSave(sessionId);
            resultFromFile.IsSuccessful.Should().BeFalse();
        }

        public void RemoveSnapshot()
        {
            
        }

        public void FindLastSnapshotByKey()
        {
            
        }
    }
}