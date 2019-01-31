using System;
using NUnit.Framework;

using CustomWorkflowLibrary.HelperClasses;

namespace CustomWorkflowLibrary.HelperClassesTests
{
    [TestFixture]
    public class DynamicsWrapperUnitTests
    {
        [Test]
        public void DefaultConstructor()
        {
            var crm = new DynamicsWrapper();
            var whoAmI = crm.WhoAmI();
            Console.WriteLine($"{whoAmI.UserId}");

            Assert.IsNotNull(whoAmI.UserId);
        }

    }
}
