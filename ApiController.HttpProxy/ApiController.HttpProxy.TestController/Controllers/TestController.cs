using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ApiControllerForwarder.HttpProxy.TestController.Controllers
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

        [HttpGet]
        [Route("GetSyncTest")]
        public HttpResponseMessage GetSyncTest(dynamic input)
        {
            return HttpProxy.Proxy(this.Request, "https://www.reddit.com/r/JUSTINBIEBER.json");
        }

        [HttpPost]
        [Route("PostTest")]
        public async Task<HttpResponseMessage> PostTest(dynamic input)
        {
            return await HttpProxy.ProxyAsync(this.Request, "http://localhost/test");
        }

        [HttpGet]
        [Route("GetTestWithQryParam")]
        public string GetTestWithQryParam(string someParam)
        {
            return "you just sent " + someParam;
        }

        [HttpGet]
        [Route("GetTest2")]
        public string GetTest2()
        {
            return "success!";
        }

        [HttpGet]
        [Route("PostTest2")]
        public SomeComplicatedObj PostTest2(SomeComplicatedObj input)
        {
            input.Age += 5;
            return input;
        }

        [HttpGet]
        [Route("PostTestWithQryParam")]
        public string PostTestWithQryParam([FromUri]string someParam44)
        {
            return "you just sent " + someParam44;
        }
    }

    public class SomeComplicatedObj
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}