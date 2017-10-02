using Chrono.DataProviders;
using System;
using System.IO;

namespace Chrono.FileSystem.DataProviders
{
    public abstract class FileDataProvider : IDataProvider
    {
        public void AddSession(Session session)
        {
            var fileName = GetFileNameForSession(session.Id);
            var serializedValue = Serialize(session);

            WriteToFile(fileName, serializedValue);
        }

        public void AddSnapshot(Snapshot snapshot)
        {
            throw new NotImplementedException();
        }

        public Session GetSession(string sessionId)
        {
            var fileName = GetFileNameForSession(sessionId);
            var serializedValue = ReadFromFile(fileName);
            var session = Deserialize<Session>(serializedValue);
            return session;
        }

        public Snapshot GetSnapshot(string sessionId, string snapshotId)
        {
            throw new NotImplementedException();
        }

        public void RemoveSession(string sessionId)
        {
            var fileName = GetFileNameForSession(sessionId);
            RemoveFile(fileName);
        }

        public void RemoveSnapshot(string sessionId, string snapshotId)
        {
            throw new NotImplementedException();
        }

        protected abstract string Serialize(object value);

        protected abstract TEntity Deserialize<TEntity>(string stringValue);

        protected void WriteToFile(string fileName, string content)
        {
            var direcotryPath = GetDirectoryPath();
            var filePath = Path.Combine(direcotryPath, fileName);
            File.WriteAllText(filePath, content);
        }

        protected string ReadFromFile(string fileName)
        {
            var direcotryPath = GetDirectoryPath();
            var filePath = Path.Combine(direcotryPath, fileName);
            var content = File.ReadAllText(filePath);
            return content;
        }

        protected void RemoveFile(string fileName)
        {
            var direcotryPath = GetDirectoryPath();
            var filePath = Path.Combine(direcotryPath, fileName);
            File.Delete(filePath);
        }

        protected string GetDirectoryPath()
        {
            return Path.Combine(Environment.CurrentDirectory, "Sessions");
        }

        private string GetFileNameForSession(string sessionId)
        {
            return $"Session:{sessionId}.txt";
        }
    }
}
