using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace UnitTest.UseCases.V1.UpdateProperty
{
    public sealed class DataSetup
    {
        public static IEnumerable<TestCaseData> UpdatedPropertyOk()
        {
            yield return new TestCaseData(
                new Guid("62DB3D0F-A3AF-4EF0-B430-3B38A682D0F0"), null, "SW 85 St, FL", null, null, null, null, null, null);
        }

        public static IEnumerable<TestCaseData> UpdatedPropertyBadRequest()
        {
            yield return new TestCaseData(
                null, null, "SW 85 St, FL", null, null, null, null, null, null);
        }

        public static IEnumerable<TestCaseData> UpdatedPropertyNotFound()
        {
            yield return new TestCaseData(
                new Guid("00000000-0000-0000-0000-000000000000"), null, "SW 85 St, FL", null, null, null, null, null, null);
        }
    }
}
