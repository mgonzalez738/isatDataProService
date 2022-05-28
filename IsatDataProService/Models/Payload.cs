namespace Gie.IsatDataPro.Models
{
    /// <summary>
    /// A decoded message payload derived from a Message Definition File provisioned on the Mailbox.
    /// </summary>
    public class Payload
    {
        /// <summary>
        /// The name of the message, unique within the defined SIN.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The Service Identification Number, also the first byte of raw payload.
        /// </summary>
        /// <remarks>
        /// Used to define a set of related messages or remote operations. 
        /// SIN is intended as a context encapsulating both Return and Forward messages defined in a Message Definition File.
        /// </remarks>
        public byte SIN { get; set; }

        /// <summary>
        /// The Message Identification Number, also the second byte of raw payload.
        /// </summary>
        /// <remarks>
        /// Used to define a message or remote operation within a SIN context. 
        /// MIN are intended to represent a specific directional operation and are often paired as Forward request with Return response.
        /// </remarks>
        public byte MIN { get; set; }

        /// <summary>
        /// Indicates if the message is Forward/Mobile-Terminated.
        /// </summary>
        public bool IsForward { get; set; }

        /// <summary>
        /// An array of decoded message data fields derived from a Message Definition File provisioned on the Mailbox.
        /// </summary>
        public object[] Fields { get; set; }
    }
}
