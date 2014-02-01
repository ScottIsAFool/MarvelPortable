using System.Diagnostics;
using Newtonsoft.Json;
using PropertyChanged;

namespace MarvelPortable.Model
{
    [DebuggerDisplay("Title = {Title}, ID = {Id}")]
    [ImplementPropertyChanged]
    public class Story
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("resourceURI")]
        public string ResourceUri { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("modified")]
        public string Modified { get; set; }

        [JsonProperty("thumbnail")]
        public object Thumbnail { get; set; }

        [JsonProperty("creators")]
        public Creators Creators { get; set; }

        [JsonProperty("characters")]
        public Collection Characters { get; set; }

        [JsonProperty("series")]
        public CollectionWithUri Series { get; set; }

        [JsonProperty("comics")]
        public Collection Comics { get; set; }

        [JsonProperty("events")]
        public Collection Events { get; set; }

        [JsonProperty("originalIssue")]
        public CollectionItem OriginalIssue { get; set; }
    }

    public class StoryResponse : Data<Story>{}
}
