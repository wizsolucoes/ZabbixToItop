using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ZabbixToItop.Models;
using ZabbixToItop.Services;
using ZabbixToItop.Settings;

namespace ZabbixToItopTests
{
    [TestClass]
    public class ItopConfigurationTests
    {
        [TestMethod]
        public void Should_Initialize_ItopConfiguration_Correctly()
        {
            string[] args = new string[] { "https://testes.com", "", "", "", "", "UserRequest", "Description", "Problem started at 17:10:52 on 2020.11.19^M Problem name: teste novo ping^M Host: Cluster1^M Severity: Disaster^M ^M Original problem ID: 3058^M ^M ^M Equipe: Helpdesk^MHost: Cluster1^M Severidade: Disaster^M Impacto: 2" };

            var settings = new RequestSettings(args);

            Assert.AreEqual(null, settings.Service_name);
            Assert.AreEqual(null, settings.Service_subcategory_name);
            Assert.AreEqual("Cluster1", settings.Title);
            Assert.AreEqual(null, settings.Resource_group_name);
            Assert.AreEqual("Cluster1", settings.Comment);
            Assert.AreEqual("monitoring", settings.Origin);
            Assert.AreEqual("Helpdesk", settings.Team);
            Assert.AreEqual("Description", settings.Description);
            Assert.AreEqual("Cluster1", settings.Ci);
            Assert.AreEqual("Disaster", settings.Urgency);
            Assert.AreEqual("2", settings.Impact);
            Assert.AreEqual("UserRequest", settings.Class);
            Assert.AreEqual("dispatched", settings.Status);
        }
    }
}
