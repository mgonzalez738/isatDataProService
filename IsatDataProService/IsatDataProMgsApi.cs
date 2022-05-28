using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Gie.IsatDataPro.Extensions;
using Gie.IsatDataPro.Models;

namespace Gie.IsatDataPro
{
    /// <summary>
    /// IsatData Pro Message Gateway Service (MGS) API client functions.
    /// </summary>
    public static class IsatDataProMgsApi
    {
        #region Members

        public const string BaseUrl = "https://api.inmarsat.com/v1/idp/gateway/rest/";
        private static readonly HttpClient _client = new()
        {
            BaseAddress = new Uri(BaseUrl)
        };
        
        #endregion

        #region Public Methods

        /// <summary>
        /// Gets current MGS time.
        /// </summary>
        /// <returns>Current MGS datetime.</returns>
        public static async Task<DateTime> GetInfoUtcTimeAsync()
        {
            try { return await _getInfoUtcTimeAsync(_client); }
            catch { throw; }
        }

        /// <summary>
        /// Gets current MGS time. 
        /// </summary>
        /// <param name="client">HttpClient instance to use.</param>
        /// <returns>Current MGS datetime.</returns>
        public static async Task<DateTime> GetInfoUtcTimeAsync(HttpClient client)
        {
            try { return await _getInfoUtcTimeAsync(client); }
            catch { throw; }
        }

        /// <summary>
        /// Gets a list of supported error definitions.
        /// </summary>
        /// <returns>List of supported error definitions.</returns>
        public static async Task<List<ErrorInfo>> GetInfoErrorsAsync()
        {
            try { return await _getInfoErrorsAsync(_client); }
            catch { throw; }
        }

        /// <summary>
        /// Gets a list of supported error definitions.
        /// </summary>
        /// <param name="client">HttpClient instance to use.</param>
        /// <returns>List of supported error definitions.</returns>
        public static async Task<List<ErrorInfo>> GetInfoErrorsAsync(HttpClient client)
        {
            try { return await _getInfoErrorsAsync(client); }
            catch { throw; }
        }

        /// <summary>
        /// Gets current MGS version. 
        /// </summary>
         /// <returns>Current MGS version.</returns>
        public static async Task<string> GetInfoVersionAsync()
        {
            try { return await _getInfoVersionAsync(_client); }
            catch { throw; }
        }

        /// <summary>
        /// Gets current MGS version. 
        /// </summary>
        /// <param name="client">HttpClient instance to use.</param>
        /// <returns>Current MGS version.</returns>
        public static async Task<string> GetInfoVersionAsync(HttpClient client)
        {
            try { return await _getInfoVersionAsync(client); }
            catch { throw; }
        }

        /// <summary>
        /// Retrieves Mobile-Originated messages (up to 500) based on a start-time.
        /// </summary>
        /// <param name="accessId">MGS Mailbox access Id.</param>
        /// <param name="accessPassword">MGS Mailbox access password.</param>
        /// <param name="includeRawPayload">Returns an array of bytes even if decoded payload is available.RawPayload is returned automatically if no Message Definition File defines payload decoding for the Mailbox.</param>
        /// <param name="includeType">Includes data type when using a Message Definition File on the Mailbox.</param>
        /// <param name="startUtc">Messages sent from this start time reference (UTC) or later will be retrieved.</param>
        /// <returns>GetReturnMessagesResult object that holds API response.</returns>
        public static async Task<GetReturnMessagesResult> GetReturnMessagesAsync(string accessId, string accessPassword, bool includeRawPayload, bool includeType, DateTime startUtc)
        {
            try { return await _getReturnMessagesAsync(_client, accessId, accessPassword, includeRawPayload, includeType, startUtc); }
            catch { throw; }
        }

        /// <summary>
        /// Retrieves Mobile-Originated messages (up to 500) based on a start-time.
        /// </summary>
        /// <param name="accessId">MGS Mailbox access Id.</param>
        /// <param name="accessPassword">MGS Mailbox access password.</param>
        /// <param name="includeRawPayload">Returns an array of bytes even if decoded payload is available.RawPayload is returned automatically if no Message Definition File defines payload decoding for the Mailbox.</param>
        /// <param name="includeType">Includes data type when using a Message Definition File on the Mailbox.</param>
        /// <param name="startUtc">Messages sent from this start time reference (UTC) or later will be retrieved.</param>
        /// <param name="mobileId">Filter to retrieve only messages sent by this Mobile ID.</param>
        /// <returns>GetReturnMessagesResult object that holds API response.</returns>
        public static async Task<GetReturnMessagesResult> GetReturnMessagesAsync(string accessId, string accessPassword, bool includeRawPayload, bool includeType, DateTime startUtc, string mobileId)
        {
            try { return await _getReturnMessagesAsync(_client, accessId, accessPassword, includeRawPayload, includeType, startUtc, mobileId); }
            catch { throw; }
        }

