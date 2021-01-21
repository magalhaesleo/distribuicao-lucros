using distribuicao_lucros_domain.Features.Employees;

using distribuicao_lucros_infra_data.Features.Employees;

using Firebase;
using Firebase.Database;

using FluentAssertions;

using Moq;
using Moq.Protected;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
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
            var fakeUrl = "http://fakedatabase.com";

            var options = new FirebaseOptions();

            httpClientFactoryMock = new Mock<IHttpClientFactory>();

            options.HttpClientFactory = httpClientFactoryMock.Object;

            var firebaseClient = new FirebaseClient(fakeUrl, options);

            employeeRepository = new EmployeeRepository(firebaseClient);
        }

        [Test]
        public async Task Add_Range_Should_Post_Once_By_Time()
        {
            var fakeEmployees = new Employee[2];

            var httpClientProxyMock = new Mock<IHttpClientProxy>();

            httpClientFactoryMock.Setup(h => h.GetHttpClient(It.IsAny<TimeSpan?>())).Returns(httpClientProxyMock.Object);

            var httpClientHandlerMock = new Mock<HttpClientHandler>();

            using var requestResult = new HttpResponseMessage(HttpStatusCode.OK);
            requestResult.Content = new StringContent("OK");

            httpClientHandlerMock
                                 .Protected()
                                 .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                                 .ReturnsAsync(requestResult);

            var httpClient = new HttpClient(httpClientHandlerMock.Object);

            httpClientProxyMock.Setup(h => h.GetHttpClient()).Returns(httpClient);

            await employeeRepository.Add(fakeEmployees);

            httpClientHandlerMock.Protected().Verify("SendAsync", Times.Exactly(fakeEmployees.Count()), ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>());
            httpClientHandlerMock.VerifyNoOtherCalls();
            httpClientProxyMock.Verify(h => h.GetHttpClient(), Times.Exactly(2));
            httpClientProxyMock.VerifyNoOtherCalls();
            httpClientFactoryMock.VerifyNoOtherCalls();
        }

        [Test]
        public async Task Delete_Employees_Should_Call_Server_Api()
        {
            var httpClientProxyMock = new Mock<IHttpClientProxy>();

            httpClientFactoryMock.Setup(h => h.GetHttpClient(It.IsAny<TimeSpan?>())).Returns(httpClientProxyMock.Object);

            var httpClientHandlerMock = new Mock<HttpClientHandler>();

            using var requestResult = new HttpResponseMessage(HttpStatusCode.OK);
            requestResult.Content = new StringContent("OK");

            httpClientHandlerMock
                                 .Protected()
                                 .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                                 .ReturnsAsync(requestResult);

            var httpClient = new HttpClient(httpClientHandlerMock.Object);

            httpClientProxyMock.Setup(h => h.GetHttpClient()).Returns(httpClient);

            await employeeRepository.Delete();

            httpClientHandlerMock.Protected().Verify("SendAsync", Times.Once(), ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>());
            httpClientHandlerMock.VerifyNoOtherCalls();
            httpClientProxyMock.Verify(h => h.GetHttpClient(), Times.Once);
            httpClientProxyMock.VerifyNoOtherCalls();
            httpClientFactoryMock.VerifyNoOtherCalls();
        }

        [Test]
        public async Task GetAll_Employees_Should_Return_One()
        {
            var httpClientProxyMock = new Mock<IHttpClientProxy>();

            httpClientFactoryMock.Setup(h => h.GetHttpClient(It.IsAny<TimeSpan?>())).Returns(httpClientProxyMock.Object);

            var httpClientHandlerMock = new Mock<HttpClientHandler>();

            using var requestResult = new HttpResponseMessage(HttpStatusCode.OK);

            var fakeResponse = "{\"-MRX7YykQrEOVvRls6ZC\":{\"AdmissionDate\":\"2012-01-05T00:00:00\",\"Department\":\"Diretoria\",\"GrossSalary\":12696.2,\"Name\":\"Victor Wilson\",\"Registration\":9968,\"Role\":\"Diretor Financeiro\"}}";

            requestResult.Content = new StringContent(fakeResponse);

            httpClientHandlerMock
                                 .Protected()
                                 .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                                 .ReturnsAsync(requestResult);

            var httpClient = new HttpClient(httpClientHandlerMock.Object);

            httpClientProxyMock.Setup(h => h.GetHttpClient()).Returns(httpClient);

            IEnumerable<Employee> emloyees = await employeeRepository.GetAll();

            emloyees.Should().HaveCount(1);
            httpClientHandlerMock.Protected().Verify("SendAsync", Times.Once(), ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>());
            httpClientHandlerMock.VerifyNoOtherCalls();
            httpClientProxyMock.Verify(h => h.GetHttpClient(), Times.Once);
            httpClientProxyMock.VerifyNoOtherCalls();
            httpClientFactoryMock.VerifyNoOtherCalls();
        }
    }
}
