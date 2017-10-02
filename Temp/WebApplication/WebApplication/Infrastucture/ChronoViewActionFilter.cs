using Newtonsoft.Json;
using System.Web.Mvc;
using WebApplication.ChronoService;

namespace WebApplication.Infrastucture
{
    public class ChronoViewActionFilter : ActionFilterAttribute
    {
        public IStateService StateService { get; set; } 

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var mode = StateService.GetMode();

            if (mode == ChronoMode.Read)
            {
                var actionName = filterContext.ActionDescriptor.ActionName;
                var controllername = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

                var key = $"{controllername}.{actionName}";
                var t = StateService.Last();
                var state = StateService.Get(t, key);
                var stringValue = state.Value;

                var viewResult = (ViewResult)filterContext.Result;
                var converter = new ViewResultJsonConverter(filterContext.Controller, viewResult.Model.GetType());
                var value = JsonConvert.DeserializeObject<ViewResult>(stringValue, converter);
                value.ViewEngineCollection = viewResult.ViewEngineCollection;
                filterContext.Result = value;
            }

            if (mode == ChronoMode.Write)
            {
                var t = StateService.CreateTimestamp();
                var actionName = filterContext.ActionDescriptor.ActionName;
                var controllername = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

                var key = $"{controllername}.{actionName}";
                var value = JsonConvert.SerializeObject(filterContext.Result);

                var state = new State
                {
                    Key = key,
                    StateTimestamp = t,
                    Value = value
                };
                StateService.Save(state);
            }                
        }
    }    
}