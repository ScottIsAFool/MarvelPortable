using Newtonsoft.Json;

namespace MarvelPortable.Model
{
    public class Data<TResponseType>
    {
        [JsonProperty("offset")]
        public int Offset { get; set; }

        [JsonProperty("limit")]
        public int Limit { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("results")]
        public TResponseType[] Results { get; set; }
    }
}