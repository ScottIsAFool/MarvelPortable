using System.Diagnostics;
using Newtonsoft.Json;
using PropertyChanged;

namespace MarvelPortable.Model
{
    [DebuggerDisplay("Name = {FirstName} {LastName}, ID = {Id}")]
    [ImplementPropertyChanged]
    public class Creator
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("middleName")]
        public string MiddleName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("suffix")]
        public string Suffix { get; set; }

        [JsonProperty("fullName")]
        public string FullName { get; set; }

        [JsonProperty("modified")]
        public string Modified { get; set; }

        [JsonProperty("thumbnail")]
        public ImageItem Thumbnail { get; set; }

        [JsonProperty("resourceURI")]
        public string ResourceUri { get; set; }

        [JsonProperty("comics")]
        public Collection Comics { get; set; }

        [JsonProperty("series")]
        public Collection Series { get; set; }

        [JsonProperty("stories")]
        public Collection Stories { get; set; }

        [JsonProperty("events")]
        public Collection Events { get; set; }

        [JsonProperty("urls")]
        public Url[] Urls { get; set; }
    }

    public class CreatorResponse : Data<Creator>{}
}
