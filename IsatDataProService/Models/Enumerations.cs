namespace Gie.IsatDataPro
{
    /// <summary>
    /// Submit message states for a to-mobile message.
    /// </summary>
    /// <remarks>
    /// When a message is submitted it is considered open and remains so until either an acknowledgement is received from the terminal or an error condition occurs.
    /// </remarks>
    public enum SubmitMessageState : int
    {
        /// <summary>
        /// The system has accepted the message (Open).
        /// </summary>
        Submitted = 0,
        /// <summary>
        /// The system received an acknowledgement from the terminal (Closed).
        /// </summary>
        Received = 1,
        /// <summary>
        /// An error code specifies the reason (Closed).
        /// </summary>
        Error = 2,
        /// <summary>
        /// The system failed to deliver the message to a terminal (Closed).
        /// </summary>
        DeliveryFailed = 3,
        /// <summary>
        /// Message failed to be delivered within 60 minutes (Closed).
        /// </summary>
        /// <remarks>
        /// A normal state when the message cannot be delivered is DeliveryFailed. 
        /// This state is required in case the message is still not delivered 60 minutes after it was sent from the MGS.
        /// </remarks>
        TimedOut = 4,
        /// <summary>
        /// Message was successfully cancelled by the client application (Closed).
        /// </summary>
        Cancelled = 5,
        /// <summary>
        /// Message is queued (Open).
        /// </summary>
        /// <remarks>
        /// The terminal is in low-power mode and message will be forwarded when terminal wakes up.
        /// </remarks>
        Waiting = 6
    }
}
