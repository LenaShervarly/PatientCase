using Starcounter;

namespace PatientCaseApplication
{
    [Database]
    abstract public class Person
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public int Age { get; set; }

        public Address Address { get; set; }

        public string Email { get; set; }

        public string PhoneNo { get; set; }

        public ulong PersonalNo { get; set; }
    }
}

