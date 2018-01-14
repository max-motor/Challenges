using System;
using System.Threading.Tasks;
using HospitalProject.Domain.Services.Contracts;
using HospitalProject.Messaging.Contracts.Commands;
using HospitalProject.Messaging.Contracts.Events;
using Newtonsoft.Json;
using NServiceBus;

namespace HospitalProject.Messaging.Server.Handlers.Commands
{
    public class CreateHospitalCommandHandler : IHandleMessages<CreateHospitalCommand>
    {
        private readonly IHospitalDomainService _domainService;

        public CreateHospitalCommandHandler(IHospitalDomainService domainService)
        {
            _domainService = domainService;
        }

        public async Task Handle(CreateHospitalCommand message, IMessageHandlerContext context)
        {
            //TODO: Use Automapper
            var request = new CreateHospitalRequest
            {
                Name = message.Name,
                Address = message.Address
            };

            //Create hospital
            var result = await _domainService.CreateAsync(request);

            Console.WriteLine("CreateHospitalCommand message received: " + JsonConvert.SerializeObject(message));

            // Publish HospitalCreatedEvent if operation successful
            if (result.Result == CreateHospitalResult.Success)
            {
                await context.Publish(
                    //Todo: use AutoMapper here
                    new HospitalCreatedEvent
                    {
                        Id = result.Entity.Id,
                        Name = result.Entity.Name,
                        Address = result.Entity.Address
                    });
                return;
            }

            // Publish HospitalCreateFailedEvent if hospital creation failed
            await context.Publish(
                //Todo: use AutoMapper here
                new HospitalCreateFailedEvent
                {
                    Name = message.Name,
                    Address = message.Address
                });
        }
    }
}
