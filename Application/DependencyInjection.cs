using System.Reflection;
using Application.Common.Behaviours;
using Application.Common.HangfireMediator;
using Application.Common.Interfaces;
using Application.Common.Validators;
using AutoMapper;
using FluentValidation;
using Hangfire;
using Hangfire.MySql;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            
            //START - Pipeline behaviours
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            //END
            
            //START - Internal Services
            services.AddTransient<IEinladecodeVermittlerValidation, EinladecodeVermittlerValidation>();
            services.AddTransient<IHangfireOverMediator, HangfireOverMediator>();
            //END
            
            //START - Hangfire config
            var hangfireConnectionString = configuration.GetConnectionString("HangfireConnection");
            
            services.AddHangfire(hangfireConfiguration =>
            {
                hangfireConfiguration.UseStorage(new MySqlStorage(hangfireConnectionString,
                    new MySqlStorageOptions()));
                hangfireConfiguration.UseSerializerSettings(new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.All
                });
            });

            services.AddHangfireServer();
            //END - Hangfire config
            
            return services;
        }
    }
}