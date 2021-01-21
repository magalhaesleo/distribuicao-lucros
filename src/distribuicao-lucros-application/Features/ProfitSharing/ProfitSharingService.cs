using distribuicao_lucros_application.Features.Employees;

using distribuicao_lucros_domain.Features.Employees;
using distribuicao_lucros_domain.Features.ProfitSharing;
using distribuicao_lucros_domain.Features.ProfitSharings;

using distribuicao_lucros_infra.Helpers.Currency;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace distribuicao_lucros_application.Features.ProfitSharing
{
    public class ProfitSharingService : IProfitSharingService
    {
        private readonly IEmployeeService employeeService;

        public ProfitSharingService(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public async Task<ProfitSharingResult> GetProfitSharingResult(double availableValue)
        {
            IEnumerable<Employee> employees = await employeeService.GetAll();
            
            double totalValue = 0;

            var participations = new List<EmployeeProfit>();

            foreach (Employee employee in employees)
            {
                double profitSharing = employee.GetProfitSharing();

                totalValue += profitSharing;

                participations.Add(new EmployeeProfit
                {
                    Matricula = employee.Registration.ToString(),
                    Nome = employee.Name,
                    Valor_Da_Participacao = profitSharing.ToCurrency()
                });
            }

            if (totalValue > availableValue)
                throw new InsufficientValueProfitSharingException($"O valor é insuficiente para cálculo de participação nos lucros. O valor necessário para distribuição é { totalValue.ToCurrency() }");

            return new ProfitSharingResult 
            {
                Participacoes = participations,
                Total_De_Funcionarios = participations.Count.ToString(),
                Total_Distribuido = totalValue.ToCurrency(),
                Total_Disponibilizado = availableValue.ToCurrency(),
                Saldo_Total_Disponibilizado = (availableValue - totalValue).ToCurrency()
            };
        }
    }
}
