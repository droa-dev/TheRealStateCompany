using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTest.UseCases.V1.AddPropertyImage
{
    public class DataSetup
    {
        public static IEnumerable<TestCaseData> AddPropertyImageOk()
        {
            yield return new TestCaseData("TestFile.jpg", Encoding.ASCII.GetBytes("v1v6LvA5giOSqNvYssNjhi/d0YVMcvjINY7L5GJT"), new Guid("62DB3D0F-A3AF-4EF0-B430-3B38A682D0F0"));
        }

        public static IEnumerable<TestCaseData> AddPropertyImageBadRequest()
        {
            yield return new TestCaseData(string.Empty, Encoding.ASCII.GetBytes("v1v6LvA5giOSqNvYssNjhi/d0YVMcvjINY7L5GJT"), Guid.Empty);
        }

        public static IEnumerable<TestCaseData> AddPropertyImageNotFound()
        {
            yield return new TestCaseData("TestFile2.jpg", Encoding.ASCII.GetBytes("v1v6LvA5giOSqNvYssNjhi/d0YVMcvjINY7L5GJT"), new Guid("10DEB25A-0B12-4D48-AE41-89F3DAC2B6F2"));
        }
    }
}
