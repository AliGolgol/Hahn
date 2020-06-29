using Hahn.ApplicationProcess.May2020.Domain.Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hahn.ApplicationProcess.May2020.Data
{
    public class ApplicationContext:DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options):base(options)
        {
        }

        public DbSet<Applicant> Applicants { get; set; }
    }
}