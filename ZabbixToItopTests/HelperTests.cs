using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ZabbixToItop.Models;
using ZabbixToItop.Util;

namespace ZabbixToItopTests
{
    [TestClass]
    public class HeperTests
    {
        [TestMethod]
        public void Should_Convert_Ticket_Oject_To_JSON()
        {
            List<Functionalcis> functionalcis = new List<Functionalcis> {
                new Functionalcis { Functionalci_id = "1", Impact_code = "manual"}
            };

            TicketFields fields = new TicketFields
            {
                Org_id = "1",
                Description = "Ticket description",
                Functionalcis_list = functionalcis,
                Title = "Ticket title teste"
            };

            Ticket ticket = new Ticket
            {
                Operation = "core/create",
                Class = "UserRequest",
                Comment = "Ticket commentary",
                Output_fields = "id",
                Fields = fields
            };

            string ticketJson = Helper.ObjectToJson(ticket);
            ticketJson = ticketJson.Replace(System.Environment.NewLine, "");
            ticketJson = new Regex("[ ]{2,}", RegexOptions.None).Replace(ticketJson, " ");

            string expectedJson = "{ \"operation\": \"core/create\", \"class\": \"UserRequest\", \"comment\": \"Ticket commentary\", \"output_fields\": \"id\", \"fields\": { \"org_id\": \"1\", \"title\": \"Ticket title teste\", \"description\": \"Ticket description\", \"functionalcis_list\": [ { \"functionalci_id\": \"1\", \"impact_code\": \"manual\" } ] }}";

            Assert.AreEqual(expectedJson, ticketJson);
        }

        [TestMethod]
        public void Should_Convert_Empty_Ticket_Oject_To_Empty_JSON()
        {
            Ticket ticket = new Ticket();

            string ticketJson = Helper.ObjectToJson(ticket);
            ticketJson = ticketJson.Replace(System.Environment.NewLine, "");
            ticketJson = new Regex("[ ]{2,}", RegexOptions.None).Replace(ticketJson, " ");

            string expectedJson = "{}";

            Assert.AreEqual(expectedJson, ticketJson);
        }

        [TestMethod]
        public void Should_Return_Correctly_When_Success_Itop_Response()
        {
            string successItopMessage = "{\"objects\":{\"UserRequest::48\":{\"code\":0,\"message\":\"created\",\"class\":\"UserRequest\",\"key\":\"48\",\"fields\":{\"id\":\"48\"}}},\"code\":0,\"message\":null}";

            ItopResponse utilsResponse = Helper.FormatItopResponse(successItopMessage);

            Assert.AreEqual(0, utilsResponse.code);
            Assert.AreEqual("created", utilsResponse.message);
        }

        [TestMethod]
        public void Should_Return_Correctly_When_Fail_Itop_Response()
        {
            string successItopMessage = "{\"code\":100,\"message\":\"Error: Missing parameter 'operation'\"}";

            ItopResponse utilsResponse = Helper.FormatItopResponse(successItopMessage);

            Assert.AreEqual(100, utilsResponse.code);
            Assert.AreEqual("Error: Missing parameter 'operation'", utilsResponse.message);
        }

        [TestMethod]
        public void Should_Get_The_Correct_Value_From_String()
        {
            var response = Helper.GetStringBetween("Host: AP - MATRIZ - Leste - Copa Severity: Average EQUIPE:Infra Datacenter, IMPACTO:2, SERVICE: Wifi Ativos de Rede, SERVICESUBCATEGORY:Rede wifi Indisponivel", "SERVICE:", ", SERVICESUBCATEGORY");
            Assert.AreEqual("Wifi Ativos de Rede", response);
        }


    }
}
