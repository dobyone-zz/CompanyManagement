using CompaniesManagement.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace CompaniesManagement.Data
{
    public class CompaniesContext: DbContext
    {
        public CompaniesContext(DbContextOptions<CompaniesContext> options) : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CompanyEntityTypeConfiguration());
        }
    }
}
