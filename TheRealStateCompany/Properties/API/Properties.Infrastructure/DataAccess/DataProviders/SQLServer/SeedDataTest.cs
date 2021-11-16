using Properties.Domain.ValueObjects;
using System;

namespace Properties.Infrastructure.DataAccess.DataProviders.SQLServer
{
    public static class SeedDataTest
    {
        #region DefaultOwner
        public static readonly OwnerGuid DefaultOwnerId =
            new OwnerGuid(new Guid("F8957212-2F12-4C8A-AB08-68B1F5718E34"));

        public static readonly decimal DefaultOwnerIdentification =
            new(7244102);

        public static readonly string DefaultOwnerName =
            new("Jim Lee");

        public static readonly string DefaultOwnerAddress =
            new("Clarence St, FL");

        //public static readonly byte[] DefaultOwnerPhoto =
        //    new byte[]();

        public static readonly DateTime DefaultOwnerBirthday =
            DateTime.Now;
        #endregion

        #region DefaultProperty
        public static readonly PropertyGuid DefaultPropertyId
            = new PropertyGuid(new Guid("62DB3D0F-A3AF-4EF0-B430-3B38A682D0F0"));

        public static readonly string DefaultPropertyName =
            new("Florida Property 01");

        public static readonly string DefaultPropertyAddress =
            new("NW 85 St, FL");

        public static readonly decimal DefaultPropertyPrice =
            new(354210);

        public static readonly decimal DefaultPropertyTax =
           new(74200);

        public static readonly string DefaultPropertyCodeInternal =
            new("FLPTY01");

        public static readonly string DefaultPropertyYear =
            new("2016");

        public static CountryStatesId DefaultPropertyCountryStatesId =
            new CountryStatesId(12);
        #endregion

        #region DefaultPropertyTrace
        public static readonly PropertyTraceGuid DefaultPropertyTraceId
            = new PropertyTraceGuid(new Guid("60B5AE7B-3D05-4719-95B2-F661EFD045D1"));

        public static readonly DateTime DefaultPropertyTraceDateSale =
            DateTime.Now;
        #endregion

        #region DefaultCountryStates
        public static readonly CountryStatesId DefaultCountryStatesId
            = new CountryStatesId(12);

        public static readonly string DefaultCountryStatesName
            = new("Florida");

        public static readonly string DefaultCountryStatesAbbr
            = new("FL");
        #endregion

    }
}
