export class Report {
    Id: number;
    LCID: string;
    Status: string;
    Product: string;
    Component: string;
    Description: string;
    TracePattern: string;
    NextStep: string;
    RelevantIssues: Array<string>;
    RelevantJobs: Array<string>;
    NodeTree: string;
    StartTime: Date;
    EndTime: Date;
    ParseStartTime: Date;
    ParseEndTime: Date;
    AnalysisStartTime: Date;
    AnalysisEndTime: Date;
    StatusMsg: string;
    RootCause: string;
}