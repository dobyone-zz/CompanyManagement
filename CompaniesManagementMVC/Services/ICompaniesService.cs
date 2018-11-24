using CompaniesManagementMVC.ViewModels;
using System.Threading.Tasks;

namespace CompaniesManagementMVC.Services
{
    public interface ICompaniesService
    {
        Task<Companies> GetCompanies();

        Task<Company> GetById(int id);

        Task Create(Company company);

        Task Delete(int id);

        Task Update(Company company);
    }
}
