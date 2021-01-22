using distribuicao_lucros_domain.Features.Employees;
using distribuicao_lucros_domain.Features.ProfitSharing;
using distribuicao_lucros_domain.Helpers;

using FluentAssertions;

using Moq;

using NUnit.Framework;

using System;

namespace distribuicao_lucros_domain_tests.Features.ProfitSharing
{
    public class ProfitSharingCalculatorTest
    {
        private ProfitSharingCalculator profitSharingCalculator;
        private Mock<IDateTimeHelper> dateTimeHelperMock;
        private readonly int minimumSalary = 1100;

        [SetUp]
        public void SetUp()
        {
            dateTimeHelperMock = new Mock<IDateTimeHelper>();
            
            profitSharingCalculator = new ProfitSharingCalculator(dateTimeHelperMock.Object);
        }

        [Test]
        public void Get_Weight_By_AdmissionDate_Minus_Than_One_Year_Should_Be_One()
        {
            int expectedWeight = 1;

            var nowFake = new DateTime(2021, 01, 22);

            dateTimeHelperMock.Setup(d => d.GetDateTimeNow()).Returns(nowFake);

            var employee = new Employee
            {
                AdmissionDate = new DateTime(2020, 12, 05)
            };

            profitSharingCalculator.GetWeightByAdmissionDate(employee).Should().Be(expectedWeight);

            dateTimeHelperMock.Verify(d => d.GetDateTimeNow(), Times.Once);
            dateTimeHelperMock.VerifyNoOtherCalls();
        }

        [Test]
        public void Get_Weight_By_AdmissionDate_Date_Equals_One_Year_Should_Be_One()
        {
            int expectedWeight = 1;

            var admissionDate = new DateTime(2020, 01, 22);

            int OneYearInDays = 365;

            var nowFake = admissionDate.AddDays(OneYearInDays);

            dateTimeHelperMock.Setup(d => d.GetDateTimeNow()).Returns(nowFake);

            var employee = new Employee
            {
                AdmissionDate = admissionDate
            };

            profitSharingCalculator.GetWeightByAdmissionDate(employee).Should().Be(expectedWeight);

            dateTimeHelperMock.Verify(d => d.GetDateTimeNow(), Times.Once);
            dateTimeHelperMock.VerifyNoOtherCalls();
        }

        [Test]
        public void Get_Weight_By_AdmissionDate_Greater_Than_One_Year_And_Minus_Than_Tree_Years_Should_Be_Two()
        {
            int expectedWeight = 2;

            var nowFake = new DateTime(2021, 01, 22);

            dateTimeHelperMock.Setup(d => d.GetDateTimeNow()).Returns(nowFake);

            var employee = new Employee
            {
                AdmissionDate = new DateTime(2019, 12, 05)
            };

            profitSharingCalculator.GetWeightByAdmissionDate(employee).Should().Be(expectedWeight);

            dateTimeHelperMock.Verify(d => d.GetDateTimeNow(), Times.Once);
            dateTimeHelperMock.VerifyNoOtherCalls();
        }

        [Test]
        public void Get_Weight_By_AdmissionDate_Date_Equals_Tree_Years_Should_Be_Two()
        {
            int expectedWeight = 2;

            var admissionDate = new DateTime(2020, 01, 22);

            int treeYearsInDays = 1095;

            var nowFake = admissionDate.AddDays(treeYearsInDays);

            dateTimeHelperMock.Setup(d => d.GetDateTimeNow()).Returns(nowFake);

            var employee = new Employee
            {
                AdmissionDate = admissionDate
            };

            profitSharingCalculator.GetWeightByAdmissionDate(employee).Should().Be(expectedWeight);

            dateTimeHelperMock.Verify(d => d.GetDateTimeNow(), Times.Once);
            dateTimeHelperMock.VerifyNoOtherCalls();
        }


