using Newtonsoft.Json;
using PropertyChanged;

namespace MarvelPortable.Model
{
    [ImplementPropertyChanged]
    public class Url
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("url")]
        public string Uri { get; set; }
    }
}