using System;

namespace Chrono.Common
{
    public static class Contract
    {
        public static void NotNull<TException>(object value) where TException : Exception
        {
            if(value == null)
            {
                var exception = Activator.CreateInstance<TException>();
                throw exception;
            }
        }            
    }
}
