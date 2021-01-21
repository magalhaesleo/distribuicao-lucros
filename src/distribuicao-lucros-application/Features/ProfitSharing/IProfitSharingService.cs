using distribuicao_lucros_domain.Features.ProfitSharings;

using System.Threading.Tasks;

namespace distribuicao_lucros_application.Features.ProfitSharing
{
    public interface IProfitSharingService
    {
        Task<ProfitSharingResult> GetProfitSharingResult(double availableValue);
    }
}
