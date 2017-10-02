using System.Web.Mvc;
using WebApplication.ChronoService;
using WebApplication.Infrastucture;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IService service;
        private readonly IStateService stateService;

        public HomeController(IService service, IStateService stateService)
        {
            this.service = service;
            this.stateService = stateService;
        }

        public ActionResult Index()
        {
            return View();
        }

        // interface interceptor
        // добавить зависимость параметров
        [ChronoViewActionFilter]
        public ActionResult Index2(string param)
        {
            stateService.EnableWriteMode();

            service.Action();
            service.Action1(param);
            var result1 = service.Func();
            var result2 = service.Func1(param);

            stateService.EnableReadMode();

            var result11 = service.Func();
            var result21 = service.Func1(param);

            return View("Index", (object)param);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}