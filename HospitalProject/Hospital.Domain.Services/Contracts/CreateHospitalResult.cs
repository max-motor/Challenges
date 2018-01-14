namespace HospitalProject.Domain.Services.Contracts
{
    /// <summary>
    /// CreateHospital operation result
    /// </summary>
    public enum CreateHospitalResult
    {
        /// <summary>
        /// Hospital created successfully
        /// </summary>
        Success = 0,

        /// <summary>
        /// Didn't create hospital, because a hospital with the same name already exists
        /// </summary>
        AlreadyExists,
    }
}