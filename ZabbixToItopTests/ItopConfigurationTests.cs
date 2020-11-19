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
            string[] args = new string[] { "https://testes.com", "", "", "", "", "UserRequest", "Description", "Problem started at 17:10:52 on 2020.11.19^M Problem name: teste novo ping^M Host: Cluster1^M Severity: Disaster^M ^M Original problem ID: 3058^M ^M ^M Equipe: Helpdesk^MHost: Cluster1^M Severidade: Disaster^M Impacto: 2" };

            ItopConfiguration config = new ItopConfiguration(args);

            Assert.AreEqual(null, config.Service_name);
            Assert.AreEqual(null, config.Service_subcategory_name);
            Assert.AreEqual("Cluster1", config.Title);
            Assert.AreEqual(null, config.Resource_group_name);
            Assert.AreEqual("Cluster1", config.Comment);
            Assert.AreEqual("monitoring", config.Origin);
            Assert.AreEqual("Helpdesk", config.Team);
            Assert.AreEqual("Description", config.Description);
            Assert.AreEqual("Cluster1", config.Ci);
            Assert.AreEqual("4", config.Urgency);
            Assert.AreEqual("2", config.Impact);
            Assert.AreEqual("UserRequest", config.Class);
            Assert.AreEqual("dispatched", config.Status);
        }

        [TestMethod]
        public void Should_Assert_Urgency_Value_Correctly()
        {
            string[] args = new string[] { "https://testes.com", "", "", "", "", "UserRequest", "Description", "monitoring", "Problem started at 17:30:52 on 2020.11.18^M Problem name: teste novo ping^M Host: Cluster1^M Severity: Disaster^M ^M Original problem ID: 1453^M", "none", "Helpdesk", "2" };

            var config = new ItopConfiguration(args);

            Assert.AreEqual("0", config.CheckUrgency("Not classified"));
            Assert.AreEqual("0", config.CheckUrgency("Information"));
            Assert.AreEqual("1", config.CheckUrgency("Warning"));
            Assert.AreEqual("2", config.CheckUrgency("Average"));
            Assert.AreEqual("3", config.CheckUrgency("High"));
            Assert.AreEqual("4", config.CheckUrgency("Disaster"));
        }
    }
}
