using CADService.Models;
using System.Data;
using System.Linq;
using System.Web.Http;

namespace CADService.Controllers
{
    [RoutePrefix("api/dict")]
    public class DictController : ApiController
    {
        private CADServiceContext db = new CADServiceContext();

        // GET: api/dict/products
        [Route("products")]
        public IQueryable<CadProduct> GetCadProducts()
        {
            foreach (var product in db.CadProducts)
            {
                short productID = product.ID;

                //order by name of product and component
                product.Components = db.CadProductComponents.Where(x => x.ProductID ==  productID).OrderByDescending(x => x.Name);
                product.Versions = db.CadProductVers.Where(x => x.ProductID == productID).OrderByDescending(x => x.Name);
                
            }

            return db.CadProducts.OrderBy(x => x.ID);
        }

        // GET: api/dict/versions
        [Route("versions")]
        public IQueryable<CadProductVer> GetProductVers()
        {
            return db.CadProductVers;
        }

        // GET: api/dict/components
        [Route("components")]
        public IQueryable<CadProductComponent> GetCadProductComponents()
        {
            return db.CadProductComponents;
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