        [Test]
        public void Get_Weight_By_AdmissionDate_Greater_Than_Three_Years_And_Minus_Than_Eight_Years_Should_Be_Tree()
        {
            int expectedWeight = 3;

            var nowFake = new DateTime(2021, 01, 22);

            dateTimeHelperMock.Setup(d => d.GetDateTimeNow()).Returns(nowFake);

            var employee = new Employee
            {
                AdmissionDate = new DateTime(2015, 12, 05)
            };

            profitSharingCalculator.GetWeightByAdmissionDate(employee).Should().Be(expectedWeight);

            dateTimeHelperMock.Verify(d => d.GetDateTimeNow(), Times.Once);
            dateTimeHelperMock.VerifyNoOtherCalls();
        }

        [Test]
        public void Get_Weight_By_AdmissionDate_Date_Equals_Eight_Years_Should_Be_Tree()
        {
            int expectedWeight = 3;

            var admissionDate = new DateTime(2015, 01, 22);

            int eightYearsInDays = 2920;

            var nowFake = admissionDate.AddDays(eightYearsInDays);

            dateTimeHelperMock.Setup(d => d.GetDateTimeNow()).Returns(nowFake);

            var employee = new Employee
            {
                AdmissionDate = admissionDate
            };

            profitSharingCalculator.GetWeightByAdmissionDate(employee).Should().Be(expectedWeight);

            dateTimeHelperMock.Verify(d => d.GetDateTimeNow(), Times.Once);
            dateTimeHelperMock.VerifyNoOtherCalls();
        }

        [Test]
        public void Get_Weight_By_AdmissionDate_Date_Greather_Than_Eight_Years_Should_Be_Five()
        {
            int expectedWeight = 5;

            var admissionDate = new DateTime(2010, 01, 22);

            var nowFake = new DateTime(2021, 01, 22);

            dateTimeHelperMock.Setup(d => d.GetDateTimeNow()).Returns(nowFake);

            var employee = new Employee
            {
                AdmissionDate = admissionDate
            };

            profitSharingCalculator.GetWeightByAdmissionDate(employee).Should().Be(expectedWeight);

            dateTimeHelperMock.Verify(d => d.GetDateTimeNow(), Times.Once);
            dateTimeHelperMock.VerifyNoOtherCalls();
        }

        [Test]
        public void Get_Weight_By_DepartMent_Diretoria_Should_Be_One()
        {
            int expectedWeight = 1;

            var employee = new Employee
            {
                Department = "Diretoria"
            };

            profitSharingCalculator.GetWeightByDepartment(employee).Should().Be(expectedWeight);

            dateTimeHelperMock.VerifyNoOtherCalls();
        }

        [Test]
        public void Get_Weight_By_DepartMent_Contabilidade_Should_Be_Two()
        {
            int expectedWeight = 2;

            var employee = new Employee
            {
                Department = "Contabilidade"
            };

            profitSharingCalculator.GetWeightByDepartment(employee).Should().Be(expectedWeight);

            dateTimeHelperMock.VerifyNoOtherCalls();
        }

        [Test]
        public void Get_Weight_By_DepartMent_Financeiro_Should_Be_Two()
        {
            int expectedWeight = 2;

            var employee = new Employee
            {
                Department = "Financeiro"
            };

            profitSharingCalculator.GetWeightByDepartment(employee).Should().Be(expectedWeight);

            dateTimeHelperMock.VerifyNoOtherCalls();
        }

        [Test]
        public void Get_Weight_By_DepartMent_Tecnologia_Should_Be_Two()
        {
            int expectedWeight = 2;

            var employee = new Employee
            {
                Department = "Tecnologia"
            };

            profitSharingCalculator.GetWeightByDepartment(employee).Should().Be(expectedWeight);

            dateTimeHelperMock.VerifyNoOtherCalls();
        }

        [Test]
        public void Get_Weight_By_DepartMent_Servicos_Gerais_Should_Be_Tree()
        {
            int expectedWeight = 3;

            var employee = new Employee
            {
                Department = "Serviços Gerais"
            };

            profitSharingCalculator.GetWeightByDepartment(employee).Should().Be(expectedWeight);

            dateTimeHelperMock.VerifyNoOtherCalls();
        }

        [Test]
        public void Get_Weight_By_DepartMent_Relacionamento_com_Cliente_Should_Be_Five()
        {
            int expectedWeight = 5;

            var employee = new Employee
            {
                Department = "Relacionamento com o Cliente"
            };

            profitSharingCalculator.GetWeightByDepartment(employee).Should().Be(expectedWeight);

            dateTimeHelperMock.VerifyNoOtherCalls();
        }

