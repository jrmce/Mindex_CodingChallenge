
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using CodeChallenge.Models;
using CodeCodeChallenge.Tests.Integration.Extensions;
using CodeCodeChallenge.Tests.Integration.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeCodeChallenge.Tests.Integration
{
    [TestClass]
    public class CompensationControllerTests
    {
        private static HttpClient _httpClient;
        private static TestServer _testServer;

        [ClassInitialize]
        // Attribute ClassInitialize requires this signature
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static void InitializeClass(TestContext context)
        {
            _testServer = new TestServer();
            _httpClient = _testServer.NewClient();
        }

        [ClassCleanup]
        public static void CleanUpTest()
        {
            _httpClient.Dispose();
            _testServer.Dispose();
        }

        [TestMethod]
        public void CreateCompensation_Returns_Created()
        {
            // Arrange
            var compensation = new Compensation()
            {
                Employee = new Employee()
                {
                    EmployeeId = new Guid().ToString(),
                    Department = "Complaints",
                    FirstName = "Debbie",
                    LastName = "Downer",
                    Position = "Receiver",
                },
                Id = new Guid().ToString(),
                Salary = 60000,
                EffectiveDate = new DateTime()
            };

            var requestContent = new JsonSerialization().ToJson(compensation);

            // Execute
            var postRequestTask = _httpClient.PostAsync("api/compensation",
               new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var response = postRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var newComp = response.DeserializeContent<Compensation>();
            Assert.AreEqual(compensation.Employee.FirstName, newComp.Employee.FirstName);
            Assert.AreEqual(compensation.Employee.LastName, newComp.Employee.LastName);
            Assert.AreEqual(compensation.Employee.Department, newComp.Employee.Department);
            Assert.AreEqual(compensation.Employee.Position, newComp.Employee.Position);
            Assert.AreEqual(compensation.Salary, newComp.Salary);
            Assert.AreEqual(compensation.EffectiveDate, newComp.EffectiveDate);
        }

        [TestMethod]
        public void GetCompensationByEmployeeId_Returns_Ok()
        {
            // Arrange
            var employeeId = "b7839309-3348-463b-a7e3-5de1c168beb3";
            var expectedFirstName = "Paul";
            var expectedLastName = "McCartney";

            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/compensation/{employeeId}");
            var response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var newComp = response.DeserializeContent<Compensation>();
            Assert.AreEqual(expectedFirstName, newComp.Employee.FirstName);
            Assert.AreEqual(expectedLastName, newComp.Employee.LastName);
        }
    }
}
