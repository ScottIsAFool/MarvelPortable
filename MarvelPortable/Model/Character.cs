using Newtonsoft.Json;
using PropertyChanged;

namespace MarvelPortable.Model
{
    [ImplementPropertyChanged]
    public class Character
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("modified")]
        public string Modified { get; set; }

        [JsonProperty("thumbnail")]
        public Thumbnail Thumbnail { get; set; }

        [JsonProperty("resourceURI")]
        public string ResourceUri { get; set; }

        [JsonProperty("comics")]
        public Collection Comics { get; set; }

        [JsonProperty("series")]
        public Collection Series { get; set; }

        [JsonProperty("stories")]
        public Collection Stories { get; set; }

        [JsonProperty("events")]
        public Collection Events { get; set; }

        [JsonProperty("urls")]
        public Url[] Urls { get; set; }
    }
}
