using AutoMapper;

using distribuicao_lucros_application.Features.Employees.Dto;
using distribuicao_lucros_application.Features.Employees.Mappers;

using distribuicao_lucros_domain.Features.Employees;

using FluentAssertions;

using NUnit.Framework;

using System.Collections.Generic;
using System.Linq;

namespace distribuicao_lucros_application_tests.Features.Employees.Mappers
{
    public class EmployeeProfileTest
    {
        private IMapper mapper;

        [SetUp]
        public void SetUp()
        {
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<EmployeeProfile>());

            mapperConfiguration.AssertConfigurationIsValid();

            mapper = mapperConfiguration.CreateMapper();
        }

        [Test]
        public void Map_EmployeeDto_To_Employee_Should_Be_Ok()
        {
            var element = new EmployeeDTO
            {
                Matricula = "0009968",
                Nome = "Victor Wilson",
                Area = "Diretoria",
                Cargo = "Diretor Financeiro",
                Salario_Bruto = "R$ 12.696,20",
                Data_De_Admissao = "2012-01-05"
            };

            var employees = new EmployeeDTO[] 
            {
                element
            };

            IEnumerable<Employee> employeesMapped = mapper.Map<IEnumerable<Employee>>(employees);

            employeesMapped.Should().HaveCount(employees.Count());
        }
    }
}
