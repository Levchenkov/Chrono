using System;
using Castle.DynamicProxy;
using Newtonsoft.Json;

namespace Chrono.Client.Autofac
{
    public class ChronoInterceptor : IInterceptor
    {
        private readonly IClientService clientService;
        private const string SessionId = "SessionId";

        public ChronoInterceptor(IClientService clientService)
        {
            this.clientService = clientService;
        }

        public void Intercept(IInvocation invocation)
        {
            var mode = clientService.GetSessionMode(SessionId);

            if (mode == ChronoSessionMode.Play)
            {
                if (invocation.Method.ReturnType != typeof(void))
                {
                    var key = $"{invocation.TargetType.FullName}.{invocation.Method.Name}";
                    
                    var snapshot = clientService.FindLastSnapshotByKey(SessionId, key);
                    var stringValue = snapshot.Value;
                    var value = JsonConvert.DeserializeObject(stringValue, invocation.Method.ReturnType);
                    invocation.ReturnValue = value;
                }
            }

            if (mode == ChronoSessionMode.Record)
            {
                var begin = DateTime.Now;
                invocation.Proceed();

                if (invocation.Method.ReturnType != typeof(void))
                {
                    var key = $"{invocation.TargetType.FullName}.{invocation.Method.Name}";
                    var value = JsonConvert.SerializeObject(invocation.ReturnValue);

                    var snapshot = new ChronoSnapshot
                    {
                        Id = Guid.NewGuid().ToString(),
                        Key = key,
                        SessionId = SessionId,
                        Value = value,
                        Begin = begin,
                        End = DateTime.Now
                    };
                    clientService.Save(snapshot);
                }
            }
        }
    }
}
