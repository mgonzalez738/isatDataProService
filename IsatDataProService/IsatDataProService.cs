using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Serilog;
using Gie.IsatDataPro.Models;
using System.Collections.Generic;
using Newtonsoft.Json;
using Gie.IsatDataPro.Extensions;
using System.IO;
using System.Reflection;

namespace Gie.IsatDataPro
{
    /// <summary>
    /// Service for accessing IsatData Pro Gateway Messaging API.
    /// </summary>
    public class IsatDataProService
    {
        #region Private Members

        private readonly ILogger _log;
        private HttpClient _httpClient;
        private List<IsatDataProApiError> _errorList;
        private string _accessId;
        private string _accessPassword;
        private const string _baseApiUrl = "https://api.inmarsat.com/v1/idp/gateway/rest/";
        private const string _assemblyName = "Gie.IsatDataPro";
        private const string _errorListFileName = "IsatDataProApiErrorList.json";

        #endregion

        #region Public Properties

        #endregion

        #region Constructors

        /// <summary>
        /// Creates new instance of service.
        /// Base Api URL https://api.inmarsat.com/v1/idp/gateway/rest
        /// <param name="accessId">MGS API access Id</param>
        /// <param name="accessPassword">MGS API access Id</param>
        /// </summary>
        public IsatDataProService(string accessId, string accessPassword) 
        {
            // Initialize logger
            _log = Log.ForContext("Name", this.ToString()[(this.ToString().LastIndexOf('.') + 1)..]);
            // Initialize service
            Init(accessId, accessPassword, new HttpClient());
        }

        /// <summary>
        /// Creates new instance of service.
        /// Default Api URL https://api.inmarsat.com/v1/idp/gateway/rest
        /// </summary>
        /// <param name="accessId">MGS API access Id</param>
        /// <param name="accessPassword">MGS API access Id</param>
        /// <param name="serviceName">Service name to be shown on Serilog logs</param>
        public IsatDataProService(string accessId, string accessPassword, string serviceName)
        {
            // Initialize logger
            _log = Log.ForContext("Name", serviceName);
            // Initialize service
            Init(accessId, accessPassword, new HttpClient());
        }

