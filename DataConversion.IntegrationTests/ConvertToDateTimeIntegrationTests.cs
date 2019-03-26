using System;
using System.Collections.Generic;

using NUnit.Framework;

using CustomWorkflowLibrary.HelperClasses;
using Microsoft.Crm.Sdk.Messages;

namespace DataConversion.IntegrationTests
{
    [TestFixture]
    public class ConvertToDateTimeIntegrationTests
    {
        private readonly DynamicsWrapper _crm = new DynamicsWrapper();
        private const string IntegrationTestEntityName = "cn_integrationtest";

        private readonly List<Guid> _recordsCreated = new List<Guid>();



    }
}
