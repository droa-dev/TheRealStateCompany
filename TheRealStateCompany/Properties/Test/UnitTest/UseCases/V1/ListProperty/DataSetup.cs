using NUnit.Framework;
using System.Collections.Generic;

namespace UnitTest.UseCases.V1.ListProperty
{
    public sealed class DataSetup
    {
        public static IEnumerable<TestCaseData> ListPropertyOk()
        {
            yield return new TestCaseData(
                null, null, new decimal(100000), new decimal(500000), string.Empty, string.Empty);
        }

        public static IEnumerable<TestCaseData> ListPropertyBadRequest()
        {
            yield return new TestCaseData(
                new decimal(0), null, new decimal(0), new decimal(0), null, null);
        }

        public static IEnumerable<TestCaseData> ListPropertyNotFound()
        {
            yield return new TestCaseData(
                null, "CA", new decimal(1), new decimal(100), null, null);
        }
    }
}
