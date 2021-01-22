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


        /// <summary>
        /// Persiste uma coleção de funcionários na base
        /// </summary>
        /// <param name="employees">Coleção de funcionários</param>
        /// <response code="202">Accepted</response>
        [HttpPost]
        public async Task<IActionResult> Add(IEnumerable<EmployeeDTO> employees)
        {
            await employeeService.Add(employees);

            return Accepted();
        }


        /// <summary>
        /// Busca todas os funcinários armazenados na base
        /// </summary>
        /// <response code="200">Ok</response>
        [HttpGet]
        public async Task<IEnumerable<Employee>> Get()
        {
            return await employeeService.GetAll();
        }

        /// <summary>
        /// Remove todos os funcionários da base
        /// </summary>
        /// <response code="202">Accepted</response>
        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            await employeeService.Delete();

            return Accepted();
        }
    }
}
