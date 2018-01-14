using NServiceBus;

namespace HospitalProject.Messaging.Contracts.Events
{
    /// <summary>
    /// The event is raised Hospital creation command failed
    /// </summary>
    public class HospitalCreateFailedEvent : IEvent
    {
        /// <summary>
        /// Hospital name
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Hospital address
        /// </summary>
        public string Address { get; set; }
    }
}
