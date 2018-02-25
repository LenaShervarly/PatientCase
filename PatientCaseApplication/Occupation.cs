using Starcounter;

namespace PatientCaseApplication
{
    [Database]
    public class Occupation
    {
        public OccupationType Type { get; set; }

        public string Description { get; set; }
    }
}