using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Gie.IsatDataPro.Extensions
{
    /// <summary>
    /// Custom extension methods for HttpResponseMessage class.
    /// </summary>
    public static class HttpResponseMessageExtensions
    {
        private const string _jsonContentType = "application/json";

        /// <summary>
        /// Throws an excewption if HttpResponseMessage.Content.Headers.ContentType.MediaType is not application/json
        /// </summary>
        public static void EnsureJsonContentType(this HttpResponseMessage response)
        {
            if (response.Content.Headers.ContentType.MediaType != _jsonContentType)
            {
                throw new Exception($"Response is not {_jsonContentType} type.");
            }
        }
    }
}
