using System;

namespace Chrono.Client
{
    public class ChronoSnapshot
    {
        public string Id
        {
            get;
            set;
        }

        public string SessionId
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
    }
}
