using distribuicao_lucros_domain.Features.Employees;

namespace distribuicao_lucros_domain.Features.ProfitSharing
{
    public interface IProfitSharingCalculator
    {
        double GetProfitSharing(Employee employee);
    }
}
