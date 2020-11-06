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
            Ticket ticketJson = itopApi.GenerateTicket(config);
            Assert.AreEqual(ticketJson.Class, "UserRequest");
            Assert.AreEqual(ticketJson.Fields.Service_id, "SELECT Service WHERE name='Software'");
            Assert.AreEqual(ticketJson.Fields.Functionalcis_list[0].Functionalci_id, "SELECT FunctionalCI WHERE name='Cluster1'");
        }
        
    }
}
