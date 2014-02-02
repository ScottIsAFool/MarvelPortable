using System.Diagnostics;
using Newtonsoft.Json;
using PropertyChanged;

namespace MarvelPortable.Model
{
    [DebuggerDisplay("Title = {Title}, ID = {Id}")]
    [ImplementPropertyChanged]
    public class Series
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

        [JsonProperty("startYear")]
        public int StartYear { get; set; }

        [JsonProperty("endYear")]
        public int EndYear { get; set; }

        [JsonProperty("rating")]
        public string Rating { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("modified")]
        public string Modified { get; set; }

        [JsonProperty("thumbnail")]
        public ImageItem Thumbnail { get; set; }

        [JsonProperty("creators")]
        public Creators Creators { get; set; }

        [JsonProperty("characters")]
        public Collection Characters { get; set; }

        [JsonProperty("stories")]
        public Collection Stories { get; set; }

        [JsonProperty("comics")]
        public Collection Comics { get; set; }

        [JsonProperty("events")]
        public Collection Events { get; set; }

        [JsonProperty("next")]
        public Next Next { get; set; }

        [JsonProperty("previous")]
        public Previous Previous { get; set; }
    }

    public class SeriesResponse : Data<Series>{}
}
