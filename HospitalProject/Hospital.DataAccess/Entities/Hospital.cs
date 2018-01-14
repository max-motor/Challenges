using System;

namespace HospitalProject.DataAccess.Entities
{
    /// <summary>
    /// Entity represents Hospital
    /// </summary>
    public class Hospital
    {
        /// <summary>
        /// Hospital ID
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Hospital Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Hoospital Address
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Entity creation time
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}
