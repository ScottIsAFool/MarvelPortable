using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
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

        /// <summary>
        /// Gets the characters asynchronous.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="modifiedSince">The modified since.</param>
        /// <param name="comicIds">The comic ids.</param>
        /// <param name="seriesIds">The series ids.</param>
        /// <param name="eventIds">The event ids.</param>
        /// <param name="storyIds">The story ids.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
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

            if (comicIds != null && comicIds.Any())
            {
                postData.Add("comics", string.Join(",", comicIds));
            }

            if (seriesIds != null && seriesIds.Any())
            {
                postData.Add("series", string.Join(",", seriesIds));
            }

            if (eventIds != null && eventIds.Any())
            {
                postData.Add("events", string.Join(",", eventIds));
            }

            if (storyIds != null && storyIds.Any())
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

            var response = await GetResponse<Response<Character>>("characters", options, cancellationToken);

            return await response.Data.ToCollection<CharacterResponse>();
        }

        /// <summary>
        /// Gets the character asynchronous.
        /// </summary>
        /// <param name="characterId">The character identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<CharacterResponse> GetCharacterAsync(int characterId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var method = string.Format("characters/{0}", characterId);

            var response = await GetResponse<Response<Character>>(method, string.Empty, cancellationToken);

            return await response.Data.ToCollection<CharacterResponse>();
        }

        private async Task<TReturnType> GetResponse<TReturnType>(string method, string options, CancellationToken cancellationToken = default(CancellationToken), [CallerMemberName] string callingMethod = "") where TReturnType : MarvelBase
        {
            var fullOptions = string.Concat(options, GetHashAndTimeStamp());

            var url = string.Format("{0}/{1}?{2}", ApiUrl, method, fullOptions);

            _logger.Debug("GET: {0}", url);
            var requestTime = DateTime.Now;

            var response = await HttpClient.GetAsync(url, cancellationToken);

            var duration = DateTime.Now - requestTime;

            _logger.Debug("Received {0} status after {1}ms from {2}: {3}", response.StatusCode, duration.TotalMilliseconds, "GET", url);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var result = await responseString.DeserialiseAsync<TReturnType>();

            if (result.Code != 200)
            {
                throw new MarvelException(result.Code, result.Status);
            }

            return result;
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
