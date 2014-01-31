using Newtonsoft.Json;
using PropertyChanged;

namespace MarvelPortable.Model
{
    [ImplementPropertyChanged]
    public class Response<TResponseType> : MarvelBase
    {
        [JsonProperty("data")]
        public Data<TResponseType> Data { get; set; }
    }
}