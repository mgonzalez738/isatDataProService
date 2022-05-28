using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Dynamic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;
using Newtonsoft.Json;
using Gie.IsatDataPro;

namespace isatDataProTests
{
    [TestClass]
    public class IsatDataProMgsApiTests
    {
        private MockHttpMessageHandler _mockHttpMessageHandler;
        private HttpClient _mockHttpClient;
        private const string _baseApiUrl = IsatDataProMgsApi.BaseUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockHttpMessageHandler = new MockHttpMessageHandler();
            _mockHttpClient = _mockHttpMessageHandler.ToHttpClient();
            _mockHttpClient.BaseAddress = new Uri(_baseApiUrl);
        }

        [TestMethod]
        public async Task TestGetInfoUtcTimeAsync()
        {
            const string relativeUrl = "info_utc_time.json/";
            _mockHttpMessageHandler.ResetExpectations();
            _mockHttpMessageHandler.Expect(_baseApiUrl + relativeUrl).Respond("application/json", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));

            var dt = await IsatDataProMgsApi.GetInfoUtcTimeAsync(_mockHttpClient);
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

            var errorList = await IsatDataProMgsApi.GetInfoErrorsAsync(_mockHttpClient); 
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

            var version = await IsatDataProMgsApi.GetInfoVersionAsync(_mockHttpClient);
            _mockHttpMessageHandler.VerifyNoOutstandingExpectation();

            Console.WriteLine($"Version: {version}");
            Assert.IsTrue(true);
        }

        [TestMethod]
        public async Task TastGetReturnMessagesAsync()
        {
            // Element 1
            dynamic element1 = new ExpandoObject();
            element1.Index = 0;
            element1.Fields = null;

            // Field 1
            dynamic field1 = new ExpandoObject();
            field1.Name = "string";
            field1.Value = "string";
            field1.Elements = new object[] { element1 };
            field1.Type = "unsignedint";

            // Payload 1
            dynamic payload1 = new ExpandoObject();
            payload1.Name = "pingModem";
            payload1.SIN = 0;
            payload1.MIN = 0;
            payload1.IsForward = true;
            payload1.Fields = new object[] { field1 };

            // Test message 1
            dynamic returnMessage1 = new ExpandoObject();
            returnMessage1.ID = 940439;
            returnMessage1.MessageUTC = DateTime.Now;
            returnMessage1.MobileID = "01000056SKY1A95";
            returnMessage1.ReceiveUTC = DateTime.Now;
            returnMessage1.RegionName = "EMEARB17";
            returnMessage1.SIN = 0;
            returnMessage1.OTAMessageSize = 15;
            returnMessage1.Payload = payload1;
            returnMessage1.RawPayload = new int[] { 0, 1, 2 };

            // Test response
            dynamic returnMessageResult = new ExpandoObject();
            returnMessageResult.ErrorID = 0;
            returnMessageResult.More = false;
            returnMessageResult.NextStartUTC = DateTime.Now;
            returnMessageResult.NextStartID = 12345679;
            returnMessageResult.Messages = new object[] { returnMessage1 };
            string returnMessageResultString = JsonConvert.SerializeObject(returnMessageResult);

            const string relativeUrl = "get_return_messages.json/";
            _mockHttpMessageHandler.ResetExpectations();
            _mockHttpMessageHandler.Expect(_baseApiUrl + relativeUrl).Respond("application/json", returnMessageResultString);

            var result = await IsatDataProMgsApi.GetReturnMessagesAsync(_mockHttpClient, "id", "pass", true, true, 1);
            _mockHttpMessageHandler.VerifyNoOutstandingExpectation();

            Console.WriteLine($"Response:");
            Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
            Assert.IsTrue(true);
        }
    }
}
