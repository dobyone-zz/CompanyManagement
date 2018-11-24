using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CompaniesManagementMVC.Models;
using CompaniesManagementMVC.Services;
using CompaniesManagementMVC.ViewModels;
using System.Threading.Tasks;
using System;

namespace CompaniesManagementMVC.Controllers
{
    public class HomeController : Controller
    {
        private ICompaniesService companiesService;
        private IEmployeesService employeesService;

        public HomeController(ICompaniesService companiesService, IEmployeesService employeesService)
        {
            this.companiesService = companiesService;
            this.employeesService = employeesService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Companies()
        {
            ViewData["Message"] = "Companies";

            var companies = await companiesService.GetCompanies();

            return View(companies);
        }

        public async Task<IActionResult> Employees(int companyId)
        {
            ViewData["Message"] = "Employees";

            var employees = await employeesService.GetCompanyEmployees(companyId);

            return View(employees);
        }

        public async Task<IActionResult> EditCompany(int id)
        {
            ViewData["Message"] = "Edit company";

            var company = await companiesService.GetById(id);

            return View(company);
        }

        public async Task<IActionResult> EditEmployee(int id)
        {
            ViewData["Message"] = "Edit company";

            var employee = await this.employeesService.GetById(id);

            return View(employee);
        }

        public IActionResult AddCompany()
        {
            ViewData["Message"] = "Add company";

            return View(new Company());
        }

        public IActionResult AddEmployee(int companyId)
        {
            ViewData["Message"] = "Add employee";

            return View(new Employee() { CompanyId = companyId, VacationDays = 20, ExperienceLevel = "A", StartingDate = DateTime.Now  });
        }

        public async Task<IActionResult> DeleteCompany(int id)
        {
            await companiesService.Delete(id);

            return this.RedirectToAction("Companies");
        }

        public async Task<IActionResult> DeleteEmployee(int id, int companyId)
        {
            await this.employeesService.Delete(id);

            return this.RedirectToAction("Employees", new { companyId = companyId });
        }

        [HttpPost]
        public async Task<IActionResult> SaveCompany(Company company)
        {
            await companiesService.Update(company);

            return this.RedirectToAction("Companies");
        }

        [HttpPost]
        public async Task<IActionResult> SaveEmployee(Employee employee)
        {
            await this.employeesService.Update(employee);

            return this.RedirectToAction("Employees", new { companyId = employee.CompanyId });
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompany(Company company)
        {
            await companiesService.Create(company);

            return this.RedirectToAction("Companies");
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(Employee employee)
        {
            await this.employeesService.Create(employee);

            return this.RedirectToAction("Employees", new { companyId = employee.CompanyId });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
