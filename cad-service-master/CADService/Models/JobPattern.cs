using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CADService.Models
{
    [System.ComponentModel.DataAnnotations.Schema.Table("JobPattern")]
    public class JobPattern
    {
        public int JobId { get; set; }
        public string PatternId { get; set; }

        public string ToSQLString()
        {
            string sqlContent = "insert into JobPattern(JobId, PatternId) values(" + JobId + ", '" + PatternId + "')";
            return sqlContent;
        }
    }
}