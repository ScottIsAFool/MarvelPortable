using System;
using System.Net;
using System.Net.Http;
using MarvelPortable.Logging;

namespace MarvelPortable
{
    public class MarvelClient
    {
        private const string ApiUrl = "http://gateway.marvel.com:80/v1/public";

        private readonly ILogger _logger;

        #region Constructors
        public MarvelClient(string apiKey, string privateKey)
            : this(apiKey, privateKey, null, new NullLogger())
        {}

        public MarvelClient(string apiKey, string privateKey, HttpMessageHandler handler)
            : this(apiKey, privateKey, handler, new NullLogger())
        {}

        public MarvelClient(string apiKey, string privateKey, ILogger logger)
            : this(apiKey, privateKey, null, logger)
        {}

        public MarvelClient(string apiKey, string privateKey, HttpMessageHandler handler, ILogger logger)
        {
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new ArgumentNullException("apiKey", "API Key cannot be null or empty");
            }

            ApiKey = apiKey;
            PrivateKey = privateKey;
            _logger = logger ?? new NullLogger();

            HttpClient = handler == null
                ? new HttpClient(new HttpClientHandler {AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip})
                : new HttpClient(handler);
        }
        #endregion

        public HttpClient HttpClient { get; private set; }
        public string ApiKey { get; private set; }
        public string PrivateKey { get; private set; }

        private string GetHashAndTimeStamp()
        {
            var now = DateTime.Now.ToString();

            var toHash = string.Concat(now, PrivateKey, ApiKey);
            var hash = MD5Core.GetHashString(toHash);

            return string.Format("&ts={0}&hash={1}", now, hash);
        }
    }
}
