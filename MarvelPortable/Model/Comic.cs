using System.Diagnostics;
using Newtonsoft.Json;
using PropertyChanged;

namespace MarvelPortable.Model
{
    [DebuggerDisplay("Title = {Title}, ID = {Id}")]
    [ImplementPropertyChanged]
    public class Comic
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("digitalId")]
        public int DigitalId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("issueNumber")]
        public int IssueNumber { get; set; }

        [JsonProperty("variantDescription")]
        public string VariantDescription { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("modified")]
        public string Modified { get; set; }

        [JsonProperty("isbn")]
        public string Isbn { get; set; }

        [JsonProperty("upc")]
        public string Upc { get; set; }

        [JsonProperty("diamondCode")]
        public string DiamondCode { get; set; }

        [JsonProperty("ean")]
        public string Ean { get; set; }

        [JsonProperty("issn")]
        public string Issn { get; set; }

        [JsonProperty("format")]
        public string Format { get; set; }

        [JsonProperty("pageCount")]
        public int PageCount { get; set; }

        [JsonProperty("textObjects")]
        public TextObject[] TextObjects { get; set; }

        [JsonProperty("resourceURI")]
        public string ResourceUri { get; set; }

        [JsonProperty("urls")]
        public Url[] Urls { get; set; }

        [JsonProperty("series")]
        public Collection Series { get; set; }

        [JsonProperty("variants")]
        public CollectionItem[] Variants { get; set; }

        [JsonProperty("collections")]
        public object[] Collections { get; set; }

        [JsonProperty("collectedIssues")]
        public object[] CollectedIssues { get; set; }

        [JsonProperty("dates")]
        public DateItem[] Dates { get; set; }

        [JsonProperty("prices")]
        public PriceItem[] Prices { get; set; }

        [JsonProperty("thumbnail")]
        public ImageItem Thumbnail { get; set; }

        [JsonProperty("images")]
        public ImageItem[] Images { get; set; }

        [JsonProperty("creators")]
        public Creators Creators { get; set; }

        [JsonProperty("characters")]
        public Collection Characters { get; set; }

        [JsonProperty("stories")]
        public Collection Stories { get; set; }

        [JsonProperty("events")]
        public Collection Events { get; set; }
    }

    public class ComicResponse : Data<Comic>
    {
    }
}