        /// <summary>
        /// Retrieves Mobile-Originated messages (up to 500) based on ID of a previously retrieved message.
        /// </summary>
        /// <param name="accessId">MGS Mailbox access Id.</param>
        /// <param name="accessPassword">MGS Mailbox access password.</param>
        /// <param name="includeRawPayload">Returns an array of bytes even if decoded payload is available.RawPayload is returned automatically if no Message Definition File defines payload decoding for the Mailbox.</param>
        /// <param name="includeType">Includes data type when using a Message Definition File on the Mailbox.</param>
        /// <param name="fromId">A unique ID of a previously retrieved message, typically derived from NextStartID parameter of a prior get_return_messages operation where More messages are available to retrieve.</param>
        /// <returns>GetReturnMessagesResult object that holds API response.</returns>
        public static async Task<GetReturnMessagesResult> GetReturnMessagesAsync(string accessId, string accessPassword, bool includeRawPayload, bool includeType, int fromId)
        {
            try { return await _getReturnMessagesAsync(_client, accessId, accessPassword, includeRawPayload, includeType, fromId); }
            catch { throw; }
        }

        /// <summary>
        /// Retrieves Mobile-Originated messages (up to 500) based on ID of a previously retrieved message.
        /// </summary>
        /// <param name="accessId">MGS Mailbox access Id.</param>
        /// <param name="accessPassword">MGS Mailbox access password.</param>
        /// <param name="includeRawPayload">Returns an array of bytes even if decoded payload is available.RawPayload is returned automatically if no Message Definition File defines payload decoding for the Mailbox.</param>
        /// <param name="includeType">Includes data type when using a Message Definition File on the Mailbox.</param>
        /// <param name="fromId">A unique ID of a previously retrieved message, typically derived from NextStartID parameter of a prior get_return_messages operation where More messages are available to retrieve.</param>
        /// <param name="mobileId">Filter to retrieve only messages sent by this Mobile ID.</param>
        /// <returns>GetReturnMessagesResult object that holds API response.</returns>
        public static async Task<GetReturnMessagesResult> GetReturnMessagesAsync(string accessId, string accessPassword, bool includeRawPayload, bool includeType, int fromId, string mobileId)
        {
            try { return await _getReturnMessagesAsync(_client, accessId, accessPassword, includeRawPayload, includeType, fromId, mobileId); }
            catch { throw; }
        }

        /// <summary>
        /// Retrieves Mobile-Originated messages (up to 500) based on a start-time.
        /// </summary>
        /// <param name="client">HttpClient instance to use.</param>
        /// <param name="accessId">MGS Mailbox access Id.</param>
        /// <param name="accessPassword">MGS Mailbox access password.</param>
        /// <param name="includeRawPayload">Returns an array of bytes even if decoded payload is available.RawPayload is returned automatically if no Message Definition File defines payload decoding for the Mailbox.</param>
        /// <param name="includeType">Includes data type when using a Message Definition File on the Mailbox.</param>
        /// <param name="startUtc">Messages sent from this start time reference (UTC) or later will be retrieved.</param>
        /// <returns>GetReturnMessagesResult object that holds API response.</returns>
        public static async Task<GetReturnMessagesResult> GetReturnMessagesAsync(HttpClient client, string accessId, string accessPassword, bool includeRawPayload, bool includeType, DateTime startUtc)
        {
            try { return await _getReturnMessagesAsync(client, accessId, accessPassword, includeRawPayload, includeType, startUtc); }
            catch { throw; }
        }

