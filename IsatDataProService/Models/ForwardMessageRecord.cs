using System;

namespace Gie.IsatDataPro
{
    /// <summary>
    /// Forward message data and metadata.
    /// </summary>
    public class ForwardMessageRecord
    {
        /// <summary>
        /// System-assigned ForwardMessageID.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// UTC based datetime of the message status.
        /// </summary>
        public DateTime StatusUTC { get; set; }

        /// <summary>
        /// Time the message was accepted by the MGS.
        /// </summary>
        public DateTime CreateUTC { get; set; }

        /// <summary>
        /// True if the message was delivered or failed.
        /// </summary>
        public bool IsClosed { get; set; }

        /// <summary>
        /// The latest state of the message.
        /// </summary>
        public SubmitMessageState State { get; set; }

        /// <summary>
        /// The mobile ID or broadcast ID the message was sent to.
        /// </summary>
        public string DestinationID { get; set; }

        /// <summary>
        /// The error ID associated with the message state.
        /// </summary>
        public int ErrorID { get; set; }

        /// <summary>
        /// A decoded message payload derived from a Message Definition File provisioned on the Mailbox.
        /// </summary>
        public Payload Payload { get; set; }

        /// <summary>
        /// An array of payload byte values in decimal format.
        /// </summary>
        public int[] RawPayload { get; set; }

        /// <summary>
        /// System-assigned reference number.
        /// </summary>
        public int ReferenceNumber { get; set; }
    }
}

