using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ZabbixToItop.Models;
using ZabbixToItop.Services;

namespace ZabbixToItopTests
{
    [TestClass]
    public class UtilsTests
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

            Utils utils = new Utils();

            string ticketJson = Utils.ObjectToJson(ticket);
            ticketJson = ticketJson.Replace(System.Environment.NewLine, "");
            ticketJson = new Regex("[ ]{2,}", RegexOptions.None).Replace(ticketJson, " ");

            string expectedJson = "{ \"operation\": \"core/create\", \"class\": \"UserRequest\", \"comment\": \"Ticket commentary\", \"output_fields\": \"id\", \"fields\": { \"org_id\": 1, \"title\": \"Ticket title teste\", \"description\": \"Ticket description\", \"functionalcis_list\": [ { \"functionalci_id\": 1, \"impact_code\": \"manual\" } ] }}";

            Assert.AreEqual(expectedJson, ticketJson);
        }

        [TestMethod]
        public void Should_Convert_Empty_Ticket_Oject_To_Empty_JSON()
        {
            Ticket ticket = new Ticket();

            Utils utils = new Utils();

            string ticketJson = Utils.ObjectToJson(ticket);
            ticketJson = ticketJson.Replace(System.Environment.NewLine, "");
            ticketJson = new Regex("[ ]{2,}", RegexOptions.None).Replace(ticketJson, " ");

            string expectedJson = "{}";

            Assert.AreEqual(expectedJson, ticketJson);
        }

        [TestMethod]
        public void Should_Return_Correctly_When_Success_Itop_Response()
        {
            string successItopMessage = "{\"objects\":{\"UserRequest::48\":{\"code\":0,\"message\":\"created\",\"class\":\"UserRequest\",\"key\":\"48\",\"fields\":{\"id\":\"48\"}}},\"code\":0,\"message\":null}";

            Utils utils = new Utils();

            ItopResponse utilsResponse = Utils.FormatItopResponse(successItopMessage);

            Assert.AreEqual(utilsResponse.code, 0);
            Assert.AreEqual(utilsResponse.message, "created");
        }

        [TestMethod]
        public void Should_Return_Correctly_When_Fail_Itop_Response()
        {
            string successItopMessage = "{\"code\":100,\"message\":\"Error: Missing parameter 'operation'\"}";

            Utils utils = new Utils();

            ItopResponse utilsResponse = Utils.FormatItopResponse(successItopMessage);

            Assert.AreEqual(utilsResponse.code, 100);
            Assert.AreEqual(utilsResponse.message, "Error: Missing parameter 'operation'");
        }
    }
}