        /// <summary>
        /// Retrieves Mobile-Originated messages (up to 500) based on a start-time.
        /// </summary>
        /// <param name="client">HttpClient instance to use.</param>
        /// <param name="accessId">MGS Mailbox access Id.</param>
        /// <param name="accessPassword">MGS Mailbox access password.</param>
        /// <param name="includeRawPayload">Returns an array of bytes even if decoded payload is available.RawPayload is returned automatically if no Message Definition File defines payload decoding for the Mailbox.</param>
        /// <param name="includeType">Includes data type when using a Message Definition File on the Mailbox.</param>
        /// <param name="startUtc">Messages sent from this start time reference (UTC) or later will be retrieved.</param>
        /// <param name="mobileId">Filter to retrieve only messages sent by this Mobile ID.</param>
        /// <returns>GetReturnMessagesResult object that holds API response.</returns>
        public static async Task<GetReturnMessagesResult> GetReturnMessagesAsync(HttpClient client, string accessId, string accessPassword, bool includeRawPayload, bool includeType, DateTime startUtc, string mobileId)
        {
            try { return await _getReturnMessagesAsync(client, accessId, accessPassword, includeRawPayload, includeType, startUtc, mobileId); }
            catch { throw; }
        }

        /// <summary>
        /// Retrieves Mobile-Originated messages (up to 500) based on ID of a previously retrieved message.
        /// </summary>
        /// <param name="client">HttpClient instance to use.</param>
        /// <param name="accessId">MGS Mailbox access Id.</param>
        /// <param name="accessPassword">MGS Mailbox access password.</param>
        /// <param name="includeRawPayload">Returns an array of bytes even if decoded payload is available.RawPayload is returned automatically if no Message Definition File defines payload decoding for the Mailbox.</param>
        /// <param name="includeType">Includes data type when using a Message Definition File on the Mailbox.</param>
        /// <param name="fromId">A unique ID of a previously retrieved message, typically derived from NextStartID parameter of a prior get_return_messages operation where More messages are available to retrieve.</param>
        /// <returns>GetReturnMessagesResult object that holds API response.</returns>
        public static async Task<GetReturnMessagesResult> GetReturnMessagesAsync(HttpClient client, string accessId, string accessPassword, bool includeRawPayload, bool includeType, int fromId)
        {
            try { return await _getReturnMessagesAsync(client, accessId, accessPassword, includeRawPayload, includeType, fromId); }
            catch { throw; }
        }

        /// <summary>
        /// Retrieves Mobile-Originated messages (up to 500) based on ID of a previously retrieved message.
        /// </summary>
        /// <param name="client">HttpClient instance to use.</param>
        /// <param name="accessId">MGS Mailbox access Id.</param>
        /// <param name="accessPassword">MGS Mailbox access password.</param>
        /// <param name="includeRawPayload">Returns an array of bytes even if decoded payload is available.RawPayload is returned automatically if no Message Definition File defines payload decoding for the Mailbox.</param>
        /// <param name="includeType">Includes data type when using a Message Definition File on the Mailbox.</param>
        /// <param name="fromId">A unique ID of a previously retrieved message, typically derived from NextStartID parameter of a prior get_return_messages operation where More messages are available to retrieve.</param>
        /// <param name="mobileId">Filter to retrieve only messages sent by this Mobile ID.</param>
        /// <returns>GetReturnMessagesResult object that holds API response.</returns>
        public static async Task<GetReturnMessagesResult> GetReturnMessagesAsync(HttpClient client, string accessId, string accessPassword, bool includeRawPayload, bool includeType, int fromId, string mobileId)
        {
            try { return await _getReturnMessagesAsync(client, accessId, accessPassword, includeRawPayload, includeType, fromId, mobileId); }
            catch { throw; }
        }

        #endregion

        #region Private Methods

        private static async Task<DateTime> _getInfoUtcTimeAsync(HttpClient client)
        {
            const string endpoint = "info_utc_time.json/";

            try
            {
                // Sends Api request
                HttpResponseMessage response = await client.GetAsync(endpoint);

                // Checks OK response
                response.EnsureSuccessStatusCode();
                // Checks that response is json type
                response.EnsureJsonContentType();
                // Gets Response contents as string
                string content = await response.Content.ReadAsStringAsync();
                // Parses response contents as DateTime object (Deleting extra quotes in content)
                DateTime dt = DateTime.ParseExact(content.Trim('"'), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);

                // Returns date time
                return dt;
            }
            catch { throw; }
        }

        private static async Task<List<ErrorInfo>> _getInfoErrorsAsync(HttpClient client)
        {
            const string endpoint = "info_errors.json/";
            List<ErrorInfo> errorList = new();

            try
            {
                // Sends Api request
                HttpResponseMessage response = await client.GetAsync(endpoint);

                // Checks OK response
                response.EnsureSuccessStatusCode();
                // Checks that response is json type
                response.EnsureJsonContentType();
                // Gets Response contents as string
                string content = await response.Content.ReadAsStringAsync();
                // Parses response contents as JArray object
                var list = JArray.Parse(content);
                // Parses each object as IsatDataProApiError
                foreach (var error in list)
                {
                    errorList.Add(JsonConvert.DeserializeObject<ErrorInfo>(error.ToString()));
                }

                // Returns errors collection
                return errorList;
            }
            catch { throw; }
        }

