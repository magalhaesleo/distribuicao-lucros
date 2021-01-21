using distribuicao_lucros_application.Features.ProfitSharing;

using distribuicao_lucros_domain.Features.ProfitSharing;
using distribuicao_lucros_domain.Features.ProfitSharings;

using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

namespace distribuicao_lucros.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfitSharingController : ControllerBase
    {
        private readonly IProfitSharingService profitSharingService;

        public ProfitSharingController(IProfitSharingService profitSharingService)
        {
            this.profitSharingService = profitSharingService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(double availableValue)
        {
            try
            {
                ProfitSharingResult profitSharingResult = await profitSharingService.GetProfitSharingResult(availableValue);

                return Ok(profitSharingResult);
            }
            catch (InsufficientValueProfitSharingException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
