using System;
using CompaniesManagement.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Collections.Generic;

namespace CompaniesManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private CompaniesContext companiesContext;

        public CompaniesController(CompaniesContext context)
        {
            this.companiesContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        // GET: api/Company
        [HttpGet]
        [ProducesResponseType(typeof(List<Company>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            var items = await this.companiesContext.Companies.ToListAsync();

            return Ok(items);
        }

        // GET: api/Company/5
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var company = await this.companiesContext.Companies.SingleOrDefaultAsync(ci => ci.Id == id);

            if (company == null)
            {
                return NotFound(new { Message = $"Company with id {id} not found." });
            }

            return Ok(company);
        }

        // POST: api/Company
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Company company)
        {
            var newCompany = new Company
            {
                Name = company.Name
            };

            this.companiesContext.Companies.Add(newCompany);
            await this.companiesContext.SaveChangesAsync();

            return Ok(newCompany);
        }

        // PUT: api/Company/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Company companyUpdate)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var company = await this.companiesContext.Companies.SingleOrDefaultAsync(ci => ci.Id == id);

            if (company != null)
            {
                company.Name = companyUpdate.Name;
                await this.companiesContext.SaveChangesAsync();
                return Ok(new { Message = $"Company name was changed to {company.Name}." });
            }

            return NotFound(new { Message = $"Company with id {id} not found." });
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var company = await this.companiesContext.Companies.SingleOrDefaultAsync(ci => ci.Id == id);

            if (company != null)
            {
                this.companiesContext.Companies.Remove(company);
                await this.companiesContext.SaveChangesAsync();
                return Ok(new { Message = $"Company {company.Name} was deleted." });
            }

            return NotFound(new { Message = $"Company with id {id} not found." });
        }
    }
}
