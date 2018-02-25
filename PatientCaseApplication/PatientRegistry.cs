using Starcounter;
using System.Collections.Generic;
using PatientCaseApplication.Helpers;

namespace PatientCaseApplication
{
    [Database]
    public class PatientRegistry
    {
        public Clinic Clinic { get; set; }

        public IEnumerable<PatientVisit> Visits => Helpers.DbHelper.SelectFrom<PatientVisit>(nameof(PatientVisit.Registry), this);

        public bool Deleted { get; set; }
    }
}
