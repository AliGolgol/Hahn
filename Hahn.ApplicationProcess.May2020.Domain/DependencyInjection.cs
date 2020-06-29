using System;
using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Hahn.ApplicationProcess.May2020.Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Hahn.ApplicationProcess.May2020.Domain
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetService<ILogger<DomainLog>>();
            services.AddSingleton(typeof(ILogger), logger);
            services.AddScoped<IApplicantService, ApplicantService>();
            return services;
        }
    }
}