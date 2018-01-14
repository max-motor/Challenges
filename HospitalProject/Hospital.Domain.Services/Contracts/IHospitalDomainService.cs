using System.Threading.Tasks;

namespace HospitalProject.Domain.Services.Contracts
{
    /// <summary>
    /// The service containg business logic to manipulate Hospital domain object
    /// </summary>
    public interface IHospitalDomainService
    {
        /// <summary>
        /// Creates Hospital
        /// </summary>
        /// <param name="request">Hospital attributes</param>
        /// <returns></returns>
        Task<CreateHospitalResponse> CreateAsync(CreateHospitalRequest request);
    }
}
