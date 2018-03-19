using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CADService.Models
{
    [Table("PatternTable")]
    public class Pattern
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string ProductName { get; set; }
        public string Version { get; set; }
        public string HotfixLevel { get; set; }
        public Nullable<int> IsIssued { get; set; }
    }
}
