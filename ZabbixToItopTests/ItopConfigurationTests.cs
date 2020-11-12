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
        public void Should_Initialize_ItopConfiguration_With_All_Arguments()
        {
            string[] args = new string[] { "", "", "", "", "", "UserRequest", "Description", "monitoring", "Cluster1", "4", "Helpdesk", "2", "Software", "Microsoft Office Support", "resourceGroupName" };

            ItopConfiguration config = new ItopConfiguration(args);

            Assert.AreEqual(config.Service_name, "Software");
            Assert.AreEqual(config.Service_subcategory_name, "Microsoft Office Support");
            Assert.AreEqual(config.Title, "Cluster1");
            Assert.AreEqual(config.Resource_group_name, "resourceGroupName");
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
        public void Should_Initialize_ItopConfiguration_Without_Resource_group_name()
        {
            string[] args = new string[] { "", "", "", "", "", "UserRequest", "Description", "monitoring", "Cluster1", "4", "Helpdesk", "2", "Software", "Microsoft Office Support" };

            ItopConfiguration config = new ItopConfiguration(args);

            Assert.AreEqual(config.Resource_group_name, null);
        }

        [TestMethod]
        public void Should_Initialize_ItopConfiguration_With_Empty_String_Resource_group_name()
        {
            string[] args = new string[] { "", "", "", "", "", "UserRequest", "Description", "monitoring", "Cluster1", "4", "Helpdesk", "2", "Software", "Microsoft Office Support", "" };

            ItopConfiguration config = new ItopConfiguration(args);

            Assert.AreEqual(config.Resource_group_name, null);
        }

        [TestMethod]
        public void Should_Initialize_ItopConfiguration_Without_Service()
        {
            string[] args = new string[] { "", "", "", "", "", "UserRequest", "Description", "monitoring", "Cluster1", "4", "Helpdesk", "2" };

            ItopConfiguration config = new ItopConfiguration(args);

            Assert.AreEqual(config.Resource_group_name, null);
            Assert.AreEqual(config.Service_name, null);
            Assert.AreEqual(config.Service_subcategory_name, null);
        }

        [TestMethod]
        public void Should_Initialize_ItopConfiguration_With_Empty_String_Service()
        {
            string[] args = new string[] { "", "", "", "", "", "UserRequest", "Description", "monitoring", "Cluster1", "4", "Helpdesk", "2", "", "" };

            var config = new ItopConfiguration(args);

            Assert.AreEqual(config.Resource_group_name, null);
            Assert.AreEqual(config.Service_name, null);
            Assert.AreEqual(config.Service_subcategory_name, null);
        }

        [TestMethod]
        public void Should_Assert_Urgency_Value_Correctly()
        {
            string[] args = new string[] { "", "", "", "", "", "UserRequest", "Description", "monitoring", "Cluster1", "4", "Helpdesk", "2", "", "" };

            var config = new ItopConfiguration(args);

            Assert.AreEqual(config.CheckUrgency("Not classified"), "");
            Assert.AreEqual(config.CheckUrgency("Information"), "");
            Assert.AreEqual(config.CheckUrgency("Warning"), "");
            Assert.AreEqual(config.CheckUrgency("Average"), "");
            Assert.AreEqual(config.CheckUrgency("High"), "");
            Assert.AreEqual(config.CheckUrgency("Disaster"), "");
        }
    }
}
