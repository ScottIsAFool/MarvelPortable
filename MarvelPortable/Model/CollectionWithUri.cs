using Newtonsoft.Json;
using PropertyChanged;

namespace MarvelPortable.Model
{
    [ImplementPropertyChanged]
    public class CollectionWithUri
    {
        [JsonProperty("available")]
        public int Available { get; set; }

        [JsonProperty("collectionURI")]
        public CollectionUri CollectionUri { get; set; }

        [JsonProperty("items")]
        public CollectionItem[] Items { get; set; }

        [JsonProperty("returned")]
        public int Returned { get; set; }
    }
}