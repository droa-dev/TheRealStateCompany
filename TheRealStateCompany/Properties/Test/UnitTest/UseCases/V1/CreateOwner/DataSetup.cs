using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace UnitTest.UseCases.V1.CreateOwner
{
    public class DataSetup
    {
        public static IEnumerable<TestCaseData> CreatedOwnerOk()
        {
            yield return new TestCaseData(new decimal(52102415), "Henry Test", "Testing main St, FL", null, DateTime.Now);            
        }

        public static IEnumerable<TestCaseData> CreatedOwnerBadRequest()
        {
            yield return new TestCaseData(new decimal(0), "Henry Test", "Testing main St, FL", null, DateTime.Now);
        }
    }
}
