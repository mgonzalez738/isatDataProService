namespace Gie.IsatDataPro.Models
{
    /// <summary>
    /// Errors definitions supported by the MGS.
    /// </summary>
    public class ErrorInfo
    {
        /// <summary>
        /// Unique error ID.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Brief description of error.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Long description of error.
        /// </summary>
        public string Description { get; set; }
    }
}
