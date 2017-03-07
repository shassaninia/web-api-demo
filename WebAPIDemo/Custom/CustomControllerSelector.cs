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

            string customHeader = "X-StudentService-Version";

            if(request.Headers.Contains(customHeader))
            {
                //get version number from header
                versionNumber = request.Headers.GetValues(customHeader).FirstOrDefault();

                if(versionNumber.Contains(","))
                {
                    versionNumber = versionNumber.Substring(0, versionNumber.IndexOf(","));
                }
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