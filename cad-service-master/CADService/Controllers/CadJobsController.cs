using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CADService.Models;
using System.Diagnostics;
using CADService.DTO;
using CADService.CodeDict;
using System.IO;

namespace CADService.Controllers
{
    [RoutePrefix("api/job")]
    public class CadJobsController : ApiController
    {
        private CADServiceContext db = new CADServiceContext();

        // GET: api/job
        [Route("")]
        public IQueryable<JobDetail> GetCadJobs()
        {
            return db.CadJobs.Where(job => job.OwnerID == 4).OrderByDescending(x => x.ID).Select(item => new JobDetail
            {
                ID = item.ID,
                LCID = item.LCID,
                Status = (short)item.StatusID,
                StartTime = item.CreatedTime,
                EndTime = item.AnalyzeEndTime,
                ProductID = item.ProductID,
                VersionID = item.VersionID,
                ComponentID = item.ComponentID
            });
        }

        // GET: api/job/5
        [ResponseType(typeof(CadJob))]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetCadJob(int id)
        {
            CadJob cadJob = await db.CadJobs.FindAsync(id);
            if (cadJob == null)
            {
                return NotFound();
            }

            return Ok(cadJob);
        }

        // PUT: api/CadJobs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCadJob(int id, CadJob cadJob)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cadJob.ID)
            {
                return BadRequest();
            }

            db.Entry(cadJob).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CadJobExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }



            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/job
        [ResponseType(typeof(Job))]
        [Route("")]
        [HttpPost]
        public async Task<IHttpActionResult> PostCadJob([FromBody] Job job)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CadJob cadJob = new CadJob()
            {
                LCID = job.LCID,
                ProductID = job.ProductID,
                VersionID = job.VersionID,
                ComponentID = job.ComponentID,
                Description = job.Description,
            };

            cadJob.StatusID = (byte)JobStatus.Submitted;
            cadJob.OwnerID = 4;
            cadJob.CreatedTime = DateTime.Now;

            db.CadJobs.Add(cadJob);
            db.SaveChanges();

            int jobId = cadJob.ID;

            //add a new map about job id and pattern id into JobPattern table.
            JobPattern jobPattern = new JobPattern();
            jobPattern.JobId = jobId;
            jobPattern.PatternId = job.PatternID;

            int retVal = DBUtil.executeCommand(jobPattern.ToSQLString());
          
       

            if (!string.IsNullOrEmpty(job.Trace))
            {
                string[] splitTraces = job.Trace.Split(new char[] { ';' }).Distinct<string>().ToArray<string>();
                foreach (string aTrace in splitTraces)
                {
                    var rawTraceFile = new RawTraceFile()
                    {
                        FileName = "",
                        FilePath = aTrace,
                        FileTypeID = 1,
                        ResultTable = "",
                        JobID = jobId,
                        ProductVersionID = job.VersionID,
                        ProductComponentID = job.ComponentID
                    };
                    db.RawTraceFiles.Add(rawTraceFile);
                }
            }

            /*
            if (!string.IsNullOrEmpty(job.PDB))
            {
                string[] splitPDBs = job.PDB.Split(new char[] { ';' }).Distinct<string>().ToArray<string>();
                foreach (string aPDB in splitPDBs)
                {
                    db.RawPDBDirs.Add(new RawPDBDir()
                    {
                        DirName = aPDB,
                        JobID = jobId
                    });
                }
            }
            */

            if (!string.IsNullOrEmpty(job.TMF))
            {
                string[] splitTMFs = job.TMF.Split(new char[] { ';' }).Distinct<string>().ToArray<string>();
                foreach (string aTMF in splitTMFs)
                {
                    db.RawTMFDirs.Add(new RawTMFDir()
                    {
                        DirName = aTMF,
                        JobID = jobId
                    });
                }
            }


            /*
            if (!string.IsNullOrEmpty(job.Source))
            {
                string[] splitSources = job.Source.Split(new char[] { ';' }).Distinct<string>().ToArray<string>();
                foreach (string aSource in splitSources)
                {
                    db.RawSourceDirs.Add(new RawSourceDir()
                    {
                        DirName = aSource,
                        JobID = jobId
                    });
                }
            }
            */

            await db.SaveChangesAsync();

            return Ok(jobId);
        }

        // DELETE: api/CadJobs/5
        [ResponseType(typeof(CadJob))]
        public async Task<IHttpActionResult> DeleteCadJob(int id)
        {
            CadJob cadJob = await db.CadJobs.FindAsync(id);
            if (cadJob == null)
            {
                return NotFound();
            }

            db.CadJobs.Remove(cadJob);
            await db.SaveChangesAsync();

            return Ok(cadJob);
        }

        // GET: api/job/onefix/LC9085
        [ResponseType(typeof(CaseDetail))]
        [Route("onefix/{lcid}")]
        public IHttpActionResult GetCadJob(string lcid)
        {
            CadOnefixCase onefix = db.CadOnefixCases.FirstOrDefault(x => x.CaseID == lcid);
            if (onefix == null)
            {
                return StatusCode(HttpStatusCode.NotFound);
            }
            var product = db.CadProducts.FirstOrDefault(x => x.ProductName == onefix.ServiceProduct);
            var version = db.CadProductVers.FirstOrDefault(x => x.Name == onefix.ProductVersion);
            var component = db.CadProductComponents.FirstOrDefault(x => x.Name == onefix.Component);

            return Ok(new CaseDetail()
            {
                CaseID = onefix.CaseID,
                ServiceProduct = product,
                ProductVersion = version,
                Component = component,
                Description = onefix.Title,
                Trace = onefix.RealLogFiles
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CadJobExists(int id)
        {
            return db.CadJobs.Count(e => e.ID == id) > 0;
        }
    }
}