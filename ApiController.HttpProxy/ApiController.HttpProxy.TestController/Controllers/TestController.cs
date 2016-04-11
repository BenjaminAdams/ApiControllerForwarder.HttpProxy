using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ApiController.HttpProxy.TestController.Controllers
{
    [RoutePrefix("testController")]
    public class TestController : System.Web.Http.ApiController
    {
        [HttpGet]
        [Route("GetTest")]
        public async Task<HttpResponseMessage> GetTest()
        {
            return await HttpProxy.ProxyAsync(this.Request, "https://www.reddit.com/r/JUSTINBIEBER.json");
        }

        [HttpPost]
        [Route("PostTest")]
        public async Task<HttpResponseMessage> PostTest(dynamic input)
        {
            return await HttpProxy.ProxyAsync(this.Request, "http://localhost/test");
        }
    }
}