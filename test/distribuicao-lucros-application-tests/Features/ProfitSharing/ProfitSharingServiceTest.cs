using distribuicao_lucros_application.Features.Employees;
using distribuicao_lucros_application.Features.ProfitSharing;

using distribuicao_lucros_domain.Features.Employees;
using distribuicao_lucros_domain.Features.ProfitSharing;
using distribuicao_lucros_domain.Features.ProfitSharings;

using FluentAssertions;

using Moq;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace distribuicao_lucros_application_tests.Features.ProfitSharing
{
    public class ProfitSharingServiceTest
    {
        private ProfitSharingService profitSharingService;
        private Mock<IEmployeeService> employeeServiceMock;
        private Mock<IProfitSharingCalculator> profitSharingCalculatorMock;

        [SetUp]
        public void SetUp()
        {
            employeeServiceMock = new Mock<IEmployeeService>();
            profitSharingCalculatorMock = new Mock<IProfitSharingCalculator>();

            profitSharingService = new ProfitSharingService(employeeServiceMock.Object, profitSharingCalculatorMock.Object);
        }

        [Test]
        public async Task Calculate_Profit_Sharing_Should_Be_Ok()
        {
            double availableValue = 400000;

            var participacoesCount = 2;

            var employeeOne = new Employee
            {
                AdmissionDate = new DateTime(2015, 01, 22),
                Department = "Serviços Gerais",
                GrossSalary = 120356,
                Name = "João",
                Registration = 1234324,
                Role = "Auxiliar de contábilidade"
            };


            var employeeTwo = new Employee
            {
                AdmissionDate = new DateTime(2010, 11, 9),
                Department = "Tecnologia",
                GrossSalary = 9900,
                Name = "João",
                Registration = 1234324,
                Role = "Suporte técnico"
            };

            IEnumerable<Employee> employees = new Employee[]
            {
                employeeOne,
                employeeTwo
            };

            employeeServiceMock.Setup(e => e.GetAll()).Returns(Task.FromResult(employees));

            double profitSharingFakeEmployeeOne = 29999;
            double profitSharingFakeEmployeeTwo = 324324;

            string totalDistribuidoExpected = "R$ 354.323,00";
            string totalDisponibilizadoExpected = "R$ 400.000,00";
            string saldoTotalDisponibilizadoExpected = "R$ 45.677,00";

            profitSharingCalculatorMock.Setup(p => p.GetProfitSharing(employeeOne)).Returns(profitSharingFakeEmployeeOne);
            profitSharingCalculatorMock.Setup(p => p.GetProfitSharing(employeeTwo)).Returns(profitSharingFakeEmployeeTwo);

            ProfitSharingResult profitSharingResult = await profitSharingService.GetProfitSharingResult(availableValue);

            profitSharingResult.Participacoes.Should().HaveCount(participacoesCount);
            profitSharingResult.Total_De_Funcionarios.Should().Be(participacoesCount.ToString());
            profitSharingResult.Total_Distribuido.Should().Be(totalDistribuidoExpected);
            profitSharingResult.Total_Disponibilizado.Should().Be(totalDisponibilizadoExpected);
            profitSharingResult.Saldo_Total_Disponibilizado.Should().Be(saldoTotalDisponibilizadoExpected);
            employeeServiceMock.Verify(e => e.GetAll(), Times.Once);
            employeeServiceMock.VerifyNoOtherCalls();
            profitSharingCalculatorMock.Verify(p => p.GetProfitSharing(employeeOne), Times.Once);
            profitSharingCalculatorMock.Verify(p => p.GetProfitSharing(employeeTwo), Times.Once);
            profitSharingCalculatorMock.VerifyNoOtherCalls();
        }

        [Test]
        public void Calculate_Profit_Sharing_With_Insufficient_Value_Should_Throw_InsufficientValueProfitSharingException()
        {
            double availableValue = 10000;

            var employeeOne = new Employee
            {
                AdmissionDate = new DateTime(2015, 01, 22),
                Department = "Serviços Gerais",
                GrossSalary = 120356,
                Name = "João",
                Registration = 1234324,
                Role = "Auxiliar de contábilidade"
            };


            var employeeTwo = new Employee
            {
                AdmissionDate = new DateTime(2010, 11, 9),
                Department = "Tecnologia",
                GrossSalary = 9900,
                Name = "João",
                Registration = 1234324,
                Role = "Suporte técnico"
            };

            IEnumerable<Employee> employees = new Employee[]
            {
                employeeOne,
                employeeTwo
            };

            employeeServiceMock.Setup(e => e.GetAll()).Returns(Task.FromResult(employees));

            double profitSharingFakeEmployeeOne = 29999;
            double profitSharingFakeEmployeeTwo = 324324;

            profitSharingCalculatorMock.Setup(p => p.GetProfitSharing(employeeOne)).Returns(profitSharingFakeEmployeeOne);
            profitSharingCalculatorMock.Setup(p => p.GetProfitSharing(employeeTwo)).Returns(profitSharingFakeEmployeeTwo);
            
            Func<Task> getProfitSharingAction = async () => await profitSharingService.GetProfitSharingResult(availableValue);

            getProfitSharingAction.Should().Throw<InsufficientValueProfitSharingException>();
            employeeServiceMock.Verify(e => e.GetAll(), Times.Once);
            employeeServiceMock.VerifyNoOtherCalls();
            profitSharingCalculatorMock.Verify(p => p.GetProfitSharing(employeeOne), Times.Once);
            profitSharingCalculatorMock.Verify(p => p.GetProfitSharing(employeeTwo), Times.Once);
            profitSharingCalculatorMock.VerifyNoOtherCalls();
        }
    }
}
