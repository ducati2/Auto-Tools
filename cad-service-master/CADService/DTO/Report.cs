using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CADService.DTO
{

    public class Report
    {
        public int Id { get; set; }
        public string LCID { get; set; }
        public string Status { get; set; }
        public string Product { get; set; }
        public string Component { get; set; }
        public string Description { get; set; }
        public string TracePattern { get; set; }
        public string NextStep { get; set; }
        public string[] RelevantIssues { get; set; }
        public string[] RelevantJobs { get; set; }
        public PatternNode NodeTree { get; set; }
        public DateTimeOffset? StartTime { get; set; }
        public DateTimeOffset? EndTime { get; set; }
        public DateTimeOffset? ParseStartTime { get; set; }
        public DateTimeOffset? ParseEndTime { get; set; }
        public DateTimeOffset? AnalysisStartTime { get; set; }
        public DateTimeOffset? AnalysisEndTime { get; set; }
        public string StatusMsg { get; set; }
        public string RootCause { get; set; }
    }
}