using System;

namespace Chrono.Client
{
    public class ChronoSession
    {
        public string Id
        {
            get;
            set;
        }

        public DateTime StartDate
        {
            get;
            set;
        }

        public DateTime? StopDate
        {
            get;
            set;
        }

        public ChronoSessionMode Mode
        {
            get;
            set;
        }
    }
}
