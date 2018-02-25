using Starcounter;

namespace PatientCaseApplication
{
    [Database]
    public class Issue
    {
        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}
