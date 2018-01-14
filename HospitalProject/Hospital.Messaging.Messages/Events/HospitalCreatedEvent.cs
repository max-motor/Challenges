using System;
using NServiceBus;

namespace HospitalProject.Messaging.Contracts.Events
{
    /// <summary>
    /// The event is raised when Hospital is created successfully
    /// </summary>
    public class HospitalCreatedEvent : IEvent
    {
        /// <summary>
        /// Hospital ID
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Hospital name
        /// </summary>
        public string  Name { get; set; }
        /// <summary>
        /// Hospital address
        /// </summary>
        public string  Address { get; set; }
    }
}