        [Test]
        public void Get_Weight_By_DepartMent_Unknown_Department_Should_Be_Throw_InvalidDepartmentException()
        {
            var employee = new Employee
            {
                Department = "Departamento desconhecido"
            };

            Action getUnknownDepartment = () => profitSharingCalculator.GetWeightByDepartment(employee);

            getUnknownDepartment.Should().Throw<InvalidDepartmentException>();
            dateTimeHelperMock.VerifyNoOtherCalls();
        }


        [Test]
        public void Get_Weight_By_Salary_Estagiario_Should_Be_One()
        {
            int expectedWeight = 1;

            var employee = new Employee
            {
                Role = "Estagiário",
                GrossSalary = 1400
            };

            profitSharingCalculator.GetWeightBySalary(employee).Should().Be(expectedWeight);

            dateTimeHelperMock.VerifyNoOtherCalls();
        }

        [Test]
        public void Get_Weight_By_Salary_Using_Minus_Than_Tree_Salary_Weight_Should_Be_One()
        {
            int expectedWeight = 1;

            var employee = new Employee
            {
                GrossSalary = minimumSalary - 1
            };
            
            profitSharingCalculator.GetWeightBySalary(employee).Should().Be(expectedWeight);

            dateTimeHelperMock.VerifyNoOtherCalls();
        }

        [Test]
        public void Get_Weight_By_Salary_Using_Tree_Salary_Weight_Should_Be_One()
        {
            int expectedWeight = 1;

            var employee = new Employee
            {
                GrossSalary = minimumSalary * 3
            };

            profitSharingCalculator.GetWeightBySalary(employee).Should().Be(expectedWeight);

            dateTimeHelperMock.VerifyNoOtherCalls();
        }

        [Test]
        public void Get_Weight_By_Salary_Using_Minus_Than_Five_Salary_Weight_Should_Be_Two()
        {
            int expectedWeight = 2;

            var employee = new Employee
            {
                GrossSalary = (minimumSalary * 5) - 1
            };

            profitSharingCalculator.GetWeightBySalary(employee).Should().Be(expectedWeight);

            dateTimeHelperMock.VerifyNoOtherCalls();
        }


        [Test]
        public void Get_Weight_By_Salary_Using_Minus_Than_Eight_Salary_Weight_Should_Be_Tree()
        {
            int expectedWeight = 3;

            var employee = new Employee
            {
                GrossSalary = (minimumSalary * 8) - 1
            };

            profitSharingCalculator.GetWeightBySalary(employee).Should().Be(expectedWeight);

            dateTimeHelperMock.VerifyNoOtherCalls();
        }

        [Test]
        public void Get_Weight_By_Salary_Using_Greater_Than_Eight_Salary_Weight_Should_Be_Five()
        {
            int expectedWeight = 5;

            var employee = new Employee
            {
                GrossSalary = (minimumSalary * 8) + 1
            };

            profitSharingCalculator.GetWeightBySalary(employee).Should().Be(expectedWeight);

            dateTimeHelperMock.VerifyNoOtherCalls();
        }

        [Test]
        public void Calculate_Profit_Sharing_Result_Should_Be_Expected_Value()
        {
            double expectedProfitSharing = 118800;

            var nowFake = new DateTime(2021, 01, 22);

            dateTimeHelperMock.Setup(d => d.GetDateTimeNow()).Returns(nowFake);

            var employee = new Employee()
            {
                AdmissionDate = new DateTime(2015, 01, 22),
                Department = "Tecnologia",
                GrossSalary = 9900,
                Name = "João",
                Registration = 1234324,
                Role = "Suporte técnico"
            };

            var profitSharingResult = profitSharingCalculator.GetProfitSharing(employee);

            profitSharingResult.Should().Be(expectedProfitSharing);
            dateTimeHelperMock.Verify(d => d.GetDateTimeNow(), Times.Once);
            dateTimeHelperMock.VerifyNoOtherCalls();
        }
    }
}
