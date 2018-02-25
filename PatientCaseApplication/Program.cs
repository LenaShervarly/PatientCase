using PatientCaseApplication.Helpers;
using Starcounter;
using System;

namespace PatientCaseApplication
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Start");
            var doctriniansClinicChain = ClinicHelper.CreateClinicChain("Doctrinians", "Chain of Clinics united by common idea of improving medical system");
            var stockholmiansClinicChain = ClinicHelper.CreateClinicChain("Stockholmians", "Classical medical units in Stocklolmian region");

            var karolinskaAddress = AddressProvider.CreateAddress("Sweden", "Stockholm LL", "Stockholm", "Solnavägen", 1, null, 17177);
            var karolinskaClinic = ClinicHelper.CreateClinic("Karolinska", karolinskaAddress);

            var capioAddress = AddressProvider.CreateAddress("Sweden", "Stockholm LL", "Stockholm", "Ringvägen", 113, null, 11860);
            var capioClinic = ClinicHelper.CreateClinic("Capio", capioAddress);

            ClinicHelper.AddClinicToClinicChain(karolinskaClinic, stockholmiansClinicChain);
            ClinicHelper.AddClinicToClinicChain(karolinskaClinic, stockholmiansClinicChain);

            var neurologistOccupation = OcupationProvider.CreateOccupation(OccupationType.Doctor, "Neurologist");
            var pediatrOccupation = OcupationProvider.CreateOccupation(OccupationType.Doctor, "Pediatr");
            var pediatricNurseOccupation = OcupationProvider.CreateOccupation(OccupationType.Nurse, "Pediatric nurse");
            var adminOccupation = OcupationProvider.CreateOccupation(OccupationType.Admin, "Administrator");

            var neurologist = PersonHelper.CreateNewMedic(196002011364, neurologistOccupation);
            var pediatr = PersonHelper.CreateNewMedic(197001011089, pediatrOccupation);
            var pediatricNurse = PersonHelper.CreateNewMedic(199002284501, pediatricNurseOccupation);
            var admin = PersonHelper.CreateNewMedic(198503122466, adminOccupation);

            ClinicHelper.Hire(neurologist, karolinskaClinic);
            ClinicHelper.Hire(admin, karolinskaClinic);
            ClinicHelper.Hire(pediatr, capioClinic);
            ClinicHelper.Hire(pediatricNurse, capioClinic);

            var patientRegisterKarolinska = PatientTreatmentHelper.CreateNewPatietnRegistry(karolinskaClinic);
            var patientVisitKarolinska = PatientTreatmentHelper.CreateNewPatientVisit(199503280549, patientRegisterKarolinska);

            var patientRegisterCapio = PatientTreatmentHelper.CreateNewPatietnRegistry(karolinskaClinic);
            var patientVisitCapio = PatientTreatmentHelper.CreateNewPatientVisit(198511280549, patientRegisterCapio);

            var bloodTest = IssueProvider.CreateIssue("Blood test", 50.02m);
            var ultrasound = IssueProvider.CreateIssue("Ultrasound", 150.00m);
            var ekg = IssueProvider.CreateIssue("Electrocardiogram", 149.99m);
            var mriScan = IssueProvider.CreateIssue("Magnetic resonance imaging scan", 2000.00m);

            PatientTreatmentHelper.AddIssue(patientVisitCapio, bloodTest);
            PatientTreatmentHelper.AddIssue(patientVisitCapio, ultrasound);
            PatientTreatmentHelper.AddIssue(patientVisitKarolinska, ekg);
            PatientTreatmentHelper.AddIssue(patientVisitKarolinska, mriScan);
        }
    }
}