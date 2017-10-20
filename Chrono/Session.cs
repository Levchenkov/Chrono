using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Chrono.Exceptions;

namespace Chrono
{
    [DataContract]
    public class Session
    {
        public Session()
        {
            Snapshots = new Dictionary<string, Snapshot>();
        }

        [DataMember]
        public string Id
        {
            get;
            set;
        }

        [DataMember]
        public DateTime Begin
        {
            get;
            set;
        }

        [DataMember]
        public DateTime? End
        {
            get;
            set;
        }

        [DataMember]
        public IDictionary<string, Snapshot> Snapshots
        {
            get;
            set;
        }

        public void AddSnapshot(Snapshot snapshot)
        {
            Snapshots.Add(snapshot.Id, snapshot);
            snapshot.Session = this;
            snapshot.SessionId = Id;
        }

        public Snapshot GetSnapshot(string snapshotId)
        {
            var result = GetSnapshotSave(snapshotId);

            if (result.IsSuccessful)
            {
                return result.Value;
            }

            throw new SnapshotNotFoundException();
        }

        public FuncResult<Snapshot> GetSnapshotSave(string snapshotId)
        {
            if (!Snapshots.ContainsKey(snapshotId))
            {
                return FuncResult.Failed<Snapshot>();
            }

            return Snapshots[snapshotId].AsFuncResult();
        } 

        public void RemoveSnapshot(string snapshotId)
        {
            if (!Snapshots.ContainsKey(snapshotId))
            {
                throw new KeyNotFoundException();
            }

            Snapshots.Remove(snapshotId);
        }
    }
}
