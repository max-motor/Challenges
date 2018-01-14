using System;
using System.Threading.Tasks;
using HospitalProject.Messaging.Contracts.Events;
using Newtonsoft.Json;
using NServiceBus;

namespace HospitalProject.Messaging.Server.Handlers.Events
{
    /// <summary>
    /// Event handler doesn't necessarily belong in the this project.
    /// In real-life example event subscriber will live in their own service
    /// This is done for visibility purposes only: to illustrate the event is published and consumed
    /// </summary>
    public class HospitalCreateFailedEventHandler : IHandleMessages<HospitalCreateFailedEvent>
    {
        public async Task Handle(HospitalCreateFailedEvent message, IMessageHandlerContext context)
        {
            Console.WriteLine("HospitalCreateFailedEvent message received: " + JsonConvert.SerializeObject(message));
        }
    }
}
