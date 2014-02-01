using Newtonsoft.Json;
using PropertyChanged;

namespace MarvelPortable.Model
{
    [ImplementPropertyChanged]
    public class ImageItem
    {
        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("extension")]
        public string Extension { get; set; }

        /// <summary>
        /// Gets the full size URI.
        /// </summary>
        /// <value>
        /// The full size URI.
        /// </value>
        [JsonIgnore]
        public string FullSizeUri
        {
            get { return string.Format("{0}.{1}", Path, Extension); }
        }

        /// <summary>
        /// Gets the portrait small URI (50x75).
        /// </summary>
        /// <value>
        /// The portrait small URI.
        /// </value>
        [JsonIgnore]
        public string PortraitSmallUri
        {
            get { return string.Format("{0}/{1}.{2}", Path, "portrait_small", Extension); }
        }

        /// <summary>
        /// Gets the portrait medium URI (100x150).
        /// </summary>
        /// <value>
        /// The portrait medium URI.
        /// </value>
        [JsonIgnore]
        public string PortraitMediumUri
        {
            get { return string.Format("{0}/{1}.{2}", Path, "portrait_medium", Extension); }
        }

        /// <summary>
        /// Gets the portrait extra large URI (150x225).
        /// </summary>
        /// <value>
        /// The portrait extra large URI.
        /// </value>
        [JsonIgnore]
        public string PortraitExtraLargeUri
        {
            get { return string.Format("{0}/{1}.{2}", Path, "portrait_xlarge", Extension); }
        }

        /// <summary>
        /// Gets the portrait fantastic URI (168x252).
        /// </summary>
        /// <value>
        /// The portrait fantastic URI.
        /// </value>
        [JsonIgnore]
        public string PortraitFantasticUri
        {
            get { return string.Format("{0}/{1}.{2}", Path, "portrait_fantastic", Extension); }
        }

        /// <summary>
        /// Gets the portrait uncanny URI (300x450).
        /// </summary>
        /// <value>
        /// The portrait uncanny URI.
        /// </value>
        [JsonIgnore]
        public string PortraitUncannyUri
        {
            get { return string.Format("{0}/{1}.{2}", Path, "portrait_uncanny", Extension); }
        }

        /// <summary>
        /// Gets the portrait incredible URI (216x324).
        /// </summary>
        /// <value>
        /// The portrait incredible URI.
        /// </value>
        [JsonIgnore]
        public string PortraitIncredibleUri
        {
            get { return string.Format("{0}/{1}.{2}", Path, "portrait_incredible", Extension); }
        }

        /// <summary>
        /// Gets the standard small URI (65x45).
        /// </summary>
        /// <value>
        /// The standard small URI.
        /// </value>
        [JsonIgnore]
        public string StandardSmallUri
        {
            get { return string.Format("{0}/{1}.{2}", Path, "standard_small", Extension); }
        }

        /// <summary>
        /// Gets the standard medium URI (100x100).
        /// </summary>
        /// <value>
        /// The standard medium URI.
        /// </value>
        [JsonIgnore]
        public string StandardMediumUri
        {
            get { return string.Format("{0}/{1}.{2}", Path, "standard_medium", Extension); }
        }

        /// <summary>
        /// Gets the standard large URI (140x140).
        /// </summary>
        /// <value>
        /// The standard large URI.
        /// </value>
        [JsonIgnore]
        public string StandardLargeUri
        {
            get { return string.Format("{0}/{1}.{2}", Path, "standard_large", Extension); }
        }

        /// <summary>
        /// Gets the standard extra large URI (200x200).
        /// </summary>
        /// <value>
        /// The standard extra large URI.
        /// </value>
        [JsonIgnore]
        public string StandardExtraLargeUri
        {
            get { return string.Format("{0}/{1}.{2}", Path, "standard_xlarge", Extension); }
        }

        /// <summary>
        /// Gets the standard fantastic URI (250x250).
        /// </summary>
        /// <value>
        /// The standard fantastic URI.
        /// </value>
        [JsonIgnore]
        public string StandardFantasticUri
        {
            get { return string.Format("{0}/{1}.{2}", Path, "standard_fantastic", Extension); }
        }

        /// <summary>
        /// Gets the standard amazing URI (180x180).
        /// </summary>
        /// <value>
        /// The standard amazing URI.
        /// </value>
        [JsonIgnore]
        public string StandardAmazingUri
        {
            get { return string.Format("{0}/{1}.{2}", Path, "standard_amazing", Extension); }
        }

        /// <summary>
        /// Gets the landscape small URI (120x90).
        /// </summary>
        /// <value>
        /// The landscape small URI.
        /// </value>
        [JsonIgnore]
        public string LandscapeSmallUri
        {
            get { return string.Format("{0}/{1}.{2}", Path, "landscape_small", Extension); }
        }

        /// <summary>
        /// Gets the landscape medium URI (175x130).
        /// </summary>
        /// <value>
        /// The landscape medium URI.
        /// </value>
        [JsonIgnore]
        public string LandscapeMediumUri
        {
            get { return string.Format("{0}/{1}.{2}", Path, "landscape_medium", Extension); }
        }

        /// <summary>
        /// Gets the landscape large URI (190x140).
        /// </summary>
        /// <value>
        /// The landscape large URI.
        /// </value>
        [JsonIgnore]
        public string LandscapeLargeUri
        {
            get { return string.Format("{0}/{1}.{2}", Path, "landscape_large", Extension); }
        }

        /// <summary>
        /// Gets the landscape extra large URI (270x200).
        /// </summary>
        /// <value>
        /// The landscape extra large URI.
        /// </value>
        [JsonIgnore]
        public string LandscapeExtraLargeUri
        {
            get { return string.Format("{0}/{1}.{2}", Path, "landscape_xlarge", Extension); }
        }

        /// <summary>
        /// Gets the landscape amazing URI (250x156).
        /// </summary>
        /// <value>
        /// The landscape amazing URI.
        /// </value>
        [JsonIgnore]
        public string LandscapeAmazingUri
        {
            get { return string.Format("{0}/{1}.{2}", Path, "landscape_amazing", Extension); }
        }

        /// <summary>
        /// Gets the landscape incredible URI (464x261).
        /// </summary>
        /// <value>
        /// The landscape incredible URI.
        /// </value>
        public string LandscapeIncredibleUri
        {
            get { return string.Format("{0}/{1}.{2}", Path, "landscape_incredible", Extension); }
        }
    }
}