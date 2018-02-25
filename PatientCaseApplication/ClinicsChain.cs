using Starcounter;
using System.Collections.Generic;
using PatientCaseApplication.Helpers;

namespace PatientCaseApplication
{
    [Database]
    public class ClinicsChain
    {
        public string Name { get; set; }

        public string UnitingCharacteristic { get; set; }

        public IEnumerable<Clinic> Clinics => Helpers.DbHelper.SelectFrom<Clinic>(nameof(Clinic.Chain), this);
    }
}
