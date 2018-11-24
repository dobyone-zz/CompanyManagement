using System;
using CompaniesManagement.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CompaniesManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private EmployeesContext employeesContext;

        public EmployeesController(EmployeesContext context)
        {
            this.employeesContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        // GET: api/Employee
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var items = await this.employeesContext.Employees.ToListAsync();

            return Ok(items);
        }

        // GET: api/Employee/5
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var employee = await this.employeesContext.Employees.SingleOrDefaultAsync(ci => ci.Id == id);

            if (employee == null)
            {
                return NotFound(new { Message = $"Employee with id {id} not found." });
            }

            return Ok(employee);
        }

        // POST: api/Employee
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Employee employee)
        {
            var newEmployee = new Employee
            {
                Name = employee.Name,
                StartingDate = DateTime.Now,
                ExperienceLevel = "B",
                CompanyId = 1,
                Salary = 1000,
                VacationDays = 20
            };

            this.employeesContext.Employees.Add(newEmployee);
            await this.employeesContext.SaveChangesAsync();

            return Ok(newEmployee);
        }

        // PUT: api/Employee/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Employee employeeUpdate)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var employee = await this.employeesContext.Employees.SingleOrDefaultAsync(ci => ci.Id == id);

            if (employee != null)
            {
                employee.Name = employeeUpdate.Name;
                employee.StartingDate = employeeUpdate.StartingDate;
                employee.ExperienceLevel = employeeUpdate.ExperienceLevel;
                employee.Salary = employeeUpdate.Salary;
                employee.VacationDays = employeeUpdate.VacationDays;

                await this.employeesContext.SaveChangesAsync();
                return Ok(new { Message = $"Employee {employee.Name} was updated" });
            }

            return NotFound(new { Message = $"Employee with id {id} not found." });
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var employee = await this.employeesContext.Employees.SingleOrDefaultAsync(ci => ci.Id == id);

            if (employee != null)
            {
                this.employeesContext.Employees.Remove(employee);
                await this.employeesContext.SaveChangesAsync();
                return Ok(new { Message = $"Employee {employee.Name} was deleted." });
            }

            return NotFound(new { Message = $"Employee with id {id} not found." });
        }
    }
}
