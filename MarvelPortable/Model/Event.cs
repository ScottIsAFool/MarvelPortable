using System.Diagnostics;
using Newtonsoft.Json;
using PropertyChanged;

namespace MarvelPortable.Model
{
    [DebuggerDisplay("Title = {Title}, ID = {Id}")]
    [ImplementPropertyChanged]
    public class Event
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("resourceURI")]
        public string ResourceUri { get; set; }

        [JsonProperty("urls")]
        public Url[] Urls { get; set; }

        [JsonProperty("modified")]
        public string Modified { get; set; }

        [JsonProperty("start")]
        public string Start { get; set; }

        [JsonProperty("end")]
        public string End { get; set; }

        [JsonProperty("thumbnail")]
        public ImageItem Thumbnail { get; set; }

        [JsonProperty("creators")]
        public Creators Creators { get; set; }

        [JsonProperty("characters")]
        public Collection Characters { get; set; }

        [JsonProperty("stories")]
        public CollectionWithUri Stories { get; set; }

        [JsonProperty("comics")]
        public Collection Comics { get; set; }

        [JsonProperty("series")]
        public CollectionWithUri Series { get; set; }

        [JsonProperty("next")]
        public Next Next { get; set; }

        [JsonProperty("previous")]
        public Previous Previous { get; set; }
    }

    public class EventResponse: Data<Event>{}
}
