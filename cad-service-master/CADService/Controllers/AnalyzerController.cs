using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using CADService.Models;
using CADService.DTO;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System;
using System.Diagnostics;

namespace CADService.Controllers
{
    public class AnalyzerController : ApiController
    {
        private CADServiceContext db = new CADServiceContext();

        // GET: api/Analyzer/5
        [ResponseType(typeof(CDFLine))]
        [Route("api/analyzer/{jobId:int}")]
        [HttpPost]
        public IHttpActionResult GetParsedCDFTraceTemplate(int jobId, [FromBody] TraceQuery param)
        {
            string condition = string.Empty;
            if (param.Condition != null)
            {
                IList<string> conditionList = new List<string>();
                
                if (!string.IsNullOrEmpty(param.Condition.Module))
                {
                    conditionList.Add( " ModuleName = '" + param.Condition.Module + "'");
                }
                if (!string.IsNullOrEmpty(param.Condition.Src))
                {
                    conditionList.Add(" Src = '" + param.Condition.Src + "'");
                }
                if (!string.IsNullOrEmpty(param.Condition.Function))
                {
                    conditionList.Add(" FunctionName = '" + param.Condition.Function + "'");
                }
                if (!string.IsNullOrEmpty(param.Condition.Message))
                {
                    conditionList.Add(" Message LIKE '%" + param.Condition.Message + "%'");
                }
                if (conditionList.Count > 0) {
                    condition = string.Format("WHERE {0}", string.Join(" AND ", conditionList));
                }
                
            }
            string sql =
                @"SELECT [ID],[CPU],[LogTime],[ThreadID],[ThreadName],[ProcessID],
                    [ProcessName],[SessionID],[ModuleName] ,[Src],[LineNum],[FunctionName],
                    [LevelID],[ClassName],[Message],[Comments],[RawTraceID],[NodeID],
                    [CdfModuleID],[IsIssuePattern],[JobID]
                FROM (SELECT ROW_NUMBER() OVER ( ORDER BY LogTime ) AS RowNum, * FROM ParsedCDFTrace_{0} {3})
                AS CDFTrace
                WHERE RowNum >= {1}
                    AND RowNum < {2}
                ORDER BY RowNum";
            List<CDFLine> list = db.Database.SqlQuery<CDFLine>(string.Format(sql, jobId, param.Pagination.startIndex, param.Pagination.endIndex, condition)).ToList<CDFLine>();

            return Ok(list);
        }

        [Route("api/analyzer/source")]
        [HttpPost]
        public IHttpActionResult GetSourceByName([FromBody] SourceParam param)
        {
            var codedRepo = db.CadCodeRepos.FirstOrDefault(x => x.TraceModuleName == param.Module && x.TraceSrcName == param.Src);
            if (codedRepo == null)
            {
                return StatusCode(HttpStatusCode.NotFound);
            }
            else
            {
                string codeUrl = string.Format(@"https://wwwcode.eng.citrite.net/source/raw/{0}", codedRepo.CodeUrl);
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(codeUrl);
                httpWebRequest.ContentType = "text/javascript;charset=utf-8";
                httpWebRequest.Method = "GET";
                httpWebRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                // TODO
                httpWebRequest.Credentials = new NetworkCredential("domain account", "domain password", "domain name");
                string responseText = null;
                try
                {
                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream(), Encoding.UTF8))
                    {
                        responseText = streamReader.ReadToEnd();
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.ToString());
                    return StatusCode(HttpStatusCode.NotFound);
                }

                return Ok(HttpUtility.HtmlEncode(responseText));
            }

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}