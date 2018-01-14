using System;
using System.Threading.Tasks;
using HospitalProject.DataAccess;
using HospitalProject.DataAccess.Entities;
using HospitalProject.Domain.Services.Contracts;

namespace HospitalProject.Domain.Services
{
    public class HospitalDomainService : IHospitalDomainService
    {
        private readonly HospitalDbContext _dbContext;

        public HospitalDomainService(HospitalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CreateHospitalResponse> CreateAsync(CreateHospitalRequest request)
        {
            if (_dbContext.Hospitals.GetByName(request.Name) != null)
            {
                return new CreateHospitalResponse { Result = CreateHospitalResult.AlreadyExists };
            }

            //Todo: use AutoMapper
            //Note: relying on NServiceBus to handle exceptions and do exception logging
            var hospital = _dbContext.Hospitals.Add(new Hospital
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Address = request.Address,
                CreatedAt = DateTime.UtcNow
            });
            await _dbContext.SaveChangesAsync();

            return new CreateHospitalResponse
            {
                Result = CreateHospitalResult.Success,
                Entity = hospital
            };
        }
    }
}
