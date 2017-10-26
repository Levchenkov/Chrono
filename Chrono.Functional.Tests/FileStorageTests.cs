using System;
using Chrono.Exceptions;
using Chrono.FileSystem.DataProviders;
using Chrono.FileSystem.Storages;
using Chrono.Storages;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Chrono.Functional.Tests
{
    //TODO: Add support of StorageSettings.IsEnabledFileChache
    //TODO: when save session, save snapshot in separate files. Create option?
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

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CloseSession_SessionIdIsNull_ExpectedException()
        {
            subject.CloseSession(null);
        }

        [TestMethod]
        [ExpectedException(typeof(SessionNotFoundException))]
        public void CloseSession_SessionIdProvidedAndSessionDoesNotExistInMemory_ExceptionExpected()
        {
            subject.CloseSession("NotExistingSessionId");
        }

        [TestMethod]
        public void CloseSession_SessionIdProvided_FileWithSessionShouldBeCreated()
        {
            var snapshot = new Snapshot
            {
                Id = "SnapshotId",
                Begin = DateTime.Now,
                End = DateTime.Now,
                Key = "key",
                Parameters = "parameter",
                Value = "value"
            };

            var session = new Session
            {
                Id = "SessionId",
                Begin = DateTime.Now
            };

            session.AddSnapshot(snapshot);
            inMemoryStorage.Clear();
            inMemoryStorage.Add(session);

            subject.CloseSession(session.Id);

            var sessionFromFile = dataProvider.GetSession(session.Id);
            sessionFromFile.Should().NotBeNull();
            sessionFromFile.Id.Should().Be(session.Id);
            sessionFromFile.Snapshots.Count.Should().Be(session.Snapshots.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RemoveSession_SessionIdIsNull_ExpectedException()
        {
            subject.RemoveSession(null);
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

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RemoveSnapshot_SessionIdIsNull_ExpectedException()
        {
            subject.RemoveSnapshot(null, "qwe");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RemoveSnapshot_SnapshotIdIsNull_ExpectedException()
        {
            subject.RemoveSnapshot("qwe", null);
        }

        [TestMethod]
        public void RemoveSnapshot_SessionIdAndSnapshotIdProvided_SnapshotShouldBeRemovedFromMemoryAndFile()
        {
            var snapshot = new Snapshot
            {
                Id = "SnapshotId"
            };

            var session = new Session
            {
                Id = "SessionId"
            };

            session.AddSnapshot(snapshot);
            inMemoryStorage.Clear();
            inMemoryStorage.Add(session);
            dataProvider.AddSnapshot(snapshot);

            subject.RemoveSnapshot(snapshot.SessionId, snapshot.Id);

            var resultFromMemory = inMemoryStorage.GetSnapshotSave(snapshot.SessionId, snapshot.Id);
            resultFromMemory.IsSuccessful.Should().BeFalse();

            var resultFromFile = dataProvider.GetSnapshotSave(snapshot.SessionId, snapshot.Id);
            resultFromFile.IsSuccessful.Should().BeFalse();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FindLastSnapshotByKey_SessionIdIsNull_ExpectedException()
        {
            subject.FindLastSnapshotByKey(null, "qwe");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FindLastSnapshotByKey_KeyIsNull_ExpectedException()
        {
            subject.FindLastSnapshotByKey("SessionId", null);
        }

        [TestMethod]
        public void FindLastSnapshotByKey_SessionExistInMemory_SessionShouldBeLoadedFromMemory()
        {
            var snapshot = new Snapshot
            {
                Id = "SnapshotId",
                Key = "key",
                Value = "value"
            };

            var session = new Session
            {
                Id = "SessionId"
            };

            session.AddSnapshot(snapshot);
            inMemoryStorage.Clear();
            inMemoryStorage.Add(session);
            dataProvider.RemoveSession(session.Id);

            var result = subject.FindLastSnapshotByKey(session.Id, "key");
            result.Should().NotBeNull();
            result.Id.Should().Be(snapshot.Id);
            result.Key.Should().Be("key");
            result.Value.Should().Be("value");
        }

        [TestMethod]
        public void FindLastSnapshotByKey_SessionDoesNotExistInMemoryAndExistInFile_SessionShouldBeLoadedFromFile()
        {
            var snapshot = new Snapshot
            {
                Id = "SnapshotId",
                Key = "key",
                Value = "value"
            };

            var session = new Session
            {
                Id = "SessionId"
            };

            session.AddSnapshot(snapshot);
            inMemoryStorage.Clear();
            dataProvider.AddSession(session);

            var result = subject.FindLastSnapshotByKey(session.Id, "key");

            result.Should().NotBeNull();
            result.Id.Should().Be(snapshot.Id);
            result.Key.Should().Be("key");
            result.Value.Should().Be("value");
        }
    }
}