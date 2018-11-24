using System.Collections.Generic;

namespace CompaniesManagementMVC.ViewModels
{
    public class Employees
    {
        public List<Employee> Items { get; set; }

        public int CompanyId { get; set; }
    }
}
