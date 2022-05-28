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
using Gie.IsatDataPro.Utils;

namespace Gie.IsatDataPro
{
    /// <summary>
    /// Service for accessing IsatData Pro Gateway Messaging API.
    /// </summary>
    public class IsatDataProService
    {
        #region Private Members

        private readonly ILogger _log;
        private List<ErrorInfo> _errorList;
        private string _accessId;
        private string _accessPassword;
        private const string _assemblyName = "Gie.IsatDataPro";
        private const string _errorListFileName = "IsatDataProApiErrorList.json";
        private ScheduleTimer _pollTimer;

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
            Init(accessId, accessPassword);
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
            Init(accessId, accessPassword);
        }

        #endregion

        #region Public Methods

        public void Start(int periodSeconds)
        {
            _pollTimer = new ScheduleTimer();
            _pollTimer.Elapsed += PollTimerElapsed;
            _pollTimer.Start(periodSeconds);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Initialize service
        /// </summary>
        /// <param name="accessId">MGS API access Id</param>
        /// <param name="accessPassword">MGS API access Id</param>
        /// <param name="httpClient">HttpClient to be use by service</param>
        private void Init(string accessId, string accessPassword)
        {
            // Set credentials
            _accessId = accessId;
            _accessPassword = accessPassword;

            _log.Information($"Service created.");

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
                        _errorList.Add(JsonConvert.DeserializeObject<ErrorInfo>(error.ToString()));
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
        }

        private void PollTimerElapsed(object sender, EventArgs e)
        {
            
        }

        #endregion

    }
}
