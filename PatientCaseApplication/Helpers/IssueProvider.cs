using Starcounter;
using System.Collections.Generic;
using System.Linq;

namespace PatientCaseApplication.Helpers
{
    public static class IssueProvider
    {
        public static Issue CreateIssue(string name, decimal price)
        {
            Issue issue = null;
            Db.Transact(() =>
            {
                issue = new Issue
                {
                    Name = name,
                    Price = price
                };
            });
            return issue;
        }

        public static IEnumerable<Issue> SelectAll()
        {
            return DbHelper.SelectAll<Issue>();
        }

        public static IEnumerable<Issue> SelectIssuesOf(string name)
        {
            return DbHelper.SelectFrom<Issue>(nameof(Issue.Name), name);
        }

        public static IEnumerable<Issue> SelectAllIssuesOf(PatientVisit visit)
        {
            return DbHelper.SelectFrom<VisitIssue>(nameof(VisitIssue.Visit), visit).Select(pv => pv.Issue).ToList();
        }

        public static IEnumerable<Issue> SelectTop100MostExpensive()
        {
            return DbHelper.SelectTop100<Issue>(nameof(Issue.Price));
        }

        public static void Delete(Issue issue)
        {
            Db.Transact(() =>
            {
                issue.Delete();
            });
        }

        public static void UpdatePrice(Issue issue, decimal newPrice)
        {
            Db.Transact(() =>
            {
                issue.Price = newPrice;
            });
        }

        public static void UpdateName(Issue issue, string newName)
        {
            Db.Transact(() =>
            {
                issue.Name = newName;
            });
        }
    }
}
