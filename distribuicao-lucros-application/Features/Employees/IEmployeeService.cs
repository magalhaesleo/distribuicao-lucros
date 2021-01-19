using distribuicao_lucros_application.Features.Employees.Dto;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace distribuicao_lucros_application.Features.Employees
{
    public interface IEmployeeService
    {
        Task Add(IEnumerable<EmployeeDTO> employees);
    }
}
