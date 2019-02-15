using System;
using System.Collections.Generic;

using NUnit.Framework;

using CustomWorkflowLibrary.HelperClasses;
using Microsoft.Crm.Sdk.Messages;

namespace DataConversion.IntegrationTests
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
        //[TearDown]
        //public void TearDown()
        //{
        //    foreach (var guid in _recordsCreated)
        //    {
        //        _crm.DeleteCrmRecord(IntegrationTestEntityName, guid);
        //    }
        //}

        [Test]
        [TestCase("true", true)]
        [TestCase("false", false)]
        [TestCase("n", false)]
        [TestCase("y", true)]
        [TestCase("yes", true)]
        [TestCase("NO", false)]
        [TestCase("1", true)]
        [TestCase("0", false)]
        public void ConvertStringToBoolean(string input, bool expectedOutput)
        {
            // Arrange
            Dictionary<string, object> attributesDictionary = new Dictionary<string, object>();

            attributesDictionary.Add("cn_name", $"Convert String to Boolean Conversion - {input}");
            attributesDictionary.Add("cn_textvalue", input);

            var guid = _crm.CreateCrmRecord(IntegrationTestEntityName, attributesDictionary);

            _recordsCreated.Add(guid);
            Console.WriteLine($"Created test record: {guid}");

            // Act
            
            ExecuteWorkflowRequest request = new ExecuteWorkflowRequest
            {
                EntityId = guid,
                WorkflowId = new Guid("980EE56B-FBB8-4516-A8AE-B70553F5463E")
                // "CWL - DataConversion - Convert String To Boolean"
            };
            _crm.Execute(request);

            // Assert

            var processedRecord = _crm.GetCrmRecord(IntegrationTestEntityName, guid, null);

            Assert.AreEqual(expectedOutput, processedRecord["cn_booleanvalue"]);

            // Tear Down
            _crm.DeleteCrmRecord(IntegrationTestEntityName, guid);
        }

        [Test]
        [TestCase(1, true)]
        [TestCase(0, false)]
        public void ConvertIntegerToBoolean(int input, bool expectedOutput)
        {
            // Arrange
            Dictionary<string, object> attributesDictionary = new Dictionary<string, object>();

            attributesDictionary.Add("cn_name", $"Convert Int to Boolean - {input}");
            attributesDictionary.Add("cn_wholenumbervalue", input);

            var guid = _crm.CreateCrmRecord(IntegrationTestEntityName, attributesDictionary);

            _recordsCreated.Add(guid);
            Console.WriteLine($"Created test record: {guid}");

            // Act
            ExecuteWorkflowRequest request = new ExecuteWorkflowRequest
            {
                EntityId = guid,
                WorkflowId = new Guid("7EBA7122-74BD-40B2-B621-4BAC2F292956")
                // "CWL - DataConversion - Convert Int To Boolean"
            };
            _crm.Execute(request);

            // Assert
            var processedRecord = _crm.GetCrmRecord(IntegrationTestEntityName, guid, null);

            Assert.AreEqual(expectedOutput, processedRecord["cn_booleanvalue"]);

            // Tear Down
            _crm.DeleteCrmRecord(IntegrationTestEntityName, guid);
        }

    }
}
