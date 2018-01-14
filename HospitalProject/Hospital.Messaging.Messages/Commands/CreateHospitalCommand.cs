using NServiceBus;

namespace HospitalProject.Messaging.Contracts.Commands
{
    /// <summary>
    /// Message for creating Hospital
    /// </summary>
    public class CreateHospitalCommand : ICommand
    {
        /// <summary>
        /// Hospital name
        /// </summary>
        public string Name { get; set; }
     
        /// <summary>
        /// Hospital Address
        /// </summary>
        public string Address { get; set; }
    }
}
