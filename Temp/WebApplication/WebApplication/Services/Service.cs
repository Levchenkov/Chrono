using System;

namespace WebApplication.Services
{
    public class Service : IService
    {
        public void Action()
        {            
        }

        public void Action1(string value)
        {            
        }

        public string Func()
        {
            return Guid.NewGuid().ToString();
        }

        public string Func1(string value)
        {
            return value + Guid.NewGuid().ToString();
        }
    }
}