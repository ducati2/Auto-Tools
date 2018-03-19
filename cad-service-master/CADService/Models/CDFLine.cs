using System;
using System.Collections.Generic;

namespace CADService.Models
{
    public class CDFLine
    {
        public int ID { get; set; }
        public int CPU { get; set; }
        public System.DateTime LogTime { get; set; }
        public Nullable<int> ThreadID { get; set; }
        public string ThreadName { get; set; }
        public Nullable<int> ProcessID { get; set; }
        public string ProcessName { get; set; }
        public Nullable<int> SessionID { get; set; }
        public string ModuleName { get; set; }
        public string Src { get; set; }
        public Nullable<int> LineNum { get; set; }
        public string FunctionName { get; set; }
        public Nullable<int> LevelID { get; set; }
        public string ClassName { get; set; }
        public string Message { get; set; }
        public string Comments { get; set; }
        public int RawTraceID { get; set; }
        public Nullable<int> NodeID { get; set; }
        public Nullable<short> CdfModuleID { get; set; }
        public Nullable<bool> IsIssuePattern { get; set; }
        public int JobID { get; set; }
    }
}
