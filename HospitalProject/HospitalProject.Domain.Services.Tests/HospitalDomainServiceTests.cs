using System;
using HospitalProject.DataAccess;
using HospitalProject.DataAccess.Entities;
using HospitalProject.DataAccess.Repositories;
using HospitalProject.Domain.Services.Contracts;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace HospitalProject.Domain.Services.Tests
{
    [TestFixture]
    public class HospitalDomainServiceTests
    {
        private Mock<HospitalDbContext> _dbContext;
        private Mock<HospitalRepository> _hospitalRepository;

        [SetUp]
        public void Setup()
        {
            _dbContext = new Mock<HospitalDbContext>();
            _hospitalRepository = new Mock<HospitalRepository>();
            _dbContext.SetupGet(p => p.Hospitals).Returns(_hospitalRepository.Object);
        }

        [Test]
        public async Task CreateAsyncReturnsAlreadyExistsIfNameExists()
        {
            //Setup
            var request = new CreateHospitalRequest
            {
                Name = "Name",
                Address = "Address"
            };

            _hospitalRepository
                .Setup(x => x.GetByName(It.IsAny<string>()))
                .Returns(new Hospital { Name = request.Name, Address = request.Address });

            var service = new HospitalDomainService(_dbContext.Object);

            // Execute
            var response = await service.CreateAsync(request);

            // Assert
            Assert.AreEqual(response.Result, CreateHospitalResult.AlreadyExists);
            Assert.IsNull(response.Entity);
            _hospitalRepository.Verify(x => x.Add(It.IsAny<Hospital>()), Times.Never);
            _dbContext.Verify(x => x.SaveChangesAsync(), Times.Never);
        }

        [Test]
        public async Task CreateAsyncReturnsSuccessOnSuccess()
        {
            //Setup
            var request = new CreateHospitalRequest
            {
                Name = "Name",
                Address = "Address"
            };

            _hospitalRepository
                .Setup(x => x.GetByName(It.IsAny<string>()))
                .Returns((Hospital)null);

            _hospitalRepository
                .Setup(x => x.Add(It.IsAny<Hospital>()))
                .Returns(new Hospital
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    Address = request.Address
                });

            var service = new HospitalDomainService(_dbContext.Object);

            // Execute
            var response = await service.CreateAsync(request);

            // Assert
            Assert.AreEqual(response.Result, CreateHospitalResult.Success);
            Assert.AreEqual(response.Entity.Name, request.Name);
            Assert.AreEqual(response.Entity.Address, request.Address);
            _hospitalRepository.Verify(x => x.Add(It.Is<Hospital>(h => h.Name.Equals(request.Name) && h.Address.Equals(request.Address))), Times.Once);
            _dbContext.Verify(x => x.SaveChangesAsync(), Times.Once);
        }
    }
}
