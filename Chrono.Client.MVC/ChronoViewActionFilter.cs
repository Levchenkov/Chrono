using System;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Chrono.Client.MVC
{
    public class ChronoViewActionFilter : ActionFilterAttribute
    {
        private const string SessionId = "SessionId";

        private DateTime Start; 

        public IChronoClientService ClientService { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            Start = DateTime.Now;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var mode = ClientService.GetSessionMode(SessionId);

            if (mode == ChronoSessionMode.Play)
            {
                var actionName = filterContext.ActionDescriptor.ActionName;
                var controllername = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

                var key = $"{controllername}.{actionName}";

                var state = ClientService.FindLastSnapshotByKey(SessionId, key);
                var stringValue = state.Value;

                var viewResult = (ViewResult)filterContext.Result;
                var converter = new ViewResultJsonConverter(filterContext.Controller, viewResult.Model.GetType());
                var value = JsonConvert.DeserializeObject<ViewResult>(stringValue, converter);
                value.ViewEngineCollection = viewResult.ViewEngineCollection;
                filterContext.Result = value;
            }

            if (mode == ChronoSessionMode.Record)
            {
                var actionName = filterContext.ActionDescriptor.ActionName;
                var controllername = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

                var key = $"{controllername}.{actionName}";
                var value = JsonConvert.SerializeObject(filterContext.Result);

                var snapshot = new ChronoSnapshot
                {
                    Id = Guid.NewGuid().ToString(),
                    Key = key,
                    Value = value,
                    SessionId = SessionId,
                    Begin = Start,
                    End = DateTime.Now
                };
                ClientService.Save(snapshot);
            }
        }
    }
}
