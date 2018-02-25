using Starcounter;
using System.Collections.Generic;
using System.Linq;

namespace PatientCaseApplication.Helpers
{
    public static class ClinicHelper
    {
        public static ClinicsChain CreateClinicChain(string name, string unitingСharacteristic) {
            ClinicsChain chain = null;
            Db.Transact(() =>
            {
                chain = new ClinicsChain
                {
                    Name = name,
                    UnitingCharacteristic = unitingСharacteristic
                };
            });
            return chain;
        }

        public static Clinic CreateClinic(string name, Address address)
        {
            Clinic clinic = null;
            Db.Transact(() =>
            {
                clinic = new Clinic
                {
                    Name = name,
                    Address = address
                };
            });
            return clinic;
        }

        public static void AddClinicToClinicChain(Clinic clinic, ClinicsChain inClinicChain)
        {
            Db.Transact(() =>
            {
                clinic.Chain = inClinicChain;
            });
        }

        public static void Hire(Medic newStuffMember, Clinic clinic)
        {
            Db.Transact(() =>
            {
                newStuffMember.PlaceOfWork = clinic;
            });
        }

        public static void Fire(Medic medicToFire)
        {
            Db.Transact(() =>
            {
                medicToFire.PlaceOfWork = null;
            });
        }

        public static void MoveToNewLocation(Address address, Clinic clinic)
        {
            Db.Transact(() =>
            {
                clinic.Address = address;
            });
        }

        public static void DeleteClinicChain(ClinicsChain chain)
        {
            Db.Transact(() =>
            {
                chain.Clinics?.ForEach(c => c.Chain = null);
                chain.Delete();
            });
        }

        public static void DeleteClinic(Clinic clinic)
        {
            Db.Transact(() =>
            {
                clinic.Personal?.ForEach(p => p.PlaceOfWork = null);

                clinic.PatientRegister?.ForEach(pr => PatientTreatmentHelper.DeletePatientRegistry(pr));

                AddressProvider.Delete(clinic.Address);

                var chain = SelectClinicChainsByUnitingCharacteristic(clinic.Chain?.UnitingCharacteristic).FirstOrDefault();

                if (chain?.Clinics?.Count() <= 1)
                    DeleteClinicChain(chain);

                clinic.Delete();
            });
        }

        public static IEnumerable<ClinicsChain> SelectAllClinicChains()
        {
            return DbHelper.SelectAll<ClinicsChain>();
        }

        public static IEnumerable<ClinicsChain> SelectClinicChainsByUnitingCharacteristic(string unitingСharacteristic)
        {
            return DbHelper.SelectFrom<ClinicsChain>(nameof(ClinicsChain.UnitingCharacteristic), unitingСharacteristic);
        }

        public static IEnumerable<Clinic> SelectAllClinicsOfClinicChain(ClinicsChain clinicsChain)
        {
            return DbHelper.SelectFrom<Clinic>(nameof(Clinic.Chain), clinicsChain); ;
        }

        public static IEnumerable<Clinic> SelectAllClinics()
        {
            return DbHelper.SelectAll<Clinic>();
        }

        public static IEnumerable<Clinic> SelectTOP100Clinics()
        {
            return DbHelper.SelectTop100<Clinic>(nameof(Clinic.Name));
        }

        public static IEnumerable<Clinic> SelectAllClinicsOfDistrict(string region)
        {
            Address searchedAddress = Db.SQL<Address>($"SELECT a FROM {typeof(Address).Name} a " +
                $"WHERE a.{nameof(Address.Region)} = ? AND a.{nameof(Address.Deleted)} = ?", region, false).FirstOrDefault();
            if (searchedAddress == null)
                return Enumerable.Empty<Clinic>();

            return Db.SQL<Clinic>($"SELECT * FROM {typeof(Clinic).Name} c WHERE c.{nameof(Clinic.Address)} = ?", searchedAddress);
        }

        public static IEnumerable<PatientRegistry> SelectAllPatientRegistryIn(Clinic clinic)
        {
            return DbHelper.SelectFrom<PatientRegistry>(nameof(PatientRegistry.Clinic), clinic);
        }

        public static IEnumerable<PatientVisit> SelectAllPatientVisitsIn(Clinic clinic)
        {
            var registry = SelectAllPatientRegistryIn(clinic);
            if (registry == null)
                return Enumerable.Empty<PatientVisit>();

            return DbHelper.SelectFrom<PatientVisit>(nameof(PatientVisit.Registry), registry);
        }

        public static IEnumerable<Patient> SelectAllPatientsIn(Clinic clinic)
        {
            var visitsToSelectedClinic = SelectAllPatientVisitsIn(clinic);
            if (visitsToSelectedClinic == null)
                return Enumerable.Empty<Patient>();

            return DbHelper.SelectFrom<Patient>(nameof(Patient.VisitsToClinic), visitsToSelectedClinic);
        }
    }
}
