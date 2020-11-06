using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using ZabbixToItop.Models;
using ZabbixToItop.Services;

namespace ZabbixToItopTests
{
    [TestClass]
    public class ItopTests
    {
        [TestMethod]
        public void Should_Generate_Ticket()
        {
            string[] args = new string[] { "UserRequest", "Description", "monitoring", "Cluster1", "4", "Helpdesk", "2", "Software", "Microsoft Office Support", "resourceGroupName" };
            Itop itopApi = new Itop();
            ItopConfiguration config = new ItopConfiguration(args);
            string ticketJson = itopApi.GenerateTicket(config);
            ticketJson = ticketJson.Replace(System.Environment.NewLine, "");
            ticketJson = new Regex("[ ]{2,}", RegexOptions.None).Replace(ticketJson, " ");
            string expectedTicketJson = "{ \"operation\": \"core/create\", \"class\": \"UserRequest\", \"comment\": \"Cluster1\", \"status\": \"dispatched\", \"output_fields\": \"id\", \"fields\": { \"org_id\": \"SELECT o FROM FunctionalCI AS fc JOIN Organization AS o ON fc.org_id = o.id WHERE fc.name=\u0027Cluster1\u0027\", \"title\": \"Cluster1\", \"description\": \"Description\", \"functionalcis_list\": [ { \"functionalci_id\": \"SELECT FunctionalCI WHERE name=\u0027Cluster1\u0027\", \"impact_code\": \"manual\" } ], \"team_id\": \"SELECT Team WHERE name = \u0027Helpdesk\u0027\", \"impact\": \"2\", \"urgency\": \"4\", \"origin\": \"monitoring\", \"service_id\": \"SELECT Service WHERE name=\u0027Software\u0027\", \"servicesubcategory_id\": \"SELECT ServiceSubcategory JOIN Service ON ServiceSubcategory.service_id = Service.id WHERE ServiceSubcategory.name=\u0027Microsoft Office Support\u0027 AND Service.name=\u0027Software\u0027\", \"contacts_list\": [ \"Helpdesk\" ], \"caller_id\": { \"first_name\": \"Processo\", \"name\": \"Automatico\" }, \"private_log\": { \"items\": [ { \"date\": \"2020-11-05T17:57:54.9476171-03:00\", \"message\": \"Resource Group: resourceGroupName\" } ] } }}";
            Assert.AreEqual(expectedTicketJson, ticketJson);
        }
        
    }
}
