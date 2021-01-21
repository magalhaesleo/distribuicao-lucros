using System.Collections.Generic;
using System.Threading.Tasks;

namespace distribuicao_lucros_domain.Features.Employees
{
    public interface IEmployeeRepository
    {
        Task Add(IEnumerable<Employee> employees);
        Task<IEnumerable<Employee>> GetAll();
        Task Delete();
    }
}
