using distribuicao_lucros_domain.Features.ProfitSharing;

using System.Collections.Generic;

namespace distribuicao_lucros_domain.Features.ProfitSharings
{
    public class ProfitSharingResult
    {
        public IEnumerable<EmployeeProfit> Participacoes { get; set; }
        public string Total_De_Funcionarios { get; set; }
        public string Total_Distribuido { get; set; }
        public string Total_Disponibilizado { get; set; }
        public string Saldo_Total_Disponibilizado { get; set; }
    }
}
