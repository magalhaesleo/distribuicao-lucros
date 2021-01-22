using distribuicao_lucros_domain.Features.Employees;

using FluentAssertions;

using NUnit.Framework;

using System;

namespace distribuicao_lucros_domain_tests.Features.Employees
{
    public class EmployeeTest
    {
        [Test]
        public void Calculate_Profit_Sharing_Diretoria_Department_Should_Use_Weight_One()
        {
            double expectedProfitSharing = 1155417.6;

            //Simulamos a data de entrada para garantir que os testes serão executados corretamente
            static DateTime fakeDateFunc() => new DateTime(2021, 02, 10);

            var employee = new Employee(fakeDateFunc)
            {
                AdmissionDate = new DateTime(2015, 08, 22),
                Department = "Diretoria",
                GrossSalary = 120356,
                Name = "João",
                Registration = 124454354,
                Role = "Diretor financeiro"
            };

            employee.GetProfitSharing().Should().Be(expectedProfitSharing);
        }

        [Test]
        public void Calculate_Profit_Sharing_Contabilidade_Department_Should_Use_Weight_Two()
        {
            double expectedProfitSharing = 1444272.0;

            //Simulamos a data de entrada para garantir que os testes serão executados corretamente
            static DateTime fakeDateFunc() => new DateTime(2021, 02, 10);

            var employee = new Employee(fakeDateFunc)
            {
                AdmissionDate = new DateTime(2014, 01, 22),
                Department = "Contabilidade",
                GrossSalary = 120356,
                Name = "João",
                Registration = 1234324,
                Role = "Auxiliar de contábilidade"
            };

            employee.GetProfitSharing().Should().Be(expectedProfitSharing);
        }

        [Test]
        public void Calculate_Profit_Sharing_Servicos_Gerais_Department_Should_Use_Weight_Tree()
        {
            double expectedProfitSharing = 1733126.4;

            //Simulamos a data de entrada para garantir que os testes serão executados corretamente
            static DateTime fakeDateFunc() => new DateTime(2021, 02, 10);

            var employee = new Employee(fakeDateFunc)
            {
                AdmissionDate = new DateTime(2015, 01, 22),
                Department = "Serviços Gerais",
                GrossSalary = 120356,
                Name = "João",
                Registration = 1234324,
                Role = "Auxiliar de contábilidade"
            };

            employee.GetProfitSharing().Should().Be(expectedProfitSharing);
        }

        [Test]
        public void Calculate_Profit_Sharing_Using_Salary_Greater_Than_Eight()
        {
            double expectedProfitSharing = 166320.0;

            //Simulamos a data de entrada para garantir que os testes serão executados corretamente
            static DateTime fakeDateFunc() => new DateTime(2020, 02, 10);

            var employee = new Employee(fakeDateFunc)
            {
                AdmissionDate = new DateTime(2010, 01, 22),
                Department = "Tecnologia",
                GrossSalary = 9900,
                Name = "João",
                Registration = 1234324,
                Role = "Suporte técnico"
            };

            employee.GetProfitSharing().Should().Be(expectedProfitSharing);
        }

        [Test]
        public void Calculate_Profit_Sharing_Using_Minus_Than_One_Year()
        {
            double expectedProfitSharing = 71280.0;

            //Simulamos a data de entrada para garantir que os testes serão executados corretamente
            static DateTime fakeDateFunc() => new DateTime(2021, 01, 10);

            var employee = new Employee(fakeDateFunc)
            {
                AdmissionDate = new DateTime(2021, 01, 22),
                Department = "Tecnologia",
                GrossSalary = 9900,
                Name = "João",
                Registration = 1234324,
                Role = "Suporte técnico"
            };

            employee.GetProfitSharing().Should().Be(expectedProfitSharing);
        }
    }
}
