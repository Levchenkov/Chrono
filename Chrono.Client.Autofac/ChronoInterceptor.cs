using System;
using System.Linq;
using Castle.DynamicProxy;
using Chrono.Administration;
using Newtonsoft.Json;

namespace Chrono.Client.Autofac
{
    public class ChronoInterceptor : IInterceptor
    {
        private readonly IClientService clientService;
        private readonly IChronoSesssionIdHolder sessionIdHolder;


        public ChronoInterceptor(IClientService clientService, IChronoSesssionIdHolder sessionIdHolder)
        {
            this.clientService = clientService;
            this.sessionIdHolder = sessionIdHolder;
        }

        public void Intercept(IInvocation invocation)
        {
            var sessionId = sessionIdHolder.GetSessionId();

            if (sessionId == null)
            {
                invocation.Proceed();
            }
            else
            {
                ProceedWithChrono(invocation, sessionId);
            }
        }

        private void ProceedWithChrono(IInvocation invocation, string sessionId)
        {
            var mode = clientService.GetSessionMode(sessionId);

            if (mode == ChronoSessionMode.Play)
            {
                if (invocation.Method.ReturnType == typeof (void))
                {
                    invocation.Proceed();
                }   
                else
                {
                    var key = $"{invocation.TargetType.FullName}.{invocation.Method.Name}";

                    var snapshot = clientService.FindLastSnapshotByKey(sessionId, key);
                    var stringValue = snapshot.Value;
                    var value = JsonConvert.DeserializeObject(stringValue, invocation.Method.ReturnType);
                    invocation.ReturnValue = value;
                }
            }

            if (mode == ChronoSessionMode.Record)
            {
                var begin = DateTime.Now;
                invocation.Proceed();

                var key = $"{invocation.TargetType.FullName}.{invocation.Method.Name}";
                var parameterDictionary = invocation.Method.GetParameters().ToDictionary(x => x.Name, x => invocation.Arguments[x.Position]);
                var parameters = JsonConvert.SerializeObject(parameterDictionary);

                var snapshot = new ChronoSnapshot
                {
                    Id = Guid.NewGuid().ToString(),
                    Key = key,
                    SessionId = sessionId,
                    Parameters = parameters,
                    Begin = begin,
                    End = DateTime.Now
                };

                if (invocation.Method.ReturnType != typeof(void))
                {
                    var value = JsonConvert.SerializeObject(invocation.ReturnValue);

                    snapshot.Value = value;
                }

                clientService.Save(snapshot);
            }
        }
    }
}
