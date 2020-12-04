using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using ZabbixToItop.Services;
using ZabbixToItop.Exceptions;
using Moq;
using Moq.Protected;
using ZabbixToItop.Settings;

namespace ZabbixToItopTests
{
    [TestClass]
    public class ItopTests
    {
        [TestMethod]
        public async Task Should_Save_Ticket_With_Success()
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"{""objects"":{""UserRequest::127"":{""code"":0,""message"":""created"",""class"":""UserRequest"",""key"":""127"",""fields"":{""id"":""127""}}},""code"":0,""message"":null}"),
            };

            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(response);
            var httpClient = new HttpClient(handlerMock.Object);

            string[] args = new string[] { "https://testes.com", "", "", "", "", "UserRequest", "Description", "Host: AP - MATRIZ - Leste - Copa Severity: Average EQUIPE:Infra Datacenter, IMPACTO:2, SERVICE: Wifi Ativos de Rede, SERVICESUBCATEGORY:Rede wifi Indisponivel" };

            var settings = new RequestSettings(args);
            var itop = new ItopService(args, httpClient);
            var result = await itop.SaveTicketAsync();
            Assert.AreEqual("code:0 message:created", result);
        }

        [TestMethod]
        [ExpectedException(typeof(ItopException))]
        public async Task Should_Throw_Error_When_Ci_Dont_Exists()
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"{""code"":100,""message"":""Error: org_id: No item found for query: SELECT o FROM FunctionalCI AS fc JOIN Organization AS o ON fc.org_id = o.id WHERE fc.name = 'Cluster111'""}"),
            };

            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(response);
            var httpClient = new HttpClient(handlerMock.Object);

            string[] args = new string[] { "https://testes.com", "", "", "", "", "UserRequest", "Description", "Host: AP - MATRIZ - Leste - Copa Severity: Average EQUIPE:Infra Datacenter, IMPACTO:2, SERVICE: Wifi Ativos de Rede, SERVICESUBCATEGORY:Rede wifi Indisponivel" };
            var settings = new RequestSettings(args);
            var itop = new ItopService(args, httpClient);
            var result = await itop.SaveTicketAsync();
        }
    }
}
