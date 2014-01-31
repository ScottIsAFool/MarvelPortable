using Newtonsoft.Json;
using PropertyChanged;

namespace MarvelPortable.Model
{
    [ImplementPropertyChanged]
    public class CollectionUri
    {
        [JsonProperty("service")]
        public string Service { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }
    }
}