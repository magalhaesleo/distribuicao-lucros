using AutoMapper;

using distribuicao_lucros_application.Features.Employees;
using distribuicao_lucros_application.Features.Employees.Dto;

using distribuicao_lucros_domain.Features.Employees;

using FluentAssertions;

using Moq;

using NUnit.Framework;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace distribuicao_lucros_application_tests.Features.Employees
{
    public class EmployeeServiceTest
    {
        private EmployeeService employeeService;
        private Mock<IEmployeeRepository> employeeRepositoryMock;
        private Mock<IMapper> mapperMock;

        [SetUp]
        public void SetUp()
        {
            employeeRepositoryMock = new Mock<IEmployeeRepository>();
            mapperMock = new Mock<IMapper>();

            employeeService = new EmployeeService(employeeRepositoryMock.Object, mapperMock.Object);
        }

        [Test]
        public async Task Add_Employee_Collection_Should_Call_Repository()
        {
            var employees = new EmployeeDTO[2];

            var employeesMapped = new Employee[1];

            mapperMock.Setup(m => m.Map<IEnumerable<Employee>>(employees)).Returns(employeesMapped);

            await employeeService.Add(employees);

            employeeRepositoryMock.Verify(e => e.Add(employeesMapped), Times.Once);
            employeeRepositoryMock.VerifyNoOtherCalls();
            mapperMock.Verify(m => m.Map<IEnumerable<Employee>>(employees), Times.Once);
            mapperMock.VerifyNoOtherCalls();
        }

        [Test]
        public async Task Delete_Employee_Should_Call_Repository()
        {
            await employeeService.Delete();

            employeeRepositoryMock.Verify(e => e.Delete(), Times.Once);
            employeeRepositoryMock.VerifyNoOtherCalls();
            mapperMock.VerifyNoOtherCalls();
        }

        [Test]
        public async Task GetAll_Employee_Should_Call_Repository_And_Return_Collection()
        {
            var employees = new Employee[2];

            employeeRepositoryMock.Setup(e => e.GetAll()).Returns(Task.FromResult<IEnumerable<Employee>>(employees));

            var employeesResult = await employeeService.GetAll();

            employeesResult.Should().BeEquivalentTo(employees);
            employeeRepositoryMock.Verify(e => e.GetAll(), Times.Once);
            employeeRepositoryMock.VerifyNoOtherCalls();
            mapperMock.VerifyNoOtherCalls();
        }
    }
}
