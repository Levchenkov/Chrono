using System;

namespace Chrono.Client.Autofac
{
    public class ChronoInterceptorAttribute : Attribute
    {
        public ChronoInterceptorAttribute(bool isEnabled = true)
        {
            IsEnabled = isEnabled;
        }

        public bool IsEnabled
        {
            get;
        }
    }
}