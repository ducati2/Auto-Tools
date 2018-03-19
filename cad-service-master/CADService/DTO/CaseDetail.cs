using CADService.Models;

namespace CADService.DTO
{
    public class CaseDetail
    {
        public string CaseID { get; set; }
        public CadProduct ServiceProduct { get; set; }
        public CadProductVer ProductVersion { get; set; }
        public CadProductComponent Component { get; set; }
        public string Description { get; set; }
        public string Trace { get; set; }

    }
}