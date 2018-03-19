export class CDFLine {
    ID: number;
    CPU: number;
    LogTime: Date;
    ThreadID: number;
    ThreadName: string;
    ProcessID: number;
    ProcessName: string;
    SessionID: number;
    ModuleName: string;
    Src: string;
    LineNum: number;
    FunctionName: string;
    LevelID: number;
    ClassName: string;
    Message: string;
    Comments: string;
    RawTraceID: number;
    NodeID: number;
    CdfModuleID: number;
    IsIssuePattern: boolean;
    JobID: number;
}