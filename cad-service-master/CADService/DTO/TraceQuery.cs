using Newtonsoft.Json;

namespace CADService.DTO
{
    public class TraceQuery
    {
        [JsonProperty("condition")]
        public TraceCondition Condition { get; set; }
        [JsonProperty("pagination")]
        public Pagination Pagination { get; set; }
    }
}