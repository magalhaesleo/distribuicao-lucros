using distribuicao_lucros.Controllers;

using distribuicao_lucros_application.Features.Employees;
using distribuicao_lucros_application.Features.Employees.Dto;

using distribuicao_lucros_domain.Features.Employees;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Moq;

using NUnit.Framework;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace distribuicao_lucros_api_tests.Controllers
{
    public class EmployeeControllerTest
    {
        private EmployeeController employeeController;
        private Mock<IEmployeeService> employeeServiceMock;

        [SetUp]
        public void SetUp()
        {
            employeeServiceMock = new Mock<IEmployeeService>();

            employeeController = new EmployeeController(employeeServiceMock.Object);
        }

        [Test]
        public async Task Add_Employee_Collection_Should_Call_Service()
        {
            var employees = new EmployeeDTO[2];

            IActionResult response = await employeeController.Add(employees);

            response.Should().BeOfType<AcceptedResult>();
            employeeServiceMock.Verify(e => e.Add(employees), Times.Once);
            employeeServiceMock.VerifyNoOtherCalls();
        }

        [Test]
        public async Task GetAll_Employee_Should_Call_Service_And_Return_Collection()
        {
            int employeesCount = 2;
            var employees = new Employee[employeesCount];

            employeeServiceMock.Setup(e => e.GetAll()).Returns(Task.FromResult<IEnumerable<Employee>>(employees));

            IEnumerable<Employee> employeesResult = await employeeController.Get();

            employeesResult.Should().HaveCount(employeesCount);
            employeeServiceMock.Verify(e => e.GetAll(), Times.Once);
            employeeServiceMock.VerifyNoOtherCalls();
        }

        [Test]
        public async Task Delte_Employee_Should_Call_Service()
        {
            IActionResult response = await employeeController.Delete();

            response.Should().BeOfType<AcceptedResult>();
            employeeServiceMock.Verify(e => e.Delete(), Times.Once);
            employeeServiceMock.VerifyNoOtherCalls();
        }
    }
}
