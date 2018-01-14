using System.Linq;
using System.Threading.Tasks;
using HospitalProject.DataAccess.Entities;
using HospitalProject.Domain.Services.Contracts;
using HospitalProject.Messaging.Contracts.Commands;
using HospitalProject.Messaging.Contracts.Events;
using HospitalProject.Messaging.Server.Handlers.Commands;
using Moq;
using NServiceBus.Testing;
using NUnit.Framework;

namespace HospitalProject.Messaging.Server.Tests
{
    [TestFixture]
    public class CreateHospitalCommandHandlerTests
    {
        private Mock<IHospitalDomainService> _domainService;

        [SetUp]
        public void Setup()
        {
            _domainService = new Mock<IHospitalDomainService>();
        }

        [Test]
        public async Task CreateHospitalCommandHandlerOnSuccessPublishesSuccessEvent()
        {
            //Setup
            var command = new CreateHospitalCommand
            {
                Name = "HospitalName",
                Address = "HospitalAddress"
            };

            _domainService.Setup(x => x.CreateAsync(It.IsAny<CreateHospitalRequest>()))
                .ReturnsAsync(new CreateHospitalResponse
                {
                    Result = CreateHospitalResult.Success,
                    Entity = new Hospital
                    {
                        Name = command.Name,
                        Address = command.Address
                    }
                });

            var handlerContext = new TestableMessageHandlerContext();
            var handler = new CreateHospitalCommandHandler(_domainService.Object);

            //Execute
            await handler.Handle(command, handlerContext);

            // Assert
            Assert.AreEqual(1, handlerContext.PublishedMessages.Length);
            Assert.IsInstanceOf<HospitalCreatedEvent>(handlerContext.PublishedMessages[0].Message);

            var message = (HospitalCreatedEvent)handlerContext.PublishedMessages[0].Message;
            Assert.AreEqual(message.Name, command.Name);
            Assert.AreEqual(message.Address, command.Address);
        }

        [Test]
        public async Task CreateHospitalCommandHandlerOnFailurePublishesFailureEvent()
        {
            //Setup
            var command = new CreateHospitalCommand
            {
                Name = "HospitalName",
                Address = "HospitalAddress"
            };

            _domainService.Setup(x => x.CreateAsync(It.IsAny<CreateHospitalRequest>()))
                .ReturnsAsync(new CreateHospitalResponse
                {
                    Result = CreateHospitalResult.AlreadyExists,
                    Entity = null
                });

            var handlerContext = new TestableMessageHandlerContext();
            var handler = new CreateHospitalCommandHandler(_domainService.Object);

            //Execute
            await handler.Handle(command, handlerContext).ConfigureAwait(false);

            // Assert
            Assert.AreEqual(1, handlerContext.PublishedMessages.Length);
            Assert.IsInstanceOf<HospitalCreateFailedEvent>(handlerContext.PublishedMessages[0].Message);

            var message = (HospitalCreateFailedEvent)handlerContext.PublishedMessages[0].Message;
            Assert.AreEqual(message.Name, command.Name);
            Assert.AreEqual(message.Address, command.Address);
        }
    }
}
