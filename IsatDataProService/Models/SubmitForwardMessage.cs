using System;

namespace Gie.IsatDataPro
{
    /// <summary>
    /// Metadata relating to the Forward message submission.
    /// </summary>
    public class ForwardStatus
    {
        /// <summary>
        /// The system-generated ID for the Mobile-Terminated message submitted.
        /// </summary>
        public string ForwardMessageID { get; set; }

        /// <summary>
        /// True if the message was delivered or failed.
        /// </summary>
        public bool IsClosed { get; set; }

        /// <summary>
        /// The latest state of the Forward message.
        /// </summary>
        public SubmitMessageState State { get; set; }

        /// <summary>
        /// The error ID associated with the message state.
        /// </summary>
        public int ErrorID { get; set; }

        /// <summary>
        /// The datetime (UTC) of the State of the message.
        /// </summary>
        public DateTime StateUTC { get; set; }

        /// <summary>
        /// System-assigned reference number.
        /// </summary>
        public int ReferenceNumber { get; set; }
    }
}

