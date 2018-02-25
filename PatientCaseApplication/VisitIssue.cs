using Starcounter;

namespace PatientCaseApplication
{
    [Database]
    public class VisitIssue
    {
        public PatientVisit Visit { get; set; }

        public Issue Issue { get; set; }
    }
}
