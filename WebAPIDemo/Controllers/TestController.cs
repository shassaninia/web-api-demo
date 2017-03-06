using System.Web.Http;

namespace WebAPIDemo.Controllers
{
    public class TestController : ApiController
    {
        public string Get()
        {
            return "Hello from TestController";
        }
    }
}
