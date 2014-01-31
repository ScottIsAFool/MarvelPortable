using Newtonsoft.Json;
using PropertyChanged;

namespace MarvelPortable.Model
{
    [ImplementPropertyChanged]
    public class PriceItem
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("price")]
        public double Price { get; set; }
    }
}