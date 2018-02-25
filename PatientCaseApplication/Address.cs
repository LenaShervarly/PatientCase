using Starcounter;

namespace PatientCaseApplication
{
    [Database]
    public class Address
    {
        public string Country { get; set; }

        public string Region { get; set; }

        public string Town { get; set; }

        public string StreetName { get; set; }

        public int StreetNumber { get; set; }

        public string Appartment { get; set; }

        public int PostalCode { get; set; }

        public bool Deleted { get; set; }
    }
}

