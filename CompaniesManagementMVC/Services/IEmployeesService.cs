using CompaniesManagementMVC.ViewModels;
using System.Threading.Tasks;

namespace CompaniesManagementMVC.Services
{
    public interface IEmployeesService
    {
        Task<Employees> GetCompanyEmployees(int companyId);

        Task<Employee> GetById(int id);

        Task Create(Employee company);

        Task Delete(int id);

        Task Update(Employee company);
    }
}
