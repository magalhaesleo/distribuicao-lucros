using System;

namespace distribuicao_lucros_domain.Features.Employees
{
    public class Employee
    {
        public const string CollectionName = "employee";
        public int Registration { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string Role { get; set; }
        public double GrossSalary { get; set; }
        public DateTime AdmissionDate { get; set; }

        private readonly Func<DateTime> Now;

        public Employee(Func<DateTime> nowFunc = null)
        {
            if (nowFunc == null)
                Now = () => DateTime.Now;
            else
                Now = nowFunc;
        }

        private int GetWeightByAdmissionDate()
        {
            var now = Now();

            int days = (now - AdmissionDate).Days;

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

        private int GetWeightByDepartment()
        {
            switch (Department)
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

        private int GetWeightBySalary()
        {
            int basicSalary = 1100; // Por enquanto mantemos o padrão do salário minimo de 2021
                                    // Porém poderiamos buscar em outro lugar essa informação, como um banco de dados ou uma API externa

            if (Role == "Estagiário" || GrossSalary <= (basicSalary * 3))
                return 1;

            if (GrossSalary < (basicSalary * 5))
                return 2;

            if (GrossSalary < (basicSalary * 8))
                return 3;

            return 5;
        }

        public double GetProfitSharing()
        {
            int pta = GetWeightByAdmissionDate();

            int paa = GetWeightByDepartment();

            int pfs = GetWeightBySalary();

            return Math.Round((((GrossSalary * pta) + (GrossSalary * paa)) /  pfs) * 12, 2);
        }
    }
}
