using Newtonsoft.Json;

namespace CADService.DTO
{
    public class TraceCondition
    {
        [JsonProperty("module")]
        public string Module { get; set; }
        [JsonProperty("src")]
        public string Src { get; set; }
        [JsonProperty("func")]
        public string Function { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}