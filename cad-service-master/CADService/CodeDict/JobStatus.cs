using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CADService.CodeDict
{
    public enum JobStatus
    {
        Draft = 1,
        Submitted = 2,
        Preparing = 3,
        ReadyForParsing = 4,
        Parsing = 5,
        ReadyForAnalyzing = 6,
        Analyzing = 7,
        Finished = 8,
        Failed = 99
    }
}