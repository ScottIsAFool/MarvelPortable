﻿using System.Diagnostics;
using Newtonsoft.Json;
using PropertyChanged;

namespace MarvelPortable.Model
{
    [DebuggerDisplay("Name = {Name}, ID = {Id}")]
    [ImplementPropertyChanged]
    public class Character
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

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

    public class CharacterResponse : Data<Character>{}
}
