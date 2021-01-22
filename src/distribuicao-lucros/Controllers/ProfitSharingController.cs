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

        /// <summary>
        /// Calcula a participação nos lucros de todos os funcionários presentes na base e retorna
        /// a bonificação de cada funcionário.
        /// Se <paramref name="availableValue"/> não for suficiente retorna uma mensagem de erro informando o valor necessário
        /// </summary>
        /// <param name="availableValue">Valor disponível para cálculo de participação nos lucros</param>
        /// <response code="200">Ok</response>
        /// <response code="400">BadRequest</response>
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
