using System;

namespace Gie.IsatDataPro.Models
{
    /// <summary>
    /// A Mobile-Originated message including metadata.
    /// </summary>
    public class ReturnMessage
    {
        /// <summary>
        /// A unique ID assigned by the network.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// The datetime (UTC) the message was made available in the Mailbox.
        /// </summary>
        public DateTime MessageUTC { get; set; }

        /// <summary>
        /// The unique Mobile ID that sent the message.
        /// </summary>
        public string MobileID { get; set; }

        /// <summary>
        /// The datetime (UTC) the message was received at the Satellite Access Station (aka Land Earth Station).
        /// </summary>
        public DateTime ReceiveUTC { get; set; }

        /// <summary>
        /// The system name of the satellite beam the message was received on.
        /// </summary>
        public string RegionName { get; set; }

        /// <summary>
        /// The Service Identification Number represented by the first payload byte.
        /// </summary>
        public byte SIN { get; set; }

        /// <summary>
        /// The size of the message (in bytes) sent over-the-air.
        /// </summary>
        public int OTAMessageSize { get; set; }

        /// <summary>
        /// A decoded message payload derived from a Message Definition File provisioned on the Mailbox.
        /// </summary>
        public Payload Payload { get; set; }

        /// <summary>
        /// An array of payload byte values in decimal format.
        /// </summary>
        public byte[] RawPayload { get; set; }
    }
}
