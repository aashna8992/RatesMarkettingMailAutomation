using Autofac;
using Topshelf;
using Topshelf.Autofac;

namespace RatesMarkettingMailAutomation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<RatesMarkettingService>().AsSelf()
                            .InstancePerLifetimeScope();
            var container = builder.Build();
            
            HostFactory.Run(configurator =>
            {
                configurator.SetServiceName("RatesMarkettingService");
                configurator.SetDisplayName("RatesMarkettingService");
                configurator.SetDescription("Sends marketting mails whenever rates drop");

                configurator.RunAsLocalSystem();
                configurator.UseAutofacContainer(container);
                configurator.Service<RatesMarkettingService>(serviceConfigurator =>
                {
                    serviceConfigurator.ConstructUsingAutofacContainer();

                    serviceConfigurator.WhenStarted((service, hostControl) =>
                    {
                        service.OnStart();
                        return true;
                    });
                    serviceConfigurator.WhenStopped((service, hostControl) =>
                    {
                        service.OnStop();
                        return true;
                    });
                });
            });
        }
    }
}
