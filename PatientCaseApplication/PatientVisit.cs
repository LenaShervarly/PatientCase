using Starcounter;
using System;
using System.Collections.Generic;
using System.Linq;


namespace PatientCaseApplication
{
    [Database]
    public class PatientVisit
    {
        public PatientRegistry Registry { get; set; }

        public DateTime Created { get; set; }

        public Patient Patient { get; set; }

        public IEnumerable<VisitIssue> Issues => Helpers.DbHelper.SelectFrom<VisitIssue>(nameof(VisitIssue.Visit), this);
      
        //[Transient]
        //public decimal TotalVisitCost => Issues.Sum(i => i.Issue.Price);
    }
}