        /// <summary>
        /// Creates new instance of service.
        /// Base Api URL https://api.inmarsat.com/v1/idp/gateway/rest
        /// <param name="accessId">MGS API access Id</param>
        /// <param name="accessPassword">MGS API access Id</param>
        /// <param name="httpClient">HttpClient to be use by service. Allows mocking in unit testing.</param>
        /// </summary>
        public IsatDataProService(string accessId, string accessPassword, HttpClient httpClient)
        {
            // Initialize logger
            _log = Log.ForContext("Name", this.ToString()[(this.ToString().LastIndexOf('.') + 1)..]);
            // Initialize service
            Init(accessId, accessPassword, httpClient);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Initialize service
        /// </summary>
        /// <param name="accessId">MGS API access Id</param>
        /// <param name="accessPassword">MGS API access Id</param>
        /// <param name="httpClient">HttpClient to be use by service</param>
        private void Init(string accessId, string accessPassword, HttpClient httpClient)
        {
            // Set credentials
            _accessId = accessId;
            _accessPassword = accessPassword;

            // Sets http client
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(_baseApiUrl);

            // Load API error code
            var assembly = Assembly.GetExecutingAssembly();
            var resourcePath = _assemblyName + ".Resources." + _errorListFileName;
            using Stream stream = assembly.GetManifestResourceStream(resourcePath);
            if (stream == null)
            {
                _log.Warning($"MGS API error list embedded file ({resourcePath}) could not be found.");
            }
            else
            {
                StreamReader reader = new(stream);
                try
                {
                    // Reads file as string
                    string jsonFile = reader.ReadToEnd();
                    // Parses string as JArray object
                    var list = JArray.Parse(jsonFile);
                    // Parses each object as IsatDataProApiError
                    _errorList = new();
                    foreach (var error in list)
                    {
                        _errorList.Add(JsonConvert.DeserializeObject<IsatDataProApiError>(error.ToString()));
                    }
                }
                catch (Exception ex)
                {
                    _log.Warning($"Error loading MGS API error list embedded file -> {ex.Message}.");
                }
                finally
                {
                    reader.Dispose();
                }
            }

            _log.Information($"Service created.");
        }

        #endregion

        #region Public API Methods

        /// <summary>
        /// Gets gateway current time (UTC).
        /// </summary>
        /// <returns>Gateways utc datetime</returns>
        public async Task<DateTime> GetInfoUtcTimeAsync()
        {
            const string relativeUrl = "info_utc_time.json/";
            try
            {
                // Sends Api request
                HttpResponseMessage response = await _httpClient.GetAsync(relativeUrl);
                // Checks OK response
                response.EnsureSuccessStatusCode();
                // Checks that response is json type
                response.EnsureJsonContentType();
                // Gets Response contents as string
                string content = await response.Content.ReadAsStringAsync();
                // Parses response contents as DateTime object (Deletes extra quotes in content)
                DateTime dt = DateTime.ParseExact(content.Trim('"'), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
                
                _log.Debug($"Method {nameof(GetInfoUtcTimeAsync)} executed.");
                _log.Verbose($"{content}.");

                // Returns date time
                return dt;
            }
            catch (Exception ex)
            {
                _log.Error($"Error executing method {nameof(GetInfoUtcTimeAsync)} -> {ex.Message}.");
                throw;
            }
        }

        /// <summary>
        /// Gets a list of supported error definitions
        /// </summary>
        /// <returns>Collection of errors definitions supported by the MGS</returns>
        public async Task<List<IsatDataProApiError>> GetInfoErrorsAsync()
        {
            const string relativeUrl = "info_errors.json/";
            List<IsatDataProApiError> errorList = new();

            try
            {
                // Sends Api request
                HttpResponseMessage response = await _httpClient.GetAsync(relativeUrl);
                // Checks OK response
                response.EnsureSuccessStatusCode();
                // Checks that response is json type
                response.EnsureJsonContentType();
                // Gets Response contents as string
                string content = await response.Content.ReadAsStringAsync();
                // Parses response contents as JArray object
                var list = JArray.Parse(content);
                // Parses each object as IsatDataProApiError
                foreach(var error in list)
                {
                    errorList.Add(JsonConvert.DeserializeObject<IsatDataProApiError>(error.ToString()));
                }

                _log.Debug($"Method {nameof(GetInfoErrorsAsync)} executed.");
                _log.Verbose($"{content}");

                // Returns errors collection
                return errorList;
            }
            catch (Exception ex)
            {
                _log.Error($"Error executing method {nameof(GetInfoErrorsAsync)} -> {ex.Message}.");
                throw;
            }
        }

        /// <summary>
        /// Gets the current MGS software version
        /// </summary>
        /// <returns>Gateways software version</returns>
        public async Task<string> GetInfoVersionAsync()
        {
            const string relativeUrl = "info_version.json/";
            try
            {
                // Sends Api request
                HttpResponseMessage response = await _httpClient.GetAsync(relativeUrl);
                // Checks OK response
                response.EnsureSuccessStatusCode();
                // Checks that response is json type
                response.EnsureJsonContentType();
                // Gets Response contents as string
                string content = await response.Content.ReadAsStringAsync();
                // Parses response contents as String object (Deletes extra quotes in content)
                string version = content.Trim('"'); 

                _log.Debug($"Method {nameof(GetInfoVersionAsync)} executed.");
                _log.Verbose($"{content}");

                // Returns version
                return version;
            }
            catch (Exception ex)
            {
                _log.Error($"Error executing method {nameof(GetInfoVersionAsync)} -> {ex.Message}.");
                throw;
            }
        }

        /// <summary>
        /// Retrieves Mobile-Originated messages (up to 500) based on a start-time.
        /// </summary>
        /// <param name="startUtc">Messages sent from this start time reference (UTC) or later will be retrieved</param>
        /// <param name="includeRawPayload">Returns an array of bytes even if decoded payload is available.RawPayload is returned automatically if no Message Definition File defines payload decoding for the Mailbox</param>
        /// <param name="includeType">Includes data type when using a Message Definition File on the Mailbox</param>
        /// <returns>TODO</returns>
        public async Task GetReturnMessagesSinceUtcAsync(DateTime startUtc, bool includeRawPayload, bool includeType)
        {
            string relativeUrl = $"get_return_messages.json/?access_id={_accessId}&password={_accessPassword}&start_utc={startUtc}&include_raw_payload={includeRawPayload}&include_type={includeType}";
            try
            {
                // Sends Api request
                HttpResponseMessage response = await _httpClient.GetAsync(relativeUrl);
                // Checks OK response
                response.EnsureSuccessStatusCode();
                // Checks that response is json type
                response.EnsureJsonContentType();

                // Gets Response contents as string
                string content = await response.Content.ReadAsStringAsync();
                // Parses response contents as String object (Deletes extra quotes in content)
                string version = content.Trim('"');

                _log.Debug($"Method {nameof(GetReturnMessagesSinceUtcAsync)} executed.");
                _log.Verbose($"{content}");

            }
            catch (Exception ex)
            {
                _log.Error($"Error executing method {nameof(GetReturnMessagesSinceUtcAsync)} -> {ex.Message}");
                throw;
            }
        }

        #endregion
    }
}
