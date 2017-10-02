using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Web.Mvc;

namespace WebApplication.Infrastucture
{
    public class ViewResultJsonConverter : JsonConverter
    {
        private ControllerBase controller;

        private Type modelType;

        public ViewResultJsonConverter(ControllerBase controller, Type modelType)
        {
            this.controller = controller;
            this.modelType = modelType;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(ViewResult);
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jobject = JObject.Load(reader);

            object model = null;
            string viewName = string.Empty;
            string masterName = string.Empty;
            ViewDataDictionary viewData = null;
            TempDataDictionary tempData = null;

            if (jobject["Model"] != null)
            {
                model = jobject["Model"].ToObject(modelType);
            }

            if (jobject["ViewName"] != null)
            {
                viewName = jobject["ViewName"].ToObject<string>();
            }

            if (jobject["MasterName"] != null)
            {
                masterName = jobject["MasterName"].ToObject<string>();
            }

            //if(jobject["ViewData"] != null)
            //{
            //    viewData = jobject["ViewData"].ToObject<ViewDataDictionary>();
            //}

            //if (jobject["TempData"] != null)
            //{
            //    tempData = jobject["TempData"].ToObject<TempDataDictionary>();
            //}

            var viewResult = CreateActionResult(model, viewName, masterName, viewData, tempData);

            return viewResult;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        private ActionResult CreateActionResult(object model, string viewName, string masterName, ViewDataDictionary viewData, TempDataDictionary tempData)
        {
            if (model != null)
            {
                controller.ViewData.Model = model;
            }
            ViewResult viewResult = new ViewResult();
            viewResult.ViewName = viewName;
            viewResult.MasterName = masterName;
            viewResult.ViewData = viewData ?? controller.ViewData;
            viewResult.TempData = tempData ?? controller.TempData;
            return viewResult;
        }
    }
}