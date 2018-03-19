using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CADService.DTO
{
    public class JobDetail
    {
        public int ID { get; set; }
        public string LCID { get; set; }
        public short ProductID { get; set; }
        public short VersionID { get; set; }
        public short ComponentID { get; set; }
        public string Description { get; set; }
        public string Trace { get; set; }
        public string TMF { get; set; }
        public string PDB { get; set; }
        public string Source { get; set; }
        public short Status { get; set; }
        public DateTimeOffset? StartTime { get; set; }
        public DateTimeOffset? EndTime { get; set; }
    }
}