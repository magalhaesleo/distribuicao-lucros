using distribuicao_lucros_domain.Features.Employees;
using distribuicao_lucros_domain.Helpers;

using System;

namespace distribuicao_lucros_domain.Features.ProfitSharing
{
    public class ProfitSharingCalculator : IProfitSharingCalculator
    {
        private readonly IDateTimeHelper dateTimeHelper;

        public ProfitSharingCalculator(IDateTimeHelper dateTimeHelper)
        {
            this.dateTimeHelper = dateTimeHelper;
        }

        public int GetWeightByAdmissionDate(Employee employee)
        {
            DateTime now = dateTimeHelper.GetDateTimeNow();

            int days = (now - employee.AdmissionDate).Days;

            if (days <= 365) // Até 1 ano
            {
                return 1;
            }
            else if (days <= 1095) // Até 3 anos
            {
                return 2;
            }
            else if (days <= 2920) // Até 8 anos
            {
                return 3;
            }

            // Acima de 8 anos
            // Não é necessário adicionar condição pois não existe outra possibilidade
            return 5;
        }

        public int GetWeightByDepartment(Employee employee)
        {
            switch (employee.Department)
            {
                case "Diretoria":
                    return 1;
                case "Contabilidade":
                case "Financeiro":
                case "Tecnologia":
                    return 2;
                case "Serviços Gerais":
                    return 3;
                case "Relacionamento com o Cliente":
                    return 5;
            }

            throw new InvalidDepartmentException();
        }

        public int GetWeightBySalary(Employee employee)
        {
            int basicSalary = 1100; // Por enquanto mantemos o padrão do salário minimo de 2021
                                    // Porém poderiamos buscar em outro lugar essa informação, como um banco de dados ou uma API externa

            if (employee.Role == "Estagiário" || employee.GrossSalary <= (basicSalary * 3))
                return 1;

            if (employee.GrossSalary < (basicSalary * 5))
                return 2;

            if (employee.GrossSalary < (basicSalary * 8))
                return 3;

            return 5;
        }

        public double GetProfitSharing(Employee employee)
        {
            int pta = GetWeightByAdmissionDate(employee);

            int paa = GetWeightByDepartment(employee);

            int pfs = GetWeightBySalary(employee);

            return Math.Round((((employee.GrossSalary * pta) + (employee.GrossSalary * paa)) / pfs) * 12, 2);
        }
    }
}
