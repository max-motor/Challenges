using System;
using System.Threading.Tasks;
using HospitalProject.Messaging.Contracts.Commands;
using NServiceBus;
using NServiceBus.Features;

namespace HospitalProject.Messaging.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).Wait();
        }

        static async Task MainAsync(string[] args)
        {
            Console.Title = "HospitalProject.Messaging.ConsoleClient";

            var endpointConfiguration = new EndpointConfiguration("HospitalProject.Messaging.ConsoleClient");

            endpointConfiguration.UseSerialization<JsonSerializer>();
            endpointConfiguration.UseTransport<LearningTransport>();
            endpointConfiguration.UsePersistence<LearningPersistence>();
            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.EnableInstallers();

            var endpointInstance = await Endpoint
                        .Start(endpointConfiguration)
                        .ConfigureAwait(false);
            try
            {
                await CreateHospital(endpointInstance);
            }
            finally
            {
                await endpointInstance.Stop()
                    .ConfigureAwait(false);
            }
        }

        static async Task CreateHospital(IEndpointInstance endpointInstance)
        {
            Console.WriteLine("Press enter to send a message");
            while (true)
            {
                var key = Console.ReadKey();
                Console.WriteLine();

                if (key.Key != ConsoleKey.Enter)
                {
                    return;
                }
                var id = Guid.NewGuid();
                var random = new Random();


                var createHospitalCommand = new CreateHospitalCommand
                {
                    Name = $"Hospital#{random.Next(5)}" ,
                    Address = $"{random.Next(100)} Main Street"
                };

                await endpointInstance
                    .Send("HospitalProject.Messaging.Server", createHospitalCommand)
                    .ConfigureAwait(false);
                Console.WriteLine($"Sent a CreateHospitalCommand message with Name: {createHospitalCommand.Name}");
            }
        }
    }
}
