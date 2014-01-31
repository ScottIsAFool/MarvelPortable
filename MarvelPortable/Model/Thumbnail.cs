using Newtonsoft.Json;
using PropertyChanged;

namespace MarvelPortable.Model
{
    [ImplementPropertyChanged]
    public class Thumbnail
    {
        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("extension")]
        public string Extension { get; set; }
    }
}