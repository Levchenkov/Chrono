using Chrono.DataProviders;
using System;
using System.IO;
using Chrono.Common;

namespace Chrono.FileSystem.DataProviders
{
    public abstract class FileDataProvider : IDataProvider
    {
        public void AddSession(Session session)
        {
            var filePath = GetPathForSession(session.Id);
            var serializedValue = Serialize(session);

            WriteToFile(filePath, serializedValue);
        }

        public void AddSnapshot(Snapshot snapshot)
        {
            var filePath = GetPathForSnapshot(snapshot.SessionId, snapshot.Id);
            var serializedValue = Serialize(snapshot);

            WriteToFile(filePath, serializedValue);
        }

        public Session GetSession(string sessionId)
        {
            Contract.NotNull<ArgumentNullException>(sessionId);

            var filePath = GetPathForSession(sessionId);
            var serializedValue = ReadFromFile(filePath);
            var session = Deserialize<Session>(serializedValue);
            return session;
        }

        public Snapshot GetSnapshot(string sessionId, string snapshotId)
        {
            Contract.NotNull<ArgumentNullException>(sessionId);
            Contract.NotNull<ArgumentNullException>(snapshotId);

            var filePath = GetPathForSnapshot(sessionId, snapshotId);
            var serializedValue = ReadFromFile(filePath);
            var snapshot = Deserialize<Snapshot>(serializedValue);
            return snapshot;
        }

        public void RemoveSession(string sessionId)
        {
            Contract.NotNull<ArgumentNullException>(sessionId);

            var filePath = GetPathForSession(sessionId);
            var directoryPath = Path.GetDirectoryName(filePath);
            Directory.Delete(directoryPath);
        }

        public void RemoveSnapshot(string sessionId, string snapshotId)
        {
            Contract.NotNull<ArgumentNullException>(sessionId);
            Contract.NotNull<ArgumentNullException>(snapshotId);

            var filePath = GetPathForSnapshot(sessionId, snapshotId);
            File.Delete(filePath);
        }

        protected abstract string Serialize(object value);

        protected abstract TEntity Deserialize<TEntity>(string stringValue);

        protected void WriteToFile(string filePath, string content)
        {
            File.WriteAllText(filePath, content);
        }

        protected string ReadFromFile(string filePath)
        {
            var content = File.ReadAllText(filePath);
            return content;
        }

        private string GetRootDirectoryPath()
        {
            return Path.Combine(Environment.CurrentDirectory, "Sessions");
        }

        private string GetPathForSession(string sessionId)
        {
            var fileName = GetFileNameForSession(sessionId);
            var rootPath = GetRootDirectoryPath();
            var directoryPath = Path.Combine(rootPath, $"Session.{sessionId}");
            Directory.CreateDirectory(directoryPath);
            var filePath = Path.Combine(directoryPath, fileName);
            return filePath;
        }

        private string GetPathForSnapshot(string sessionId, string snapshotId)
        {
            var fileName = GetFileNameForSnapshot(snapshotId);
            var rootPath = GetRootDirectoryPath();
            var directoryPath = Path.Combine(rootPath, $"Session.{sessionId}");
            Directory.CreateDirectory(directoryPath);
            var filePath = Path.Combine(directoryPath, fileName);
            return filePath;
        }

        private string GetFileNameForSession(string sessionId)
        {
            return $"Session.{sessionId}.txt";
        }

        private string GetFileNameForSnapshot(string snapshotId)
        {
            return $"Snapshot.{snapshotId}.txt";
        }
    }
}
