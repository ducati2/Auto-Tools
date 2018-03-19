using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CADService.Models
{
    public class CADServiceContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public CADServiceContext() : base("name=CADServiceContext")
        {
            this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        public System.Data.Entity.DbSet<CADService.Models.CadJob> CadJobs { get; set; }

        public System.Data.Entity.DbSet<CADService.Models.RawTraceFile> RawTraceFiles { get; set; }

        public System.Data.Entity.DbSet<CADService.Models.RawPDBDir> RawPDBDirs { get; set; }

        public System.Data.Entity.DbSet<CADService.Models.RawTMFDir> RawTMFDirs { get; set; }

        public System.Data.Entity.DbSet<CADService.Models.RawSourceDir> RawSourceDirs { get; set; }

        public System.Data.Entity.DbSet<CADService.Models.CadIssue> CadIssues { get; set; }

        public System.Data.Entity.DbSet<CADService.Models.CadProduct> CadProducts { get; set; }

        public System.Data.Entity.DbSet<CADService.Models.CadProductComponent> CadProductComponents { get; set; }

        public System.Data.Entity.DbSet<CADService.Models.CadProductVer> CadProductVers { get; set; }

        public System.Data.Entity.DbSet<CADService.Models.CadJobStatus> CadJobStatus { get; set; }

        public System.Data.Entity.DbSet<CADService.Models.CDFLine> ParsedCDFTraceTemplates { get; set; }

        public System.Data.Entity.DbSet<CADService.Models.Pattern> Patterns { get; set; }

        //added by wenpingx.
        //public System.Data.Entity.DbSet<CADService.Models.JobPattern> JobPatterns { get; set; }  

        public System.Data.Entity.DbSet<CADService.Models.PatternSegment> PatternSegments { get; set; }

        public System.Data.Entity.DbSet<CADService.Models.PatternLog> PatternLogs { get; set; }

        public System.Data.Entity.DbSet<CADService.Models.CadCodeRepo> CadCodeRepos { get; set; }

        public System.Data.Entity.DbSet<CADService.Models.CadOnefixCase> CadOnefixCases { get; set; }

    }
}
