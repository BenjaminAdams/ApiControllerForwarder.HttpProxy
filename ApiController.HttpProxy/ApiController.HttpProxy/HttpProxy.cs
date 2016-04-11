using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ApiControllerForwarder.HttpProxy
{
    public static class HttpProxy
    {
        public static async Task<HttpResponseMessage> ProxyAsync(HttpRequestMessage req, string urlRoot)
        {
            using (var client = new HttpClient())
            {
                req.RequestUri = new Uri(urlRoot + req.RequestUri.Query);

                if (req.Method == HttpMethod.Get)
                {
                    req.Content = null;
                }
                else
                {
                    await GetHttpBody(req);
                }

                return await client.SendAsync(req);
            }
        }

        private static async Task GetHttpBody(HttpRequestMessage req)
        {
            var stream = await req.Content.ReadAsStreamAsync();
            stream.Seek(0, System.IO.SeekOrigin.Begin); //sets the post body stream read position back to start
            var reader = new StreamReader(stream);  //we could do a req.Content.ReadAsStringAsync() but that would load it into memory a 2nd time, instead read from current stream
            var httpBody = reader.ReadToEnd();
            var contentType = GetContentType(req.Content.Headers);
            req.Content = new StringContent(httpBody); //we have to re-read the post body because if you have any params in the ApiController it will "eat" the value in the Request.Content
            req.Content.Headers.ContentType = contentType;
        }

        private static MediaTypeHeaderValue GetContentType(HttpContentHeaders headers)
        {
            var defaultContentType = new MediaTypeHeaderValue("application/json");
            if (headers == null || !headers.Any()) return defaultContentType;

            return headers.ContentType;
        }

        public static HttpResponseMessage Proxy(HttpRequestMessage req, string url)
        {
            var task = Task.Run(async () => await ProxyAsync(req, url));
            task.Wait();
            return task.Result;
        }
    }
}