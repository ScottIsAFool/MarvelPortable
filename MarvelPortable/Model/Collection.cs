using Newtonsoft.Json;
using PropertyChanged;

namespace MarvelPortable.Model
{
    [ImplementPropertyChanged]
    public class Collection
    {
        [JsonProperty("available")]
        public int Available { get; set; }
        
        [JsonProperty("items")]
        public CollectionItem[] Items { get; set; }

        [JsonProperty("returned")]
        public int Returned { get; set; }
    }
}