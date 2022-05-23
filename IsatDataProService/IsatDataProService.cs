using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Serilog;
using Gie.IsatDataPro.Models;
using System.Collections.Generic;
using Newtonsoft.Json;

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
        private const string _defaultApiUrl = "https://api.inmarsat.com/v1/idp/gateway/rest";

        #endregion

        #region Public Properties

        public string ApiUrl { get; set; }
        public string AccessId { get; set; }
        public string AccessPassword { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates new instance of service.
        /// Default Api URL https://api.inmarsat.com/v1/idp/gateway/rest
        /// </summary>
        public IsatDataProService() 
        {
            ApiUrl = _defaultApiUrl;
            _log = Log.ForContext("Name", this.ToString()[(this.ToString().LastIndexOf('.') + 1)..]);
            _ = ServiceInitAsync();
        }

        /// <summary>
        /// Creates new instance of service.
        /// Default Api URL https://api.inmarsat.com/v1/idp/gateway/rest
        /// </summary>
        /// <param name="name">Name to be shown on Serilog logs</param>
        public IsatDataProService(string name)
        {
            ApiUrl = _defaultApiUrl;
            _log = Log.ForContext("Name", name);
            _ = ServiceInitAsync();
        }

        #endregion

        #region Private Methods

        private async Task ServiceInitAsync()
        {
            // Configures Http
            _httpClient = new HttpClient();

            // Gets gateways version
            var version = await GetInfoVersionAsync();
            // Gets gateway error list
            _errorList = await GetInfoErrorsAsync();

            _log.Information($"Service started. MGS software version: {version}.");
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets gateway current time (UTC).
        /// </summary>
        /// <returns>Gateways utc datetime</returns>
        public async Task<Object> GetInfoUtcTimeAsync()
        {
            const string partialUrl = "/info_utc_time.json/";
            try
            {
                // Sends Api request
                HttpResponseMessage response = await _httpClient.GetAsync(ApiUrl + partialUrl);
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
            const string partialUrl = "/info_errors.json/";
            List<IsatDataProApiError> errorList = new();

            try
            {
                // Sends Api request
                HttpResponseMessage response = await _httpClient.GetAsync(ApiUrl + partialUrl);
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
            const string partialUrl = "/info_version.json/";
            try
            {
                // Sends Api request
                HttpResponseMessage response = await _httpClient.GetAsync(ApiUrl + partialUrl);
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

        #endregion
    }
}
