using Newtonsoft.Json;
using PropertyChanged;

namespace MarvelPortable.Model
{
    [ImplementPropertyChanged]
    public class CollectionItem
    {
        [JsonProperty("resourceURI")]
        public string ResourceUri { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}