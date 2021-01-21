using distribuicao_lucros_domain.Features.Employees;

using distribuicao_lucros_infra_data.Features.Employees;

using Firebase;
using Firebase.Database;

using Moq;

using NUnit.Framework;

using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace distribuicao_lucros_infra_data_tests.Features.Employees
{
    public class EmployeeRepositoryTest
    {
        private EmployeeRepository employeeRepository;
        private Mock<Firebase.IHttpClientFactory> httpClientFactoryMock;

        [SetUp]
        public void SetUp()
        {
            var fakeUrl = string.Empty;

            var options = new FirebaseOptions();

            httpClientFactoryMock = new Mock<Firebase.IHttpClientFactory>();

            options.HttpClientFactory = httpClientFactoryMock.Object;

            var firebaseClient = new FirebaseClient(fakeUrl, options);

            employeeRepository = new EmployeeRepository(firebaseClient);
        }

        [Test]
        public async Task Add_Range_Should_Post_Once_By_Time()
        {
            var fakeEmployees = new Employee[10];

            var httpClientProxyMock = new Mock<IHttpClientProxy>();

            httpClientFactoryMock.Setup(h => h.GetHttpClient(It.IsAny<TimeSpan?>())).Returns(httpClientProxyMock.Object);

            var httpClient = new HttpClient(,);

            httpClientProxyMock.Setup(h => h.GetHttpClient()).Returns()

            await employeeRepository.Add(fakeEmployees);

            httpClientProxyMock.VerifyNoOtherCalls();
            httpClientFactoryMock.VerifyNoOtherCalls();
        }
    }
}
