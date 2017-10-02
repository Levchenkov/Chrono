using System;

namespace Chrono
{
    public class Snapshot
    {
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

        public DateTime End
        {
            get;
            set;
        }

        public string Key
        {
            get;
            set;
        }

        public string Parameters
        {
            get;
            set;
        }

        public string Value
        {
            get;
            set;
        }

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
