using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CADService.Models
{
    public class CadOnefixCase
    {
        public int ID { get; set; }
        public string CaseID { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Resolution { get; set; }
        public string LogFiles { get; set; }
        public string ProductDivision { get; set; }
        public string ProductLine { get; set; }
        public string ServiceProduct { get; set; }
        public string ProductVersion { get; set; }
        public string Component { get; set; }
        public string Status { get; set; }
        public string ResolutionType { get; set; }
        public string InitSupportCaseNumer { get; set; }
        public string EscEngineer { get; set; }
        public string DevEngineer { get; set; }
        public string Environment { get; set; }
        public string ReproSteps { get; set; }
        public string WorkAround { get; set; }
        public string UniqueSymptoms { get; set; }
        public string Severity { get; set; }
        public DateTimeOffset? LastUpdateTime { get; set; }
        public string RealLogFiles { get; set; }
    }
}