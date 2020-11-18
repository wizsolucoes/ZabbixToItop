using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZabbixToItop.Models;
using ZabbixToItop.Services;

namespace ZabbixToItopTests
{
    [TestClass]
    public class TeamsTests
    {
        [TestMethod]
        public async Task Should_Send_Exception_Correctly()
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"{}"),
            };

            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(response);
            var httpClient = new HttpClient(handlerMock.Object);

            var exception = new Exception("Error teste");
            var teams = new Teams(exception, "https://testes.com", httpClient);
            teams.SendErrorAsync();

            Assert.AreEqual(teams.Message, "Error teste");
            Assert.AreEqual(teams.ErrorCode, "");
            Assert.AreEqual(teams.TeamsUrl, "https://testes.com");
            Assert.AreEqual(teams.Body, "System.Exception: Error teste");
        }

        [TestMethod]
        public async Task Should_Send_ItopException_Correctly()
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"{}"),
            };

            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(response);
            var httpClient = new HttpClient(handlerMock.Object);

            var exception = new ItopException("Error teste", 100);
            var teams = new Teams(exception, "https://testes.com", httpClient);
            teams.SendErrorAsync();

            Assert.AreEqual(teams.Message, "Error teste");
            Assert.AreEqual(teams.ErrorCode, "100");
            Assert.AreEqual(teams.TeamsUrl, "https://testes.com");
            Assert.AreEqual(teams.Body, "ZabbixToItop.Models.ItopException: Error teste");
        }

        [TestMethod]
        public async Task Should_Initialize_Correctly_With_ItopException()
        {
            var exception = new ItopException("Error teste", 100);
            var teams = new Teams(exception, "https://testes.com");

            Assert.AreEqual(teams.Message, "Error teste");
            Assert.AreEqual(teams.ErrorCode, "100");
            Assert.AreEqual(teams.TeamsUrl, "https://testes.com");
            Assert.AreEqual(teams.Body, "ZabbixToItop.Models.ItopException: Error teste");
        }

        [TestMethod]
        public async Task Should_Initialize_Correctly_With_Exception()
        {
            var exception = new Exception("Error teste");
            var teams = new Teams(exception, "https://testes.com");

            Assert.AreEqual(teams.Message, "Error teste");
            Assert.AreEqual(teams.ErrorCode, "");
            Assert.AreEqual(teams.TeamsUrl, "https://testes.com");
            Assert.AreEqual(teams.Body, "System.Exception: Error teste");
        }
    }
}
