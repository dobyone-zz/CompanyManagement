using System;

namespace CompaniesManagementMVC.ViewModels
{
    public class Employee
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }

        public string Name { get; set; }

        public string ExperienceLevel { get; set; }

        public DateTime StartingDate { get; set; }

        public double Salary { get; set; }

        public int VacationDays { get; set; }
    }
}
