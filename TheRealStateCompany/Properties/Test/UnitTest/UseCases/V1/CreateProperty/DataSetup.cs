using NUnit.Framework;
using System.Collections.Generic;

namespace UnitTest.UseCases.V1.CreateProperty
{
    public sealed class DataSetup
    {
        public static IEnumerable<TestCaseData> CreatedPropertyOk()
        {
            yield return new TestCaseData(
                "Property Test Ok", "Testing main St, FL", new decimal(400000), new decimal(80000), "TESTPTYFL01", "2015", new decimal(7244102), "FL");
        }

        public static IEnumerable<TestCaseData> CreatedPropertyBadRequest()
        {
            yield return new TestCaseData(
                "Property Test Ok", "Testing main St, FL", new decimal(0), new decimal(0), "TESTPTYFL01", "2015", new decimal(0), "FL");
        }
    }
}
