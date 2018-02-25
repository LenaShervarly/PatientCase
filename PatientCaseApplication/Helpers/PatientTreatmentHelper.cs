using System;
using System.Collections.Generic;
using System.Linq;
using Starcounter;

namespace PatientCaseApplication.Helpers
{
    public static class PatientTreatmentHelper
    {
        public static PatientRegistry CreateNewPatietnRegistry(Clinic clinic)
        {
            PatientRegistry registry = DbHelper.GetIfExist<PatientRegistry>(nameof(PatientRegistry.Clinic), clinic);
            if (registry != null)
                return registry;
            Db.Transact(() =>
            {
                registry = new PatientRegistry
                {
                    Clinic = clinic
                };
            });
            return registry;
        }

        public static PatientVisit CreateNewPatientVisit(ulong personalNumber, PatientRegistry registry)
        {
            var patient = DbHelper.GetIfExist<Patient>(nameof(Patient.PersonalNo), personalNumber);
            PatientVisit patientVisit = null;

            if (patient == null)
                patient = PersonHelper.CreatePatientBase(personalNumber);

            Db.Transact(() =>
            {
                patientVisit =  new PatientVisit
                {
                    Created = DateTime.Now,
                    Patient = patient,
                };
            });

            return patientVisit;
        }

        public static void AddIssue(PatientVisit visit, Issue issue)
        {
            if (issue == null || visit == null)
                return;

            Db.Transact(() =>
            {
                new VisitIssue
                {
                    Visit = visit,
                    Issue = issue
                };
            });
        }

        public static void DeleteIssueFromPatientVisit(Issue issue, PatientVisit visit)
        {
            Db.Transact(() =>
            {
                var visitissues = Db.SQL<VisitIssue>($"SELECT vi FROM {typeof(VisitIssue).Name} " +
                    $"WHERE {nameof(VisitIssue.Issue)} = ? AND {nameof(VisitIssue.Visit)} = ?", issue, visit);

                foreach(VisitIssue item in visitissues)
                {
                    item.Delete();
                }
            });
        }

        public static void DeletePatientRegistry(PatientRegistry registry)
        {
            if (registry == null)
                return;

            Db.Transact(() =>
            {
                var currentClinic = registry.Clinic;
                currentClinic.PatientRegister.ToList().Remove(registry);

                registry.Visits.ForEach(v => DeleteVisit(v));
            });
        }

        public static void DeleteVisit(PatientVisit visit)
        {
            Db.Transact(() =>
            {
                var registry = visit.Registry;
                registry.Visits.ToList().Remove(visit);

                var patient = visit.Patient;
                patient.VisitsToClinic.ToList().Remove(visit);
            });
        }

        public static IEnumerable<PatientVisit> SelectAllVisitsOf(Patient patient)
        {
            return DbHelper.SelectFrom<PatientVisit>(nameof(PatientVisit.Patient), patient);
        }
    }
}
