using Starcounter;

namespace PatientCaseApplication
{
    [Database]
    public class Department
    {
        public string Name { get; set; }

        public Clinic Clinic { get; set; }

        public Medic Medic { get; set; }
    }
}