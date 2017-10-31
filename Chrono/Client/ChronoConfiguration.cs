namespace Chrono.Client
{
    public class ChronoConfiguration
    {
        public bool IsChronoEnabled
        {
            get;
            set;
        }

        public ChronoClassConfiguration[] EnableChronoForClasses
        {
            get;
            set;
        }
    }
}
