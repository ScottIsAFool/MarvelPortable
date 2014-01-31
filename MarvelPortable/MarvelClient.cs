using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MarvelPortable.Extensions;
using MarvelPortable.Logging;
using MarvelPortable.Model;

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

        #region Public Properties
        public HttpClient HttpClient { get; private set; }
        public string ApiKey { get; private set; }
        public string PrivateKey { get; private set; }
        #endregion

        public async Task<CharacterResponse> GetCharactersAsync(
            string name = "",
            DateTime? modifiedSince = null,
            List<int> comicIds = null,
            List<int> seriesIds = null,
            List<int> eventIds = null,
            List<int> storyIds = null,
            SortBy sortBy = SortBy.Name,
            OrderBy orderBy = OrderBy.Ascending,
            int? limit = null,
            int? offset = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var postData = new Dictionary<string, string>();
            if(!string.IsNullOrEmpty(name))
            {
                postData.Add("name", name);
            }

            if (modifiedSince.HasValue)
            {
                postData.Add("modifiedSince", modifiedSince.Value.ToString());
            }

            if (!comicIds.IsNullOrEmpty())
            {
                postData.Add("comics", string.Join(",", comicIds));
            }

            if (!seriesIds.IsNullOrEmpty())
            {
                postData.Add("series", string.Join(",", seriesIds));
            }

            if (!eventIds.IsNullOrEmpty())
            {
                postData.Add("events", string.Join(",", eventIds));
            }

            if (!storyIds.IsNullOrEmpty())
            {
                postData.Add("stories", string.Join(",", storyIds));
            }

            var sort = sortBy.ToString().ToLower();
            if (orderBy == OrderBy.Descending)
            {
                sort = "-" + sort;
            }
            postData.Add("orderBy", sort);

            if (limit.HasValue)
            {
                postData.Add("limit", limit.Value.ToString());
            }

            if (offset.HasValue)
            {
                postData.Add("offset", offset.Value.ToString());
            }

            var options = postData.ToQueryString();

            var response = await GetResponse<Character>("characters", options, cancellationToken);
        }

        private async Task<Data<TReturnType>> GetResponse<TReturnType>(string method, string options, CancellationToken cancellationToken = default(CancellationToken))
        {
            var fullOptions = string.Concat(options, GetHashAndTimeStamp());

            var url = string.Format("{0}/{1}?{2}", ApiUrl, method, fullOptions);

            var response = await HttpClient.GetAsync(url, cancellationToken);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var item = await responseString.DeserialiseAsync<Response<TReturnType>>();
            return item.Data;
        }

        private string GetHashAndTimeStamp()
        {
            var now = DateTime.Now.ToString();

            var toHash = string.Concat(now, PrivateKey, ApiKey);
            var hash = MD5Core.GetHashString(toHash);

            return string.Format("&ts={0}&hash={1}", now, hash);
        }
    }
}
