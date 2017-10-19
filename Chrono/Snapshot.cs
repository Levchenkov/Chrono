using System;
using System.Runtime.Serialization;

namespace Chrono
{
    [DataContract]
    public class Snapshot
    {
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
        public DateTime End
        {
            get;
            set;
        }

        [DataMember]
        public string Key
        {
            get;
            set;
        }

        [DataMember]
        public string Parameters
        {
            get;
            set;
        }

        [DataMember]
        public string Value
        {
            get;
            set;
        }

        [DataMember]
        public string SessionId
        {
            get;
            set;
        }

        public Session Session
        {
            get;
            set;
        }
    }
}
