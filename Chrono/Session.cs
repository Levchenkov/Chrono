using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

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
            if (!Snapshots.ContainsKey(snapshotId))
            {
                throw new KeyNotFoundException();
            }

            return Snapshots[snapshotId];
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
