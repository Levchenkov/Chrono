using Chrono.DataProviders;
using System;
using System.IO;
using System.Linq;
using Chrono.Common;
using Chrono.Exceptions;

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
            var result = GetSessionSave(sessionId);

            if (result.IsSuccessful)
            {
                return result.Value;
            }

            throw new SessionNotFoundException();
        }

        public FuncResult<Session> GetSessionSave(string sessionId)
        {
            Contract.NotNull<ArgumentNullException>(sessionId);

            var filePath = GetPathForSession(sessionId);
            var resultFromFile = ReadFromFile(filePath);
            if (resultFromFile.IsSuccessful)
            {
                var session = Deserialize<Session>(resultFromFile.Value);
                return session.AsFuncResult();
            }

            return FuncResult<Session>.Failed();
        }

        public Snapshot GetSnapshot(string sessionId, string snapshotId)
        {
            var result = GetSnapshotSave(sessionId, snapshotId);

            if (result.IsSuccessful)
            {
                return result.Value;
            }

            throw new SnapshotNotFoundException();
        }

        public FuncResult<Snapshot> GetSnapshotSave(string sessionId, string snapshotId)
        {
            Contract.NotNull<ArgumentNullException>(sessionId);
            Contract.NotNull<ArgumentNullException>(snapshotId);

            var filePath = GetPathForSnapshot(sessionId, snapshotId);
            var resultFromFile = ReadFromFile(filePath);

            if (resultFromFile.IsSuccessful)
            {
                var snapshot = Deserialize<Snapshot>(resultFromFile.Value);
                return snapshot.AsFuncResult();
            }
            
            return FuncResult<Snapshot>.Failed();
        }

        public void RemoveSession(string sessionId)
        {
            Contract.NotNull<ArgumentNullException>(sessionId);

            var filePath = GetPathForSession(sessionId);
            var directoryPath = Path.GetDirectoryName(filePath);
            File.Delete(filePath);

            if (!Directory.GetFiles(directoryPath).Any())
            {
                Directory.Delete(directoryPath);
            }
        }

        public void RemoveSnapshot(string sessionId, string snapshotId)
        {
            Contract.NotNull<ArgumentNullException>(sessionId);
            Contract.NotNull<ArgumentNullException>(snapshotId);

            var filePath = GetPathForSnapshot(sessionId, snapshotId);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        protected abstract string Serialize(object value);

        protected abstract TEntity Deserialize<TEntity>(string stringValue);

        protected void WriteToFile(string filePath, string content)
        {
            File.WriteAllText(filePath, content);
        }

        protected FuncResult<string> ReadFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return FuncResult.Failed<string>();
            }

            var content = File.ReadAllText(filePath);
            return content.AsFuncResult();
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
