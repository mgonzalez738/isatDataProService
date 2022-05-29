using System;

namespace Gie.IsatDataPro
{
    /// <summary>
    /// A Mobile-Destinated message including metadata.
    /// </summary>
    public class ForwardMessage
    {
        /// <summary>
        /// Unique Mobile ID or Broadcast ID to send the message to.
        /// </summary>
        public string DestinationID { get; set; }

        /// <summary>
        /// Client-assigned message ID to correlate to network-assigned ID.
        /// </summary>
        public int UserMessageID { get; set; }

        /// <summary>
        /// A decoded message payload based on a Message Definition File provisioned on the Mailbox.
        /// </summary>
        public Payload Payload { get; set; }

        /// <summary>
        /// An array of bytes in decimal format. Required if not using Payload.
        /// </summary>
        public int[] RawPayload { get; set; }
    }
}

