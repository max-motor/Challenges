namespace HospitalProject.Domain.Services.Contracts
{
    /// <summary>
    /// Request object for creation Hospital
    /// </summary>
    public class CreateHospitalRequest
    {
        /// <summary>
        /// Hospital name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Hospital Address
        /// </summary>
        public string Address { get; set; }
    }
}