using System;

namespace Gie.IsatDataPro
{
    /// <summary>
    /// Response to GetForwardMessages operation.
    /// </summary>
    public class GetForwardMessagesResult
    {
        /// <summary>
        /// The error ID of the operation.
        /// </summary>
        public int ErrorID { get; set; }

        /// <summary>
        /// List of retrieved Forward messages.
        /// </summary>
        public ForwardMessageRecord[] Messages { get; set; }
    }
}

