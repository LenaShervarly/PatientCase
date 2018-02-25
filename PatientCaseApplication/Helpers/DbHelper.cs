using Starcounter;
using System.Collections.Generic;
using System.Linq;

namespace PatientCaseApplication.Helpers
{
    internal class DbHelper
    {
        public static IEnumerable<T> SelectFrom<T>(string column, object clauseValue)
        {
            return Db.SQL<T>($"SELECT x FROM {typeof(T).Name} x WHERE x.{column} = ?", clauseValue);
        }

        public static IEnumerable<T> SelectAll<T>()
        {
            return Db.SQL<T>($"SELECT x FROM {typeof(T).Name} x");
        }

        public static IEnumerable<T> SelectTop100<T>(string orderBycolumn) =>
            Db.SQL<T>($"SELECT x FROM {typeof(T).Name} x ORDER BY x.{orderBycolumn} FETCH FIRST 100");

        public static T GetIfExist<T>(string column, object matchingValue)
        {
            return Db.SQL<T>($"SELECT x FROM {typeof(T).Name} x WHERE {column} = ?", matchingValue).FirstOrDefault();
        }
    }
}