        private static async Task<string> _getInfoVersionAsync(HttpClient client)
        {
            const string endpoint = "info_version.json/";
            try
            {
                // Sends Api request
                HttpResponseMessage response = await client.GetAsync(endpoint);

                // Checks OK response
                response.EnsureSuccessStatusCode();
                // Checks that response is json type
                response.EnsureJsonContentType();
                // Gets Response contents as string
                string content = await response.Content.ReadAsStringAsync();
                // Parses response contents as String object (Deletes extra quotes in content)
                string version = content.Trim('"');

                // Returns version
                return version;
            }
            catch { throw; }
        }

        private static async Task<GetReturnMessagesResult> _getReturnMessagesAsync(HttpClient client, string accessId, string accessPassword, bool includeRawPayload, bool includeType, DateTime startUtc)
        {
            string endpoint = $"get_return_messages.json/?access_id={accessId}&password={accessPassword}&start_utc={startUtc}&include_raw_payload={includeRawPayload}&include_type={includeType}";

            try
            {
                // Sends Api request
                HttpResponseMessage response = await client.GetAsync(endpoint);

                // Checks OK response
                response.EnsureSuccessStatusCode();
                // Checks that response is json type
                response.EnsureJsonContentType();
                // Gets Response contents as string
                string content = await response.Content.ReadAsStringAsync();
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                };
                var result = JsonConvert.DeserializeObject<GetReturnMessagesResult>(content, settings);

                // Returns result
                return result;
            }
            catch { throw; }
        }

        private static async Task<GetReturnMessagesResult> _getReturnMessagesAsync(HttpClient client, string accessId, string accessPassword, bool includeRawPayload, bool includeType, DateTime startUtc, string mobileId)
        {
            string endpoint = $"get_return_messages.json/?access_id={accessId}&password={accessPassword}&start_utc={startUtc}&include_raw_payload={includeRawPayload}&include_type={includeType}&mobile_id={mobileId}";

            try
            {
                // Sends Api request
                HttpResponseMessage response = await client.GetAsync(endpoint);

                // Checks OK response
                response.EnsureSuccessStatusCode();
                // Checks that response is json type
                response.EnsureJsonContentType();
                // Gets Response contents as string
                string content = await response.Content.ReadAsStringAsync();
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                };
                var result = JsonConvert.DeserializeObject<GetReturnMessagesResult>(content, settings);

                // Returns result
                return result;
            }
            catch { throw; }
        }

        private static async Task<GetReturnMessagesResult> _getReturnMessagesAsync(HttpClient client, string accessId, string accessPassword, bool includeRawPayload, bool includeType, int fromId)
        {
            string endpoint = $"get_return_messages.json/?access_id={accessId}&password={accessPassword}&from_id={fromId}&include_raw_payload={includeRawPayload}&include_type={includeType}";

            try
            {
                // Sends Api request
                HttpResponseMessage response = await client.GetAsync(endpoint);

                // Checks OK response
                response.EnsureSuccessStatusCode();
                // Checks that response is json type
                response.EnsureJsonContentType();
                // Gets Response contents as string
                string content = await response.Content.ReadAsStringAsync();
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                };
                var result = JsonConvert.DeserializeObject<GetReturnMessagesResult>(content, settings);

                // Returns result
                return result;
            }
            catch { throw; }
        }

        private static async Task<GetReturnMessagesResult> _getReturnMessagesAsync(HttpClient client, string accessId, string accessPassword, bool includeRawPayload, bool includeType, int fromId, string mobileId)
        {
            string endpoint = $"get_return_messages.json/?access_id={accessId}&password={accessPassword}&from_id={fromId}&include_raw_payload={includeRawPayload}&include_type={includeType}&mobile_id={mobileId}";

            try
            {
                // Sends Api request
                HttpResponseMessage response = await client.GetAsync(endpoint);

                // Checks OK response
                response.EnsureSuccessStatusCode();
                // Checks that response is json type
                response.EnsureJsonContentType();
                // Gets Response contents as string
                string content = await response.Content.ReadAsStringAsync();
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                };
                var result = JsonConvert.DeserializeObject<GetReturnMessagesResult>(content, settings);

                // Returns result
                return result;
            }
            catch { throw; }
        }

        #endregion
    }
}
