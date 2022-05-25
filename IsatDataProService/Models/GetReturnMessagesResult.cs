using System;

namespace Gie.IsatDataPro.Models
{
    /// <summary>
    /// Holds a response from a get_return_messages call.
    /// </summary>
    public class GetReturnMessagesResult
    {
        /// <summary>
        /// An error number for the API operation. (0 = NO_ERROR).
        /// </summary>
        public int ErrorID { get; set; }

        /// <summary>
        /// A flag indicating if more messages are available to retrieve in a subsequent get_return_messages call.
        /// </summary>
        public bool More { get; set; }

        /// <summary>
        /// The next start time high water mark for a subsequent get_return_messages call. Null indicates no more messages are pending.
        /// </summary>
        public DateTime NextStartUTC { get; set; }

        /// <summary>
        /// The unique message ID high water mark for a subsequent get_return_messages call. A value -1 indicates no more messages are pending.
        /// </summary>
        public int NextStartID { get; set; }

        /// <summary>
        /// The list of retrieved messages.
        /// </summary>
        public ReturnMessage[] Messages { get; set; }

    }
}
