using distribuicao_lucros_domain.Features.Employees;

using FluentAssertions;

using NUnit.Framework;

using System;

namespace distribuicao_lucros_domain_tests.Features.Employees
{
    public class EmployeeTest
    {
        [Test]
        public void Calculate_Profit_Sharing_Should_Return_Base()
        {
            double expectedProfitSharing = 577708.80;

            //Simulamos a data de entrada para garantir que os testes serão executados corretamente
            Func<DateTime> fakeDateFunc = () => new DateTime(2015, 02, 10);

            var employee = new Employee(fakeDateFunc)
            {
                AdmissionDate = new DateTime(2021, 01, 22),
                Department = "Diretoria",
                GrossSalary = 120356,
                Name = "João",
                Registration = 124454354,
                Role = "Diretor financeiro"
            };

            employee.GetProfitSharing().Should().Be(expectedProfitSharing);
        }
    }
}
