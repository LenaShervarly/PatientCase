using Starcounter;
using System.Collections.Generic;
using PatientCaseApplication.Helpers;

namespace PatientCaseApplication
{
    [Database]
    public class Clinic
    {
        public string Name { get; set; }

        public IEnumerable<Medic> Personal => Helpers.DbHelper.SelectFrom<Medic>(nameof(Medic.PlaceOfWork), this);
        
        public IEnumerable<PatientRegistry> PatientRegister => Helpers.DbHelper.SelectFrom<PatientRegistry>(nameof(PatientRegistry.Clinic), this);

        public Address Address { get; set; }

        public ClinicsChain Chain { get; set; }
    }
}
