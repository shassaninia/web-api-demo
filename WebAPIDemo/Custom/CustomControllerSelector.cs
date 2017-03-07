using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace WebAPIDemo.Custom
{
    //DefaultHttpControllerSelector is responsible for determining which web api controller to use.
    //We can override this
    public class CustomControllerSelector : DefaultHttpControllerSelector
    {
        HttpConfiguration _config;
        public CustomControllerSelector(HttpConfiguration config) : base(config)
        {
            _config = config;
        }

        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            //Will get all web api controllers
            var controllers = GetControllerMapping();

            //Will get all route data for a controller
            var routeData = request.GetRouteData();

            //Get controller  name from routedata
            var controllerName = routeData.Values["controller"].ToString();

            var versionNumber = "1";

            //get query string part of url
            var versionQueryString = HttpUtility.ParseQueryString(request.RequestUri.Query);

            if (versionQueryString["v"] != null)
            {
                versionNumber = versionQueryString["v"];
            }

            if (versionNumber == "1")
            {
                controllerName = controllerName + "V1";
            }
            else
            {
                controllerName = controllerName + "V2";
            }

            HttpControllerDescriptor controllerDescriptor;

            if (controllers.TryGetValue(controllerName, out controllerDescriptor))
            {
                return controllerDescriptor;
            }

            return null;


        }
    }
}