using System;
using System.Collections.Generic;
using CustomWorkflowLibrary.HelperClasses;
using Microsoft.Crm.Sdk.Messages;
using NUnit.Framework;

namespace DataConversion.IntegrationTests
{
    [TestFixture]
    public class ConvertToStringIntegrationTests
    {
        private readonly DynamicsWrapper _crm = new DynamicsWrapper();
        private const string IntegrationTestEntityName = "cn_integrationtest";

        public class StringConversionTests
        {
            // DateTime to String
            public StringConversionTests(DateTime inputDateTime, string dateTimeFormat, string expectedOutcome)
            {
                InputDate = inputDateTime;
                DateTimeFormat = dateTimeFormat;
                OutputString = expectedOutcome;
            }

            public DateTime InputDate { get; set; }
            public string DateTimeFormat { get; set; }
            public string OutputString { get; set; }
        }
        

        public static IEnumerable<StringConversionTests> AddDateTimeCases()
        {
            yield return new StringConversionTests(
              new DateTime(2020, 2, 18),
              "dd/MM/yyyy",
              "18/02/2020"
            );

            yield return new StringConversionTests(
                new DateTime(1982, 12, 31),
                "dd/MM/yyyy",
                "31/12/1982"
            );

            yield return new StringConversionTests(
                new DateTime(2019, 3, 26),
                "dd/MM/yyyy",
                "26/03/2019"
            );

            yield return new StringConversionTests(
                new DateTime(1854, 8, 15),
                "dd/MM/yyyy",
                "15/08/1854"
            );
        }

        [Test, TestCaseSource(nameof(AddDateTimeCases))]
        public void ConvertDateTimeToString(StringConversionTests testCase)
        {
            // Arrange
            Dictionary<string, object> attributesDictionary = new Dictionary<string, object>
            {
                {"cn_name", $"Convert DateTime to String - {testCase.InputDate}"},
                { "cn_datetimevalue", testCase.InputDate }
            };
            
            var guid = _crm.CreateCrmRecord(IntegrationTestEntityName, attributesDictionary);
            Console.WriteLine($"Created test record: {guid}");

            // Act
            var request = new ExecuteWorkflowRequest
            {
                EntityId = guid,
                // TODO - query for workflow by name
                WorkflowId = new Guid("731AA509-F313-478C-AFE0-E3CA35153481")
            };
            _crm.Execute(request);

            var processedRecord = _crm.GetCrmRecord(IntegrationTestEntityName, guid, null);

            // Assert
            Assert.AreEqual(testCase.OutputString, processedRecord["cn_textvalue"]);

            // Tear Down
            _crm.DeleteCrmRecord(IntegrationTestEntityName, guid);
        }

    }
}
