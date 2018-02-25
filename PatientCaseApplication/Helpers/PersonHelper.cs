using Starcounter;
using System.Collections.Generic;

namespace PatientCaseApplication.Helpers
{
    public static class PersonHelper
    {
        public static Patient CreatePatientBase(ulong personalNumber)
        {
            Patient newPatient = DbHelper.GetIfExist<Patient>(nameof(Patient.PersonalNo), personalNumber);
            if (newPatient != null)
                return newPatient;

            Db.Transact(() =>
            {
                newPatient = new Patient
                {
                    PersonalNo = personalNumber
                };
            });
            return newPatient;
        }

        public static Medic CreateNewMedic(ulong personalNumber, Occupation occupation)
        {
            Medic medic = DbHelper.GetIfExist<Medic>(nameof(Medic.PersonalNo), personalNumber);
            Db.Transact(() =>
            {
                medic = new Medic
                {
                    PersonalNo = personalNumber,
                    CurrentOccupation = occupation
                };
            });
            return medic;
        }

        public static void UpdatePersonalInfo(ulong personalNumber, string firstName, string lastName, string gender,
           int age, Address address, string email, string phoneNumber)
        {
            var person = DbHelper.GetIfExist<Person>(nameof(Person.PersonalNo), personalNumber);
            if (person == null)
                return;
            else
            {
                Db.Transact(() =>
                {
                    person.FirstName = firstName;
                    person.LastName = lastName;
                    person.Gender = gender;
                    person.Age = age;
                    person.Address = address;
                    person.Email = email;
                    person.PhoneNo = phoneNumber;
                });
            }
        }

        public static void UpdateMainDiagnosis(Patient patient, string diagnosis)
        {
            Db.Transact(() =>
            {
                patient.MainDiagnosis = diagnosis;
            });
        }

        public static void UpdateSecondaryDiagnosis(Patient patient, string diagnosis)
        {
            Db.Transact(() =>
            {
                patient.SecondaryDiagnosis = diagnosis;
            });
        }

        public static void UpdateAdditionalInfo(Patient patient, string info)
        {
            Db.Transact(() =>
            {
                patient.AdditionalInformation = info;
            });
        }

        public static void UpdateOccupation(Medic medic, Occupation newOccupation)
        {
            Db.Transact(() =>
            {
                medic.CurrentOccupation = newOccupation;
            });
        }

        public static void UpdatePlaceOfWork(Medic medic, Clinic newPlaceOfWork)
        {
            Db.Transact(() =>
            {
                medic.PlaceOfWork = newPlaceOfWork;
            });
        }

        public static void UpdateEmail(Person person, string newEmail)
        {
            Db.Transact(() =>
            {
                person.Email = newEmail;
            });
        }

        public static void UpdatePhoneNumber(Person person, string newPhoneNumber)
        {
            Db.Transact(() =>
            {
                person.PhoneNo = newPhoneNumber;
            });
        }

        public static void DeletePerson(Person person)
        {
            Db.Transact(() =>
            {
                person.Delete();
            });
        }

        public static IEnumerable<Patient> SelectAllPatients()
        {
            return DbHelper.SelectAll<Patient>();
        }

        public static IEnumerable<Medic> SelectAllMedics()
        {
            return DbHelper.SelectAll<Medic>();
        }
    }
}
