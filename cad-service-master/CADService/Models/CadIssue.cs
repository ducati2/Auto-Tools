using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CADService.Models
{
    public class CadIssue
    {
        public int ID { get; set; }
        public string LCID { get; set; }
        public Nullable<int> JobID { get; set; }
        public string DocInfo { get; set; }
        public string Name { get; set; }
        public string RootCause { get; set; }
        public string Resolution { get; set; }
        public string PatternID { get; set; }
        public bool IssueProcessed { get; set; }
        public string IntermediateResult { get; set; }
        public string SimilarLCIDs { get; set; }
    }
}