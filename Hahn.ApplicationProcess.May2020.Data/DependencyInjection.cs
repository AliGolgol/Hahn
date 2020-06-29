using System.Text.RegularExpressions;
using Hahn.ApplicationProcess.May2020.Data.Repository;
using Hahn.ApplicationProcess.May2020.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hahn.ApplicationProcess.May2020.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IApplicantRepository, ApplicantRepository>();
            
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationContext>(options =>
                    options.UseInMemoryDatabase("HahnApplicantDb"));
            }
            else
            {
                services.AddDbContext<ApplicationContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));
            }
            return services;
        }
    }
}