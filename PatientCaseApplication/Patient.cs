using System.Collections.Generic;
using PatientCaseApplication.Helpers;

namespace PatientCaseApplication
{

    public class Patient : Person
    {
        public string MainDiagnosis { get; set; }

        public string SecondaryDiagnosis { get; set; }

        public string AdditionalInformation { get; set; }

        public IEnumerable<PatientVisit> VisitsToClinic => DbHelper.SelectFrom<PatientVisit>(nameof(PatientVisit.Patient), this);
    }
}

