using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace UnitTest.UseCases.V1.ChangePropertyPrice
{
    public class DataSetup
    {
        public static IEnumerable<TestCaseData> ChangePropertyPriceOk()
        {
            yield return new TestCaseData(new Guid("62DB3D0F-A3AF-4EF0-B430-3B38A682D0F0"), new decimal(321547), new decimal(38000));
        }

        public static IEnumerable<TestCaseData> ChangePropertyPriceBadRequest()
        {
            yield return new TestCaseData(new Guid("62DB3D0F-A3AF-4EF0-B430-3B38A682D0F0"), new decimal(0), new decimal(0));
        }

        public static IEnumerable<TestCaseData> ChangePropertyPriceNotFound()
        {
            yield return new TestCaseData(new Guid("10DEB25A-0B12-4D48-AE41-89F3DAC2B6F4"), new decimal(321547), new decimal(38000));
        }
    }
}
