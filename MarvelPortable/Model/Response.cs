using Newtonsoft.Json;

namespace MarvelPortable.Model
{
    public class Response<TResponseType>
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("etag")]
        public string Etag { get; set; }

        [JsonProperty("data")]
        public Data<TResponseType> Data { get; set; }
    }
}