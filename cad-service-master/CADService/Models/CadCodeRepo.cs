using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CADService.Models
{
    [Table("CadCodeRepo")]
    public class CadCodeRepo
    {
        public int ID { get; set; }
        public Nullable<short> VersionID { get; set; }
        public Nullable<short> ComponentID { get; set; }
        public string CodeUrl { get; set; }
        public string TraceModuleName { get; set; }
        public string TraceSrcName { get; set; }
    }
}
