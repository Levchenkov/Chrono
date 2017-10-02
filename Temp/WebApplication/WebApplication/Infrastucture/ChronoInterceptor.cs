using Castle.DynamicProxy;
using Newtonsoft.Json;
using WebApplication.ChronoService;

namespace WebApplication.Infrastucture
{
    public class ChronoInterceptor : IInterceptor
    {
        private readonly IStateService stateService;

        public ChronoInterceptor(IStateService stateService)
        {
            this.stateService = stateService;
        }

        public void Intercept(IInvocation invocation)
        {
            var mode = stateService.GetMode();            

            if(mode == ChronoMode.Read)
            {
                if (invocation.Method.ReturnType != typeof(void))
                {
                    var key = $"{invocation.TargetType.FullName}.{invocation.Method.Name}";
                    var t = stateService.Last();
                    var state = stateService.Get(t, key);
                    var stringValue = state.Value;
                    var value = JsonConvert.DeserializeObject(stringValue, invocation.Method.ReturnType);
                    invocation.ReturnValue = value;
                }
            }

            if (mode == ChronoMode.Write)
            {
                invocation.Proceed();

                if (invocation.Method.ReturnType != typeof(void))
                {
                    var t = stateService.CreateTimestamp();

                    var key = $"{invocation.TargetType.FullName}.{invocation.Method.Name}";
                    var value = JsonConvert.SerializeObject(invocation.ReturnValue);

                    var state = new State
                    {
                        Key = key,
                        StateTimestamp = t,
                        Value = value
                    };
                    stateService.Save(state);
                }
            }            
        }
    }
}