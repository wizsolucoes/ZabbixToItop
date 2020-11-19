using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using ZabbixToItop.Models;
using ZabbixToItop.Services;
using ZabbixToItop.Exceptions;
using Moq;
using Moq.Protected;
using ZabbixToItop.Settings;
using ZabbixToItop.Util;

namespace ZabbixToItopTests
{
    [TestClass]
    public class ItopTests
    {
        [TestMethod]
        public async Task Should_Generate_Ticket()
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"{""objects"":{""ServiceSubcategory::15"":{""code"":0,""message"":"""",""class"":""ServiceSubcategory"",""key"":""15"",""fields"":{""id"":""15"",""friendlyname"":""Troubleshooting""}}},""code"":0,""message"":""Found: 1""}"),
            };

            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(response);
            var httpClient = new HttpClient(handlerMock.Object);

            string[] args = new string[] { "https://testes.com", "", "", "", "", "UserRequest", "Description", "Problem started at 17:10:52 on 2020.11.19^M Problem name: teste novo ping^M Host: Cluster1^M Severity: Disaster^M ^M Original problem ID: 3058^M ^M ^M Equipe: Helpdesk^MHost: Cluster1^M Severidade: Disaster^M Impacto: 2"};
            var settings = new RequestSettings(args);

            var itop = new Itop(args, httpClient);

            Ticket ticketJson = await itop.GenerateTicketAsync(settings);
            Assert.AreEqual("UserRequest", ticketJson.Class);
            Assert.AreEqual("SELECT Service AS serv JOIN lnkFunctionalCIToService AS lnk ON lnk.service_id = serv.id WHERE functionalci_id_friendlyname = 'Cluster1'", ticketJson.Fields.Service_id);
            Assert.AreEqual("SELECT FunctionalCI WHERE name='Cluster1'", ticketJson.Fields.Functionalcis_list[0].Functionalci_id);
        }

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

            string[] args = new string[] { "https://testes.com", "", "", "", "", "UserRequest", "Description", "Problem started at 17:10:52 on 2020.11.19^M Problem name: teste novo ping^M Host: Cluster1^M Severity: Disaster^M ^M Original problem ID: 3058^M ^M ^M Equipe: Helpdesk^MHost: Cluster1^M Severidade: Disaster^M Impacto: 2" };

            var settings = new RequestSettings(args);
            var itop = new Itop(args, httpClient);
            string ticketJson = Helper.ObjectToJson(await itop.GenerateTicketAsync(settings));
            var result = await itop.SaveTicketAsync(ticketJson);
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

            string[] args = new string[] { "https://testes.com", "", "", "", "", "UserRequest", "Description", "monitoring", "Problem started at 17:30:52 on 2020.11.18^M Problem name: teste novo ping^M Host: Cluster1^M Severity: Disaster^M ^M Original problem ID: 1453^M", "none", "Helpdesk", "2" };
            var settings = new RequestSettings(args);
            var itop = new Itop(args, httpClient);
            string ticketJson = Helper.ObjectToJson(await itop.GenerateTicketAsync(settings));
            var result = await itop.SaveTicketAsync(ticketJson);
        }

        [TestMethod]
        public async Task Should_Execute_Correctly_when_Find_Service_Subcategory()
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"{""objects"":{""ServiceSubcategory::15"":{""code"":0,""message"":"""",""class"":""ServiceSubcategory"",""key"":""15"",""fields"":{""id"":""15"",""friendlyname"":""Troubleshooting""}}},""code"":0,""message"":""Found: 1""}"),
            };

            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(response);
            var httpClient = new HttpClient(handlerMock.Object);

            string[] args = new string[] { "https://testes.com", "", "", "", "", "UserRequest", "Description", "monitoring", "Problem started at 17:30:52 on 2020.11.18^M Problem name: teste novo ping^M Host: Cluster1^M Severity: Disaster^M ^M Original problem ID: 1453^M", "none", "Helpdesk", "2" };
            var settings = new RequestSettings(args);
            var itop = new Itop(args, httpClient);
            var result = await itop.GetServiceSubcategoryByCIAsync(settings.Ci);
            Assert.AreEqual("SELECT ServiceSubcategory JOIN Service ON ServiceSubcategory.service_id = Service.id WHERE ServiceSubcategory.id='15'", result);
        }

        [TestMethod]
        [ExpectedException(typeof(ItopException))]
        public async Task Should_Throw_Error_When_Ci_Dont_Have_Service_Subcategory()
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"{""objects"":null,""code"":0,""message"":""Found: 0""}"),
            };

            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(response);
            var httpClient = new HttpClient(handlerMock.Object);

            string[] args = new string[] { "https://testes.com", "", "", "", "", "UserRequest", "Description", "monitoring", "Problem started at 17:30:52 on 2020.11.18^M Problem name: teste novo ping^M Host: Cluster1^M Severity: Disaster^M ^M Original problem ID: 1453^M", "none", "Helpdesk", "2" };
            var settings = new RequestSettings(args);
            var itop = new Itop(args, httpClient);
            var result = await itop.GetServiceSubcategoryByCIAsync(settings.Ci);
        }

        [TestMethod]
        public async Task Should_Get_Service_Subcategory_When_Null()
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"{""objects"":{""ServiceSubcategory::15"":{""code"":0,""message"":"""",""class"":""ServiceSubcategory"",""key"":""15"",""fields"":{""id"":""15"",""friendlyname"":""Troubleshooting""}}},""code"":0,""message"":""Found: 1""}"),
            };

            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(response);
            var httpClient = new HttpClient(handlerMock.Object);

            string[] args = new string[] { "https://testes.com", "", "", "", "", "UserRequest", "Description", "monitoring", "Problem started at 17:30:52 on 2020.11.18^M Problem name: teste novo ping^M Host: Cluster1^M Severity: Disaster^M ^M Original problem ID: 1453^M", "none", "Helpdesk", "2" };
            var settings = new RequestSettings(args);

            var itop = new Itop(args, httpClient);

            var result = await itop.GenerateTicketAsync(settings);
            Assert.AreEqual("SELECT ServiceSubcategory JOIN Service ON ServiceSubcategory.service_id = Service.id WHERE ServiceSubcategory.id='15'", result.Fields.Servicesubcategory_id);
        }
    }
}
