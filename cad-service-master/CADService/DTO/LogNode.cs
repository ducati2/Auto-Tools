using System.Text;

namespace CADService.DTO
{
    public class LogNode
    {
        public string logtext { get; set; }
        public int traceline { get; set; }
        public bool debugging { get; set; }
        public bool breaking { get; set; }

        public override string ToString()
        {
            return string.Format("{\"logtext\":\"{0}\",\"traceline\":{1},\"debugging\":{2},\"breaking\":{3}}", logtext, traceline, debugging, breaking);
        }

    }
}