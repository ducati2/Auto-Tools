using System;

namespace CADService.Models
{
    [System.ComponentModel.DataAnnotations.Schema.Table("LogTable")]
    public class PatternLog
    {
        public string ID { get; set; }
        public string SegmentID { get; set; }
        public int IndexInSegment { get; set; }
        public DateTime? Time { get; set; }
        public string Source { get; set; }
        public string FunctionName { get; set; }
        public Nullable<int> LineNum { get; set; }
        public string Module { get; set; }
        public Nullable<int> SessionID { get; set; }
        public Nullable<int> ProcessID { get; set; }
        public Nullable<int> ThreadID { get; set; }
        public string RelationWithPrevious { get; set; }
        public string Text { get; set; }
        public int LineNumInTraceFile { get; set; }
        public bool IsForDebug { get; set; }
        public bool IsBreakPoint { get; set; }
    }
}
