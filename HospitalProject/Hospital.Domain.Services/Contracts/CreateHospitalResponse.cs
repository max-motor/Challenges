using HospitalProject.DataAccess.Entities;

namespace HospitalProject.Domain.Services.Contracts
{
    /// <summary>
    /// Response object for CreateHospital operation
    /// </summary>
    public class CreateHospitalResponse
    {
        /// <summary>
        /// Operation result
        /// </summary>
        public CreateHospitalResult Result { get; set; }
        /// <summary>
        /// HOspital entity. Null if operation failed
        /// </summary>
        public Hospital Entity { get; set; }
    }
}