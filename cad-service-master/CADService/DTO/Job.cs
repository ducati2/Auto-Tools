using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CADService.DTO
{
    public class Job
    {
        public string LCID { get; set; }
        public short ProductID { get; set; }
        public short VersionID { get; set; }
        public short ComponentID { get; set; }
        public string Description { get; set; }
        public string Trace { get; set; }
        public string TMF { get; set; }
        public string PDB { get; set; }
        public string Source { get; set; }
        public string PatternID { get; set; }   //added by wenpingx.
    }
}