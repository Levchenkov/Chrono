using System;

namespace Chrono.Host
{
    public class HostHolder
    {
        public static IHost Host
        {
            get;
            private set;
        }

        public static void SetHostIfHostIsNull(Func<IHost> function)
        {
            if(Host == null)
            {
                Host = function();
            }
        }
    }
}
