using CADService.DTO;
using CADService.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace CADService.Controllers
{
    [RoutePrefix("api/report")]
    public class ReportController : ApiController
    {
        private CADServiceContext db = new CADServiceContext();

        private List<PatternNode> breakingNodeList = new List<PatternNode>();

        [ResponseType(typeof(Report))]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetReport(int id)
        {
            breakingNodeList.Clear();

            var cadJob = await db.CadJobs.FindAsync(id);

            var productName = db.CadProducts.Find(cadJob.ProductID).ProductName;
            var productVersion = db.CadProductVers.Find(cadJob.VersionID).Name;
            var productComponent = db.CadProductComponents.Find(cadJob.ComponentID).Name;
            var status = cadJob.StatusID.ToString();

            Report report = new Report()
            {
                Id = cadJob.ID,
                LCID = cadJob.LCID.Trim(),
                Status = status,
                StartTime = cadJob.CreatedTime,
                EndTime = cadJob.AnalyzeEndTime,
                Product = productName + " " + productVersion,
                Component = productComponent,
                Description = cadJob.Description,
                TracePattern = "",
                NextStep = "",
                RelevantIssues = new string[] { },
                RelevantJobs = new string[] { },
                NodeTree = null,
                ParseStartTime = cadJob.ParseStartTime,
                ParseEndTime = cadJob.ParseEndTime,
                AnalysisStartTime = cadJob.AnalyzeStartTime,
                AnalysisEndTime = cadJob.AnalyzeEndTime,
                StatusMsg = cadJob.StatusMsg
            };

            var cadIssue = db.CadIssues.FirstOrDefault(x => x.JobID == id);
            if (null != cadIssue)
            {
                if (!string.IsNullOrEmpty(cadIssue.SimilarLCIDs))
                {
                    report.RelevantIssues = cadIssue.SimilarLCIDs.Split(',');
                }
                report.NodeTree = getTreeNode(cadIssue.PatternID);

                MarkBranch(breakingNodeList);

                report.RootCause = cadIssue.RootCause;
            }
            return Ok(report);
        }

        private IList<PatternNode> getChildNodes(string parentID, PatternNode parentNode)
        {
            IList<PatternNode> children = null;

            if (!string.IsNullOrEmpty(parentID))
            {
                var segments = db.PatternSegments.Where(x => x.ParentID == parentID).OrderBy(x => x.IndexInPattern);
                if (segments.Any())
                {
                    children = new List<PatternNode>();
                    foreach (PatternSegment segment in segments)
                    {
                        var patternNode = new PatternNode()
                        {
                            parent = parentNode,
                            name = segment.Name
                        };
                        patternNode.children = getChildNodes(segment.ID, patternNode);

                        children.Add(patternNode);
                    }
                }
                else
                {
                    // if there is no child node for a pattern node, we should query log records from LogTable
                    var logRecords = db.PatternLogs.Where(x => x.SegmentID == parentID).OrderBy(x => x.IndexInSegment);
                    if (logRecords.Any())
                    {
                        children = new List<PatternNode>();
                        foreach (var log in logRecords)
                        {
                            var patterNode = new PatternNode()
                            {
                                name = log.Text,
                                traceline = log.LineNumInTraceFile,
                                debugging = log.IsForDebug,
                                breaking = log.IsBreakPoint,
                                parent = parentNode
                            };
                            if (patterNode.breaking)
                            {
                                breakingNodeList.Add(patterNode);
                            }

                            children.Add(patterNode);
                        }
                    }
                }
            }

            return children;
        }

        private PatternNode getTreeNode(string patternID)
        {
            if (string.IsNullOrEmpty(patternID))
                return null;
            var pattern = db.Patterns.FirstOrDefault(x => x.ID == patternID);
            if (null == pattern)
                return null;
            PatternNode root = new PatternNode() { name = pattern.Name };

            IList<PatternNode> children = new List<PatternNode>();
            var segments = db.PatternSegments.Where(x => x.ParentID == patternID).OrderBy(x => x.IndexInPattern);
            foreach (PatternSegment segment in segments)
            {
                var patternNode = new PatternNode()
                {
                    name = segment.Name,
                    parent = root
                };
                patternNode.children = getChildNodes(segment.ID, patternNode);
                children.Add(patternNode);
            }
            root.children = children;
            root.parent = null;

            return root;
        }

        private void MarkBranch(List<PatternNode> list)
        {
            PatternNode tmpNode = null;
            foreach (PatternNode node in list)
            {
                tmpNode = node;
                while (null != tmpNode && null != tmpNode.parent)
                {
                    tmpNode.parent.breaking = true;
                    tmpNode = tmpNode.parent;
                }
            }
        }
    }
}
