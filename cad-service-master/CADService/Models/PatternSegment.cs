using System;

namespace CADService.Models
{
    [System.ComponentModel.DataAnnotations.Schema.Table("SegmentTable")]
    public class PatternSegment
    {
        public string ID { get; set; }
        public string ParentID { get; set; }
        public string Name { get; set; }
        public Nullable<int> IndexInPattern { get; set; }
        public string Collected { get; set; }
    }
}
