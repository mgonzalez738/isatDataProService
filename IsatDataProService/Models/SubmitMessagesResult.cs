using System;

namespace Gie.IsatDataPro
{
    /// <summary>
    /// Response to submit_cancelations or submit_messages_to_destinations.
    /// </summary>
    public class SubmitMessagesResult
    {
        /// <summary>
        /// Error ID of the submission batch.
        /// </summary>
        public int ErrorID { get; set; }

        /// <summary>
        /// An array of ForwardSubmission objects.
        /// </summary>
        public ForwardSubmission Submissions { get; set; }

    }
}

