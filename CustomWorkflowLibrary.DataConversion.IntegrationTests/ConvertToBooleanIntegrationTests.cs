using System;
using System.Collections.Generic;
using Microsoft.Xrm.Sdk;
using NUnit.Framework;

using CustomWorkflowLibrary.HelperClasses;

namespace CustomWorkflowLibrary.DataConversion.IntegrationTests
{
    [TestFixture]
    public class ConvertToBooleanIntegrationTests
    {
        private readonly DynamicsWrapper _crm = new DynamicsWrapper();
        private const string IntegrationTestEntityName = "cn_integrationtest";

        private readonly List<Guid> _recordsCreated = new List<Guid>();

        /// <summary>
        /// Delete all records created within this fixture
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            foreach (var guid in _recordsCreated)
            {
                _crm.DeleteCrmRecord(IntegrationTestEntityName, guid);
            }
        }

        [Test]
        public void HappyPath()
        {
            // Arrange
            Dictionary<string, object> attributesDictionary = new Dictionary<string, object>();

            attributesDictionary.Add("cn_name", "Happy Path - Boolean Conversion");
            attributesDictionary.Add("cn_booleanvalue", true);

            var guid = _crm.CreateCrmRecord(IntegrationTestEntityName, attributesDictionary);

            _recordsCreated.Add(guid);
            Console.WriteLine($"Created test record: {guid}");

            // Act

            var workflowId = _crm.GetWorkflowIdByName("ManageContractState");
            Console.WriteLine($"Workflow ID to trigger is: {workflowId}");

            // Assert

            Assert.IsNotNull(workflowId);
            Assert.IsNotNull(guid);
        }


    }
}
