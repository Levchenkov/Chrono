using System;
using System.Collections.Generic;

namespace Chrono
{
    public class Session
    {
        public Session()
        {
            Snapshots = new Dictionary<string, Snapshot>();
        }

        public string Id
        {
            get;
            set;
        }

        public DateTime Begin
        {
            get;
            set;
        } 

        public DateTime? End
        {
            get;
            set;
        }

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
