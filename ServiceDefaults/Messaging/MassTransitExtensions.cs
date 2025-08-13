﻿using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceDefaults.Messaging;

public static class MassTransitExtensions
{
    public static IServiceCollection AddMassTransitWithAssemblies
        (this IServiceCollection services, params Assembly[] assemblies)
    {
        services.AddMassTransit(config =>
        {
            config.SetKebabCaseEndpointNameFormatter();
            config.SetInMemorySagaRepositoryProvider();
            
            config.AddConsumers(assemblies);
            config.AddSagaStateMachines(assemblies);
            config.AddSagas(assemblies);
            config.AddActivities(assemblies);
            
            config.UsingRabbitMq((context, configurator) =>
            {
                var configuration = context.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString("rabbitmq");
                
                configurator.Host(connectionString);
                configurator.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}