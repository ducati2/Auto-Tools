using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CADService.DTO
{

    public class PatternNode
    {
        //[JsonIgnore]
        //public string SegmentID { get; set; }
        [JsonIgnore]
        public PatternNode parent { get; set; }
        public String name { get; set; }
        public IList<PatternNode> children { get; set; }
        //public IList<LogNode> logs { get; set; }
        public int traceline { get; set; }
        public bool debugging { get; set; }
        public bool breaking { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Example: {"name":"flare","children":[]}</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("{");
            sb.AppendFormat("\"name\":\"{0}\",", this.name);
            if (null == children || children.Count == 0)
            {
                sb.AppendFormat("\"children\":\"{0}\"", "[]");
            }
            else
            {
                sb.Append("[");
                IList<string> nodeList = new List<string>();
                foreach (PatternNode node in children)
                {
                    nodeList.Add(node.ToString());
                }
                sb.Append(string.Join(",", nodeList.ToArray<string>()));
                sb.Append("]");
            }
            /*
            if (null == logs || logs.Count == 0)
            {
                sb.AppendFormat("\"logs\":\"{0}\"", "[]");
            }
            else
            {
                sb.Append("[");
                IList<string> nodeList = new List<string>();
                foreach (LogNode node in logs)
                {
                    nodeList.Add(node.ToString());
                }
                sb.Append(string.Join(",", nodeList.ToArray<string>()));
                sb.Append("]");
            }
            */
            sb.Append("}");
            return sb.ToString();
        }

        /// <summary>
        /// Serialize some property condition
        /// </summary>
        /// <returns></returns>
        //public bool ShouldSerializechildren()
        //{
        //    return true;
        //}

        //public static void Main(string[] args)
        //{

        //}
    }
}