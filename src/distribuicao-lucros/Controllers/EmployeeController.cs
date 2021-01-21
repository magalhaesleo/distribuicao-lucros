using distribuicao_lucros_application.Features.Employees;
using distribuicao_lucros_application.Features.Employees.Dto;

using distribuicao_lucros_domain.Features.Employees;

using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace distribuicao_lucros.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        [HttpPost]
        public async Task<IActionResult> Add(IEnumerable<EmployeeDTO> employees)
        {
            await employeeService.Add(employees);

            return Accepted();
        }

        [HttpGet]
        public async Task<IEnumerable<Employee>> Get()
        {
            return await employeeService.GetAll();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            await employeeService.Delete();

            return Accepted();
        }
    }
}
