using Starcounter;
using System.Collections.Generic;

namespace PatientCaseApplication.Helpers
{
    public static class OccupationProvider
    {
        public static Occupation CreateOccupation(OccupationType type, string description)
        {
            Occupation occupation = null;
            Db.Transact(() =>
            {
                occupation = new Occupation
                {
                    Type = type,
                    Description = description
                };
            });
            return occupation;
        }

        public static IEnumerable<Occupation> SelectAll()
        {
            return DbHelper.SelectAll<Occupation>();
        }

        public static IEnumerable<Occupation> SelectOccupationsOf(OccupationType type)
        {
            return DbHelper.SelectFrom<Occupation>(nameof(Occupation.Type), type);
        }

        public static void Delete(Occupation occupation)
        {
            Db.Transact(() =>
            {
                occupation.Delete();
            });
        }

        public static void UpdateDescription(Occupation occupation, string newDescription)
        {
            Db.Transact(() =>
            {
                occupation.Description = newDescription;
            });
        }

        public static void UpdateType(Occupation occupation, OccupationType newType)
        {
            Db.Transact(() =>
            {
                occupation.Type = newType;
            });
        }
    }
}
