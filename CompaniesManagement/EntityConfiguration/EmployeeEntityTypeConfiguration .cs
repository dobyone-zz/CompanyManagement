using CompaniesManagement.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompaniesManagement.EntityConfiguration
{
    public class EmployeeEntityTypeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");

            builder.Property(ci => ci.Id)
                .ForSqlServerUseSequenceHiLo("company_hilo")
                .IsRequired();

            builder.Property(ci => ci.CompanyId)
                .IsRequired(true);

            builder.Property(ci => ci.Name)
                .IsRequired(true)
                .HasMaxLength(250);

            builder.Property(ci => ci.ExperienceLevel)
                .IsRequired(true)
                .HasMaxLength(1);

            builder.Property(ci => ci.StartingDate)
                .IsRequired();

            builder.Property(ci => ci.Salary)
                .IsRequired();

            builder.Property(ci => ci.VacationDays)
                .IsRequired();
        }
    }
}
