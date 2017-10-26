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

        public void AddSnapshotOrReplaceIfNewer(Snapshot snaphot)
        {
            if (Snapshots.ContainsKey(snaphot.Id))
            {
                var existingSnapshot = Snapshots[snaphot.Id];

                if (existingSnapshot.Begin <= snaphot.Begin)
                {
                    Snapshots.Remove(existingSnapshot.Id);
                    AddSnapshot(snaphot);
                }
            }
            else
            {
                AddSnapshot(snaphot);
            }
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
            if (Snapshots.ContainsKey(snapshotId))
            {
                Snapshots.Remove(snapshotId);
            }
        }
    }
}
