namespace Chrono.Host
{
    public class HostSettings
    {
        /// <summary>
        /// Creates session if session does not exist.
        /// </summary>
        public bool IsSessionAutoCreate
        {
            get;
            set;
        }

        /// <summary>
        /// Close session every time as session has been updated.
        /// </summary>
        public bool IsSessionAutoClose
        {
            get;
            set;
        }

        /// <summary>
        /// Save data in memory after read from file.
        /// </summary>
        public bool IsEnabledFileCache
        {
            get;
            set;
        }
    }
}
