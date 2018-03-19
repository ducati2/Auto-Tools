using CADService.DTO;
using CADService.Models;
using System.Linq;
using System.Web.Http;


namespace CADService.Controllers
{
    [RoutePrefix("api/pattern")]
    public class PatternController : ApiController
    {
        private CADServiceContext db = new CADServiceContext();

        //get all the pattern list from db.
        [Route("")]
        public IQueryable<PatternGeneral> GetAllPatternInfo()
        {
            return db.Patterns.Where(pattern => pattern.IsIssued == 0).Select(item => new PatternGeneral
            {
                ID = item.ID,
                Name = item.Name,
            });
        }
 
    }
}