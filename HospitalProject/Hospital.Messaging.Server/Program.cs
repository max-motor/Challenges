using System;
using System.Threading.Tasks;
using HospitalProject.DataAccess;
using HospitalProject.Domain.Services;
using HospitalProject.Domain.Services.Contracts;
using NServiceBus;

namespace HospitalProject.Messaging.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).Wait();
        }
        static async Task MainAsync(string[] args)
        {
            string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = (System.IO.Path.GetDirectoryName(executable));
            AppDomain.CurrentDomain.SetData("DataDirectory", path);

            Console.Title = "HospitalProject.Messaging.Server";
            var endpointConfiguration = new EndpointConfiguration("HospitalProject.Messaging.Server");

            endpointConfiguration.UseSerialization<JsonSerializer>();
            endpointConfiguration.UseTransport<LearningTransport>();
            endpointConfiguration.UsePersistence<LearningPersistence>();
            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.EnableInstallers();

            //TODO: Use CastleWindsor here
            endpointConfiguration.RegisterComponents(configureComponents =>
            {
                configureComponents.ConfigureComponent<IHospitalDomainService>(() => new HospitalDomainService(new HospitalDbContext()), DependencyLifecycle.InstancePerCall);
                configureComponents.ConfigureComponent<HospitalDbContext>(DependencyLifecycle.InstancePerUnitOfWork);
            });

            var endpointInstance = await Endpoint
                        .Start(endpointConfiguration)
                        .ConfigureAwait(false);
            try
            {
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
            }
            finally
            {
                await endpointInstance
                        .Stop()
                        .ConfigureAwait(false);
            }
        }
    }
}
