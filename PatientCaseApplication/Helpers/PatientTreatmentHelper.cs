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
                    Registry = registry
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
                var visitissues = Db.SQL<VisitIssue>($"SELECT v FROM {typeof(VisitIssue).Name} v " +
                    $"WHERE v.{nameof(VisitIssue.Issue)} = ? AND v.{nameof(VisitIssue.Visit)} = ?", issue, visit);

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
                registry.Clinic = null;
                registry.Visits?.ForEach(v => DeleteVisit(v));
                registry.Deleted = true;
            });
        }

        public static void DeleteVisit(PatientVisit visit)
        {
            Db.Transact(() =>
            {
                visit.Registry = null; 
                visit.Patient = null;
                visit.Issues?.ForEach(i => i.Delete());
                visit.Delete();
            });
        }

        public static IEnumerable<PatientVisit> SelectAllVisitsOf(Patient patient)
        {
            return DbHelper.SelectFrom<PatientVisit>(nameof(PatientVisit.Patient), patient);
        }
    }
}
