using System;

namespace Gie.IsatDataPro
{
    /// <summary>
    /// Response to get_forward_statuses operation.
    /// </summary>
    public class GetForwardStatusesResult
    {
        /// <summary>
        /// The error ID of the operation.
        /// </summary>
        public int ErrorID { get; set; }

        /// <summary>
        /// True if more statuses remain on the MGS that match the given status filter.
        /// </summary>
        public bool More { get; set; }

        /// <summary>
        /// Datetime to use for the next GetForwardStatuses operation if More is true.
        /// </summary>
        public DateTime NextStartUTC { get; set; }

        /// <summary>
        /// Array of ForwardStatus objects.
        /// </summary>
        public ForwardStatus[] Statuses { get; set; }
    }
}

