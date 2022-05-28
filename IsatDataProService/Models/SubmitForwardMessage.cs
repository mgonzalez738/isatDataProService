using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace Gie.IsatDataPro.Models
{
    /// <summary>
    /// The body of the POST operation for submit_messages.
    /// </summary>
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class SubmitForwardMessage
    {
        /// <summary>
        /// The Mailbox Access ID credential.
        /// </summary>
        public string AccessID { get; set; }

        /// <summary>
        /// The Mailbox Password credential.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// The list of messages to be sent.
        /// </summary>
        public ForwardMessage[] Messages { get; set; }
    }
}

