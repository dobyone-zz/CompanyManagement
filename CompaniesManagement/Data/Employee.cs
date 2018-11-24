using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompaniesManagement.Data
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
