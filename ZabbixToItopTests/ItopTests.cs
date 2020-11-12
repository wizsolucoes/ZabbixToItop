using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using ZabbixToItop.Models;
using ZabbixToItop.Services;
using Moq;
using Moq.Protected;

namespace ZabbixToItopTests
{
    [TestClass]
    public class ItopTests
    {
        [TestMethod]
        public async Task Should_Generate_Ticket()
        {
            string[] args = new string[] { "", "", "", "", "", "UserRequest", "Description", "monitoring", "Cluster1", "4", "Helpdesk", "2", "Software", "Microsoft Office Support", "resourceGroupName" };
            ItopConfiguration config = new ItopConfiguration(args);
            var itop = new Itop(args);
            Ticket ticketJson = await itop.GenerateTicketAsync(config);
            Assert.AreEqual(ticketJson.Class, "UserRequest");
            Assert.AreEqual(ticketJson.Fields.Service_id, "SELECT Service WHERE name='Software'");
            Assert.AreEqual(ticketJson.Fields.Functionalcis_list[0].Functionalci_id, "SELECT FunctionalCI WHERE name='Cluster1'");
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

            string[] args = new string[] { "https://testes.com", "", "", "", "UserRequest", "Description", "monitoring", "Cluster1", "4", "Helpdesk", "2", "Software", "Microsoft Office Support", "resourceGroupName" };
            ItopConfiguration config = new ItopConfiguration(args);
            var itop = new Itop(args, httpClient);
            string ticketJson = Utils.ObjectToJson(await itop.GenerateTicketAsync(config));
            var result = await itop.SaveTicketAsync(ticketJson);
            Assert.AreEqual(result, "code:0 message:created");
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

            string[] args = new string[] { "https://testes.com", "", "", "", "UserRequest", "Description", "monitoring", "Cluster1", "4", "Helpdesk", "2", "Software", "Microsoft Office Support", "resourceGroupName" };
            ItopConfiguration config = new ItopConfiguration(args);
            var itop = new Itop(args, httpClient);
            string ticketJson = Utils.ObjectToJson(await itop.GenerateTicketAsync(config));
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

            string[] args = new string[] { "https://testes.com", "", "", "", "UserRequest", "Description", "monitoring", "Cluster1", "4", "Helpdesk", "2", "Software", "Microsoft Office Support", "resourceGroupName" };
            ItopConfiguration config = new ItopConfiguration(args);
            var itop = new Itop(args, httpClient);
            var result = await itop.GetServiceSubcategoryByCIAsync(config.Ci);
            Assert.AreEqual(result, "SELECT ServiceSubcategory JOIN Service ON ServiceSubcategory.service_id = Service.id WHERE ServiceSubcategory.id='15'");
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

            string[] args = new string[] { "https://testes.com", "", "", "", "UserRequest", "Description", "monitoring", "Cluster1", "4", "Helpdesk", "2", "Software", "Microsoft Office Support", "resourceGroupName" };
            ItopConfiguration config = new ItopConfiguration(args);
            var itop = new Itop(args, httpClient);
            var result = await itop.GetServiceSubcategoryByCIAsync(config.Ci);
        }
    }
}
