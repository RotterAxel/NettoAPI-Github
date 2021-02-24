using System;
using Application.Common.HangfireMediator;
using Application.Common.Interfaces;
using Hangfire;
using Hangfire.MySql;
using Infrastructure.Persistence.DbContexts.Insurance;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            //START - DBContext connection and DI Setup
            var connectionString = configuration.GetConnectionString("InsuranceConnection");
            
            services
                .AddDbContext<InsuranceDbContext>(options =>
                {
                    options.UseMySql(connectionString,
                        mySqlOptions =>
                        {
                            mySqlOptions.EnableRetryOnFailure(
                                10,
                                TimeSpan.FromSeconds(30),
                                null);          
                        });
                });
            
            services.AddScoped<IInsuranceDbContext>(provider => provider.GetService<InsuranceDbContext>());
            //END - DBContext connection and DI Setup
            
            
            //Services
            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IAESCryptographyService, AESCryptographyService>();
            services.AddSingleton<IRandomStringGenerator, RandomStringGenerator>();
            services.AddTransient<IVermittlerNoGenerator, VermittlerNoGenerator>();

            //START - Authentication Setup
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.Authority = configuration["Jwt:Authority"];
                o.Audience = configuration["Jwt:Audience"];
                o.RequireHttpsMetadata = false;
            });
            //END - Authentication Setup

            return services;
        }
    }
}