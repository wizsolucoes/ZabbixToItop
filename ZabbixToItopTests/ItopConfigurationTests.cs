using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ZabbixToItop.Models;
using ZabbixToItop.Services;

namespace ZabbixToItopTests
{
    [TestClass]
    public class ItopConfigurationTests
    {
        [TestMethod]
        public void Should_Initialize_ItopConfiguration_Correctly()
        {
            string[] args = new string[] { "https://testes.com", "", "", "", "", "UserRequest", "Description", "monitoring", "Problem started at 17:30:52 on 2020.11.18^M Problem name: teste novo ping^M Host: Cluster1^M Severity: Disaster^M ^M Original problem ID: 1453^M", "none", "Helpdesk", "2" };
            
            ItopConfiguration config = new ItopConfiguration(args);

            Assert.AreEqual(config.Service_name, null);
            Assert.AreEqual(config.Service_subcategory_name, null);
            Assert.AreEqual(config.Title, "Cluster1");
            Assert.AreEqual(config.Resource_group_name, null);
            Assert.AreEqual(config.Comment, "Cluster1");

            Assert.AreEqual(config.Origin, "monitoring");
            Assert.AreEqual(config.Team, "Helpdesk");
            Assert.AreEqual(config.Description, "Description");
            Assert.AreEqual(config.Ci, "Cluster1");
            Assert.AreEqual(config.Urgency, "4");
            Assert.AreEqual(config.Impact, "2");
            Assert.AreEqual(config.Class, "UserRequest");
            Assert.AreEqual(config.Status, "dispatched");
        }

        [TestMethod]
        public void Should_Assert_Urgency_Value_Correctly()
        {
            string[] args = new string[] { "https://testes.com", "", "", "", "", "UserRequest", "Description", "monitoring", "Problem started at 17:30:52 on 2020.11.18^M Problem name: teste novo ping^M Host: Cluster1^M Severity: Disaster^M ^M Original problem ID: 1453^M", "none", "Helpdesk", "2" };

            var config = new ItopConfiguration(args);

            Assert.AreEqual(config.CheckUrgency("Not classified"), "0");
            Assert.AreEqual(config.CheckUrgency("Information"), "0");
            Assert.AreEqual(config.CheckUrgency("Warning"), "1");
            Assert.AreEqual(config.CheckUrgency("Average"), "2");
            Assert.AreEqual(config.CheckUrgency("High"), "3");
            Assert.AreEqual(config.CheckUrgency("Disaster"), "4");
        }
    }
}
