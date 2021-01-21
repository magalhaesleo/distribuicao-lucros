using AutoMapper;

using distribuicao_lucros_application.Features.Employees.Dto;

using distribuicao_lucros_domain.Features.Employees;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace distribuicao_lucros_application.Features.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IMapper mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            this.employeeRepository = employeeRepository;
            this.mapper = mapper;
        }

        public async Task Add(IEnumerable<EmployeeDTO> employees)
        {
            var employeesMapped = mapper.Map<IEnumerable<Employee>>(employees);
            
            await employeeRepository.Add(employeesMapped);
        }

        public async Task Delete()
        {
            await employeeRepository.Delete();
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await employeeRepository.GetAll();
        }
    }
}
