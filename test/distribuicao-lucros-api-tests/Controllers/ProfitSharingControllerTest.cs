using distribuicao_lucros.Controllers;

using distribuicao_lucros_application.Features.ProfitSharing;

using distribuicao_lucros_domain.Features.ProfitSharing;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Moq;

using NUnit.Framework;

using System.Threading.Tasks;

namespace distribuicao_lucros_api_tests.Controllers
{
    public class ProfitSharingControllerTest
    {
        private ProfitSharingController profitSharingController;
        private Mock<IProfitSharingService> profitSharingServiceMock;

        [SetUp]
        public void SetUp()
        {
            profitSharingServiceMock = new Mock<IProfitSharingService>();

            profitSharingController = new ProfitSharingController(profitSharingServiceMock.Object);
        }

        [Test]
        public async Task ProfitSharing_Should_Call_Service_And_Return_Code_200()
        {
            double availableValue = 200000;

            IActionResult result = await profitSharingController.Get(availableValue);

            result.Should().BeOfType<OkObjectResult>();
            profitSharingServiceMock.Verify(p => p.GetProfitSharingResult(availableValue), Times.Once);
            profitSharingServiceMock.VerifyNoOtherCalls();
        }

        [Test]
        public async Task ProfitSharing_Should_Return_BadRequest_When_Throw_InsufficientValueProfitSharingException()
        {
            double availableValue = 200000;

            profitSharingServiceMock.Setup(p => p.GetProfitSharingResult(availableValue)).Throws(new InsufficientValueProfitSharingException(string.Empty));

            IActionResult result = await profitSharingController.Get(availableValue);

            result.Should().BeOfType<BadRequestObjectResult>();
            profitSharingServiceMock.Verify(p => p.GetProfitSharingResult(availableValue), Times.Once);
            profitSharingServiceMock.VerifyNoOtherCalls();
        }
    }
}
