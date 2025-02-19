using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Play.Common.Common;
using Play.Common.Exceptions;
using Play.Common.Service.Settings;
using Play.Common.Settings;

namespace Play.Common.MassTransit;

public static class Extension
{
    public static void AddMassTransitWithRabbitMq(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(configure =>
        {
            var rabbitMqSettings = configuration.GetSection(nameof(RabbitMqSettings)).Get<RabbitMqSettings>();
            if (string.IsNullOrWhiteSpace(rabbitMqSettings!.Host))
                throw new SettingException(MessageError.HostNotProvided, rabbitMqSettings);

            var serviceSettings = configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
            if (string.IsNullOrWhiteSpace(serviceSettings!.ServiceName))
                throw new SettingException(MessageError.ServiceNameNotProvided, serviceSettings);

            configure.AddConsumers(Assembly.GetEntryAssembly());

            configure.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host(rabbitMqSettings!.Host);
                configurator.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter(serviceSettings!.ServiceName, false));
                configurator.UseMessageRetry(retryConfigurator =>
                {
                    retryConfigurator.Interval(Constants.QuantityRetriesShippingConsumer, TimeSpan.FromSeconds(Constants.TimeIntervalBetweenRetries));
                });
            });
        });
    }
}