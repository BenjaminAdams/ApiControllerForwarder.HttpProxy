using ApiControllerForwarder.HttpProxy.TestController.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ApiControllerForwarder.HttpProxy.UnitTests
{
    [TestClass]
    public class HttpProxyTests
    {
        private string baseUrl = "http://localhost/ApiController.HttpProxy.TestController/testController/";

        [TestInitialize]
        public void Init()
        {
        }

        [TestMethod]
        public async Task GET_QryParamTest()
        {
            var url = baseUrl + "GetTestWithQryParam?someParam=HeyThere!";

            var req = new HttpRequestMessage(HttpMethod.Get, url);

            var resp = HttpProxy.Proxy(req, baseUrl + "GetTestWithQryParam");
            var result = await resp.Content.ReadAsStringAsync();
            Assert.AreEqual("you just sent HeyThere!", result);
        }

        [TestMethod]
        public async Task GET_NoQryParamTest()
        {
            var url = baseUrl + "GetTest2";

            var req = new HttpRequestMessage(HttpMethod.Get, url);

            var resp = HttpProxy.Proxy(req, baseUrl + "GetTest2");
            var result = await resp.Content.ReadAsStringAsync();
            Assert.AreEqual("success", result);
        }

        [TestMethod]
        public async Task POST_ComplicatedObj()
        {
            var url = baseUrl + "PostTest2";

            var input = new SomeComplicatedObj
            {
                Age = 25,
                Name = "Benjamin Adams"
            };
            var inputAsJsonStr = JsonConvert.SerializeObject(input);

            var req = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(inputAsJsonStr, Encoding.UTF8, "application/json")
            };

            var resp = HttpProxy.Proxy(req, baseUrl + "PostTest2");
            var result = await resp.Content.ReadAsStringAsync();
            //this controller will add +5 to the age
            Assert.AreEqual("{\"Name\":\"Benjamin Adams\",\"Age\":30}", result);
        }

        [TestMethod]
        public async Task POST_QryStr()
        {
            var url = baseUrl + "PostTestWithQryParam?someParam44=hiFriend";

            var req = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent("", Encoding.UTF8, "application/json")
            };

            var resp = HttpProxy.Proxy(req, baseUrl + "PostTestWithQryParam");
            var result = await resp.Content.ReadAsStringAsync();
            Assert.AreEqual("you just sent hiFriend", result);
        }
    }
}