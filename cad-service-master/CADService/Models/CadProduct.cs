using System.Collections.Generic;
using System.Linq;

namespace CADService.Models
{


    public  class CadProduct
    {
        public short ID { get; set; }
        public string ProductLine { get; set; }
        public string ProductName { get; set; }
        public IQueryable<CadProductComponent> Components { get; set; }
        public IQueryable<CadProductVer> Versions { get; set; }
    }
}
