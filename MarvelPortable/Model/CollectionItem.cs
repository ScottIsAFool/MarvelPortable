using System.Diagnostics;
using Newtonsoft.Json;
using PropertyChanged;

namespace MarvelPortable.Model
{
    [DebuggerDisplay("Name = {Name}, ID = {Id}")]
    [ImplementPropertyChanged]
    public class CollectionItem
    {
        [JsonProperty("resourceURI")]
        public string ResourceUri { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonIgnore]
        public int Id
        {
            get
            {
                var idString = ResourceUri.Replace(MarvelClient.ApiUrl, string.Empty)
                    .Replace("/comics/", string.Empty)
                    .Replace("/characters/", string.Empty)
                    .Replace("/stories/", string.Empty)
                    .Replace("/series/", string.Empty)
                    .Replace("/events/", string.Empty)
                    .Replace("/creators/", string.Empty);
                int id;
                int.TryParse(idString, out id);
                return id;
            }
        }

    }
}