# ApiControllerForwarder.HttpProxy
Proxy HTTP calls from an ApiController route to another API

### Usage

```c#
HttpProxy.ProxyAsync(request, rootUrl) 
```
```c#
HttpProxy.Proxy(request, rootUrl) 
```

### Example
```c#
    using ApiControllerForwarder.HttpProxy;
    
    [RoutePrefix("testController")]
    public class TestController : System.Web.Http.ApiController
    {
        [HttpGet]
        [Route("GetTest")]
        public async Task<HttpResponseMessage> GetTest()
        {
            return await HttpProxy.ProxyAsync(this.Request, "https://www.reddit.com/r/JUSTINBIEBER.json");
        }
    }
```


If you have query parameters it will append those to your root url.

So if you have the url `https://www.reddit.com/r/JUSTINBIEBER/?count=25&after=t3_46ate7` you only need to put `https://www.reddit.com/r/JUSTINBIEBER` as your `rootUrl`