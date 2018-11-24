using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using CompaniesManagementMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CompaniesManagementMVC.Services
{
    public class EmployeesService : IEmployeesService
    {
        private readonly HttpClient httpClient;
        private readonly string apiCompaniesUrl;

        public EmployeesService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            apiCompaniesUrl  = @"http://localhost:19379/api/Employees/";
        }

        public async Task Create(Employee company)
        {
            var uristring = this.apiCompaniesUrl;
            var jsonString = JsonConvert.SerializeObject(company);
            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var message = await httpClient.PostAsync(uristring, httpContent);
        }

        public async Task Delete(int id)
        {
            var uristring = this.apiCompaniesUrl + $"/{id.ToString()}";
            var message = await this.httpClient.DeleteAsync(uristring);
        }

        public async Task<Employee> GetById(int id)
        {
            var responseString = await this.httpClient.GetAsync(this.apiCompaniesUrl + $"/{id.ToString()}");
            var company = await responseString.Content.ReadAsAsync<Employee>();

            return company;
        }

        public async Task<Employees> GetCompanyEmployees(int companyId)
        {
            var responseString = await this.httpClient.GetAsync(this.apiCompaniesUrl);
            var items = await responseString.Content.ReadAsAsync<List<Employee>>();

            return new Employees() { CompanyId = companyId, Items = items.Where(x => x.CompanyId == companyId).ToList() };
        }

        public async Task Update(Employee employee)
        {
            var uristring = this.apiCompaniesUrl + $"/{employee.Id.ToString()}";
            var jsonString = JsonConvert.SerializeObject(employee);
            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var message = await httpClient.PutAsync(uristring, httpContent);
        }
    }
}
