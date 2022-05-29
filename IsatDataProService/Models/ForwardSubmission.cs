using System;

namespace Gie.IsatDataPro
{
    /// <summary>
    /// Metadata relating to a submitted Mobile-Terminated message.
    /// </summary>
    public class ForwardSubmission
    {
        /// <summary>
        /// The error code associated with the message acceptance.
        /// </summary>
        public int ErrorID { get; set; }

        /// <summary>
        /// The Mobile ID or the Broadcast ID the message was sent to.
        /// </summary>
        public string DestinationID { get; set; }

        /// <summary>
        /// A unique ID of the Forward message assigned by the system. 
        /// The client can use the ForwardMessageID to query the status of the message using GetForwardStatuses or GetForwardMessages.
        /// </summary>
        public int ForwardMessageID { get; set; }

        /// <summary>
        /// UTC based datetime of the submission/acceptance.
        /// </summary>
        public DateTime StateUTC { get; set; }

        /// <summary>
        /// Present if UserMessageID was passed in the submit_messages call. 
        /// This number can be used by the client application to map its own message ID the system-generated ForwardMessageID.
        /// </summary>
        public int UserMessageID { get; set; }

        /// <summary>
        /// The modem's wake-up period in seconds if it is configured for low power mode, (wakeupPeriod is non-zero), or zero (0) if not configured for low power mode.
        /// </summary>
        public int TerminalWakeupPeriod { get; set; }

        /// <summary>
        /// If the modem is not configured for low power mode, this will be an empty string. 
        /// If the modem is configured with a wakeupPeriod nonzero this field contains the scheduled UTC transmission time.
        /// </summary>
        public DateTime ScheduledSendUTC { get; set; }

        /// <summary>
        /// The size of the message (in bytes) sent over-the-air.
        /// </summary>
        public int OTAMessageSize { get; set; }

    }
}

