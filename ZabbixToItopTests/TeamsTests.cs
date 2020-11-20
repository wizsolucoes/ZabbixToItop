using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using ZabbixToItop.Exceptions;
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
            var teams = new TeamsService(exception, "https://testes.com", httpClient);
            teams.SendErrorAsync();

            Assert.AreEqual("Error teste", teams.Message);
            Assert.AreEqual("", teams.ErrorCode);
            Assert.AreEqual("https://testes.com", teams.TeamsUrl);
            Assert.AreEqual("System.Exception: Error teste", teams.Body);
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
            var teams = new TeamsService(exception, "https://testes.com", httpClient);
            teams.SendErrorAsync();

            Assert.AreEqual("Error teste", teams.Message);
            Assert.AreEqual("100", teams.ErrorCode);
            Assert.AreEqual("https://testes.com", teams.TeamsUrl);
            Assert.AreEqual("ZabbixToItop.Exceptions.ItopException: Error teste", teams.Body);
        }

        [TestMethod]
        public async Task Should_Initialize_Correctly_With_ItopException()
        {
            var exception = new ItopException("Error teste", 100);
            var teams = new TeamsService(exception, "https://testes.com");

            Assert.AreEqual("Error teste", teams.Message);
            Assert.AreEqual("100", teams.ErrorCode);
            Assert.AreEqual("https://testes.com", teams.TeamsUrl);
            Assert.AreEqual("ZabbixToItop.Exceptions.ItopException: Error teste", teams.Body);
        }

        [TestMethod]
        public async Task Should_Initialize_Correctly_With_Exception()
        {
            var exception = new Exception("Error teste");
            var teams = new TeamsService(exception, "https://testes.com");

            Assert.AreEqual("Error teste", teams.Message);
            Assert.AreEqual("", teams.ErrorCode);
            Assert.AreEqual("https://testes.com", teams.TeamsUrl);
            Assert.AreEqual("System.Exception: Error teste", teams.Body);
        }
    }
}
