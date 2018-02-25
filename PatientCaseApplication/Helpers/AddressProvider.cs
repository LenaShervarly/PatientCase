using Starcounter;
using System.Collections.Generic;
using System.Linq;

namespace PatientCaseApplication.Helpers
{
    public static class AddressProvider
    {
        public static Address CreateAddress(string country, string region, string town, string streetName, int streetNumber, string appartment, int postalCode)
        {
            Address address = null;
            Db.Transact(() =>
            {
                address = new Address
                {
                    Country = country,
                    Region = region,
                    Town = town,
                    StreetName = streetName,
                    StreetNumber = streetNumber,
                    Appartment = appartment,
                    PostalCode = postalCode
                };
            });
            return address;
        }

        public static IEnumerable<Address> SelectAll()
        {
            return DbHelper.SelectAll<Address>();
        }

        public static IEnumerable<Address> SelectAddressesOf(ClinicsChain chain)
        {
            return ClinicHelper.SelectAllClinicsOfClinicChain(chain).Select(clinic => clinic.Address).ToList();
        }

        public static void Delete(Address address)
        {
            Db.Transact(() =>
            {
                address.Deleted = true;
            });
        }

        public static void UpdateAddress(Address address, string country, string region, string town, string streetName, int streetNumber, string appartment, int postalCode)
        {
            Db.Transact(() =>
            {
                address.Country = country;
                address.Region = region;
                address.Town = town;
                address.StreetName = streetName;
                address.StreetNumber = streetNumber;
                address.Appartment = appartment;
                address.PostalCode = postalCode;
            });
        }
    }
}
