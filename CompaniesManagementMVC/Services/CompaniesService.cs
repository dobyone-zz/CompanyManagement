using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CompaniesManagementMVC.ViewModels;
using Newtonsoft.Json;

namespace CompaniesManagementMVC.Services
{
    public class CompaniesService : ICompaniesService
    {
        private readonly HttpClient httpClient;
        private readonly string apiCompaniesUrl;

        public CompaniesService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            apiCompaniesUrl  = @"http://localhost:19379/api/Companies/";
        }

        public async Task Create(Company company)
        {
            var jsonString = JsonConvert.SerializeObject(company);
            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var message = await this.httpClient.PostAsync(this.apiCompaniesUrl, httpContent);
        }

        public async Task Delete(int id)
        {
            var uristring = this.apiCompaniesUrl + $"/{id.ToString()}";
            var message = await this.httpClient.DeleteAsync(uristring);
        }

        public async Task<Company> GetById(int id)
        {
            var responseString = await this.httpClient.GetAsync(this.apiCompaniesUrl + $"/{id.ToString()}");
            var company = await responseString.Content.ReadAsAsync<Company>();

            return company;
        }

        public async Task<Companies> GetCompanies()
        {
            var responseString = await this.httpClient.GetAsync(this.apiCompaniesUrl);
            var items = await responseString.Content.ReadAsAsync<List<Company>>();

            return new Companies() { Items = items };
        }

        public async Task Update(Company company)
        {
            var uristring = this.apiCompaniesUrl + $"/{company.Id.ToString()}";
            var jsonString = JsonConvert.SerializeObject(company);
            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var message = await httpClient.PutAsync(uristring, httpContent);
        }
    }
}
