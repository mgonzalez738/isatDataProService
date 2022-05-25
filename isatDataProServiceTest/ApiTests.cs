using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;
using System;
using System.Net.Http;
using Gie.IsatDataPro;
using System.Threading.Tasks;

namespace isatDataProServiceTests
{
    [TestClass]
    public class ApiTests
    {
        private MockHttpMessageHandler _mockHttpMessageHandler;
        private HttpClient _mockHttpClient;
        private IsatDataProService _service;
        private const string _baseApiUrl = "https://api.inmarsat.com/v1/idp/gateway/rest/";

        [TestInitialize]
        public void TestInitialize()
        {
            _mockHttpMessageHandler = new MockHttpMessageHandler();
            _mockHttpClient = _mockHttpMessageHandler.ToHttpClient();
            _service = new IsatDataProService("id", "pass", _mockHttpClient);
        }

        [TestMethod]
        public async Task TestGetInfoUtcTimeAsync()
        {
            const string relativeUrl = "info_utc_time.json/";
            _mockHttpMessageHandler.ResetExpectations();
            _mockHttpMessageHandler.Expect(_baseApiUrl + relativeUrl).Respond("application/json", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));

            var dt = await _service.GetInfoUtcTimeAsync();
            _mockHttpMessageHandler.VerifyNoOutstandingExpectation();

            Console.WriteLine("DateTime: " + dt.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss"));
            Assert.IsTrue(true);
        }

        [TestMethod]
        public async Task TestGetInfoErrorsAsync()
        {
            const string relativeUrl = "info_errors.json/";
            _mockHttpMessageHandler.ResetExpectations();
            _mockHttpMessageHandler.Expect(_baseApiUrl + relativeUrl).Respond("application/json", "[{\"ID\": 16904,\"Name\":\"ERR_NOT_AUTHORIZED_TO_USE_THE_MESSAGE\",\"Description\":\"Not authorized to use the message\"}]");

            var errorList = await _service.GetInfoErrorsAsync();
            _mockHttpMessageHandler.VerifyNoOutstandingExpectation();

            Console.WriteLine($"Errors count: {errorList.Count}");
            Assert.AreEqual(errorList.Count, 1);
        }

        [TestMethod]
        public async Task TestGetInfoVersionAsync()
        {
            const string relativeUrl = "info_version.json/";
            _mockHttpMessageHandler.ResetExpectations();
            _mockHttpMessageHandler.Expect(_baseApiUrl + relativeUrl).Respond("application/json", "4.2.1.3");

            var version = await _service.GetInfoVersionAsync();
            _mockHttpMessageHandler.VerifyNoOutstandingExpectation();

            Console.WriteLine($"Version: {version}");
            Assert.IsTrue(true);
        }
    }
}
