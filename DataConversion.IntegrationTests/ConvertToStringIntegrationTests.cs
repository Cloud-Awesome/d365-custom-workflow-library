using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            public StringConversionTests(DateTime inputDateTime, string expectedOutcome)
            {
                InputDate = inputDateTime;
                OutputString = expectedOutcome;
            }

            public DateTime InputDate { get; set; }
            public string OutputString { get; set; }
        }
        

        public static IEnumerable<StringConversionTests> AddDateTimeCases()
        {
            yield return new StringConversionTests(
              new DateTime(2020, 2, 18),
              "18/2/2020"
            );
        }

        [Test, TestCaseSource(nameof(AddDateTimeCases))]
        public void ConvertDateTimeToString(StringConversionTests testCase)
        {
            // Arrange
            Dictionary<string, object> attributesDictionary = new Dictionary<string, object>
            {
                {"cn_name", $"Convert DateTime to String Conversion - {testCase.InputDate}"},
                { "cn_datetimevalue", testCase.InputDate}
            };
            
            var guid = _crm.CreateCrmRecord(IntegrationTestEntityName, attributesDictionary);

            Console.WriteLine($"Created test record: {guid}");

            // Act

            ExecuteWorkflowRequest request = new ExecuteWorkflowRequest
            {
                EntityId = guid,
                WorkflowId = new Guid("731AA509-F313-478C-AFE0-E3CA35153481")
            };
            _crm.Execute(request);

            // Assert

            var processedRecord = _crm.GetCrmRecord(IntegrationTestEntityName, guid, null);

            Assert.AreEqual(testCase.OutputString, processedRecord["cn_textvalue"]);

            // Tear Down
            _crm.DeleteCrmRecord(IntegrationTestEntityName, guid);
        }

    }
}
