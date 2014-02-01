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
        { }

        public MarvelClient(string apiKey, string privateKey, HttpMessageHandler handler)
            : this(apiKey, privateKey, handler, new NullLogger())
        { }

        public MarvelClient(string apiKey, string privateKey, ILogger logger)
            : this(apiKey, privateKey, null, logger)
        { }

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
                ? new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip })
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
            postData.AddIfNotNull("name", name);
            postData.AddIfNotNull("modifiedSince", modifiedSince);
            postData.AddIfNotNull("comics", comicIds);
            postData.AddIfNotNull("series", seriesIds);
            postData.AddIfNotNull("events", eventIds);
            postData.AddIfNotNull("stories", storyIds);

            var sort = sortBy.ToString().ToLower();
            if (orderBy == OrderBy.Descending)
            {
                sort = "-" + sort;
            }
            postData.Add("orderBy", sort);

            postData.AddIfNotNull("limit", limit);
            postData.AddIfNotNull("offset", offset);

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

        /// <summary>
        /// Gets the comics for character asynchronous.
        /// </summary>
        /// <param name="characterId">The character identifier.</param>
        /// <param name="comicFormat">The comic format.</param>
        /// <param name="comicType">Type of the comic.</param>
        /// <param name="noVariants">The no variants.</param>
        /// <param name="dateDescriptor">The date descriptor.</param>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <param name="hasDigitalIssue">The has digital issue.</param>
        /// <param name="modifiedSince">The modified since.</param>
        /// <param name="creatorIds">The creator ids.</param>
        /// <param name="seriesIds">The series ids.</param>
        /// <param name="eventIds">The event ids.</param>
        /// <param name="storyIds">The story ids.</param>
        /// <param name="otherCharacterIds">The other character ids.</param>
        /// <param name="collaboratorIds">The collaborator ids.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<ComicResponse> GetComicsForCharacterAsync(
            int characterId,
            ComicFormat? comicFormat = null,
            ComicType? comicType = null,
            bool? noVariants = null,
            DateDescriptor? dateDescriptor = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            bool? hasDigitalIssue = null,
            DateTime? modifiedSince = null,
            List<int> creatorIds = null,
            List<int> seriesIds = null,
            List<int> eventIds = null,
            List<int> storyIds = null,
            List<int> otherCharacterIds = null,
            List<int> collaboratorIds = null,
            ComicSortBy? sortBy = null,
            OrderBy? orderBy = null,
            int? limit = null,
            int? offset = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var postData = GetComicsDictionaryInternal(
                new Dictionary<string, string> { { "characterId", characterId.ToString() } },
                comicFormat,
                comicType,
                noVariants,
                dateDescriptor,
                fromDate,
                toDate,
                hasDigitalIssue,
                modifiedSince,
                creatorIds,
                seriesIds,
                eventIds,
                storyIds,
                otherCharacterIds,
                collaboratorIds,
                sortBy,
                orderBy,
                limit,
                offset);

            var options = postData.ToQueryString();
            var method = string.Format("characters/{0}/comics", characterId);

            var response = await GetResponse<Response<Comic>>(method, options, cancellationToken);

            return await response.Data.ToCollection<ComicResponse>();
        }

        /// <summary>
        /// Gets the events for character asynchronous.
        /// </summary>
        /// <param name="characterId">The character identifier.</param>
        /// <param name="eventName">Name of the event.</param>
        /// <param name="modifiedSince">The modified since.</param>
        /// <param name="creatorIds">The creator ids.</param>
        /// <param name="seriesIds">The series ids.</param>
        /// <param name="comicIds">The comic ids.</param>
        /// <param name="storyIds">The story ids.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<EventResponse> GetEventsForCharacterAsync(
            int characterId,
            string eventName = null,
            DateTime? modifiedSince = null,
            List<int> creatorIds = null,
            List<int> seriesIds = null,
            List<int> comicIds = null,
            List<int> storyIds = null,
            SortBy? sortBy = null,
            OrderBy? orderBy = null,
            int? limit = null,
            int? offset = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var postData = new Dictionary<string, string> { { "characterId", characterId.ToString() } };
            postData.AddIfNotNull("name", eventName);
            postData.AddIfNotNull("modifiedSince", modifiedSince);
            postData.AddIfNotNull("creators", creatorIds);
            postData.AddIfNotNull("series", seriesIds);
            postData.AddIfNotNull("comics", comicIds);
            postData.AddIfNotNull("stories", storyIds);

            var sort = sortBy.ToString().ToLower();
            if (orderBy == OrderBy.Descending)
            {
                sort = "-" + sort;
            }
            postData.Add("orderBy", sort);
            postData.AddIfNotNull("limit", limit);
            postData.AddIfNotNull("offset", offset);

            var options = postData.ToQueryString();
            var method = string.Format("characters/{0}/events", characterId);

            var response = await GetResponse<Response<Event>>(method, options, cancellationToken);

            return await response.Data.ToCollection<EventResponse>();
        }

        /// <summary>
        /// Gets the stories for character asynchronous.
        /// </summary>
        /// <param name="characterId">The character identifier.</param>
        /// <param name="modifiedSince">The modified since.</param>
        /// <param name="creatorIds">The creator ids.</param>
        /// <param name="seriesIds">The series ids.</param>
        /// <param name="comicIds">The comic ids.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<StoryResponse> GetStoriesForCharacterAsync(
            int characterId,
            DateTime? modifiedSince = null,
            List<int> creatorIds = null,
            List<int> seriesIds = null,
            List<int> comicIds = null,
            SortBy? sortBy = null,
            OrderBy? orderBy = null,
            int? limit = null,
            int? offset = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var postData = new Dictionary<string, string> { { "characterId", characterId.ToString() } };
            postData.AddIfNotNull("modifiedSince", modifiedSince);
            postData.AddIfNotNull("creators", creatorIds);
            postData.AddIfNotNull("series", seriesIds);
            postData.AddIfNotNull("comics", comicIds);

            var sort = sortBy.ToString().ToLower();
            if (orderBy == OrderBy.Descending)
            {
                sort = "-" + sort;
            }
            postData.Add("orderBy", sort);
            postData.AddIfNotNull("limit", limit);
            postData.AddIfNotNull("offset", offset);

            var options = postData.ToQueryString();
            var method = string.Format("characters/{0}/stories", characterId);

            var response = await GetResponse<Response<Story>>(method, options, cancellationToken);

            return await response.Data.ToCollection<StoryResponse>();
        }

        /// <summary>
        /// Gets the comics asynchronous.
        /// </summary>
        /// <param name="comicFormat">The comic format.</param>
        /// <param name="comicType">Type of the comic.</param>
        /// <param name="noVariants">The no variants.</param>
        /// <param name="dateDescriptor">The date descriptor.</param>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <param name="hasDigitalIssue">The has digital issue.</param>
        /// <param name="modifiedSince">The modified since.</param>
        /// <param name="creatorIds">The creator ids.</param>
        /// <param name="seriesIds">The series ids.</param>
        /// <param name="eventIds">The event ids.</param>
        /// <param name="storyIds">The story ids.</param>
        /// <param name="otherCharacterIds">The other character ids.</param>
        /// <param name="collaboratorIds">The collaborator ids.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<ComicResponse> GetComicsAsync(
            ComicFormat? comicFormat = null,
            ComicType? comicType = null,
            bool? noVariants = null,
            DateDescriptor? dateDescriptor = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            bool? hasDigitalIssue = null,
            DateTime? modifiedSince = null,
            List<int> creatorIds = null,
            List<int> seriesIds = null,
            List<int> eventIds = null,
            List<int> storyIds = null,
            List<int> otherCharacterIds = null,
            List<int> collaboratorIds = null,
            ComicSortBy? sortBy = null,
            OrderBy? orderBy = null,
            int? limit = null,
            int? offset = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var postData = GetComicsDictionaryInternal(
                new Dictionary<string, string>(),
                comicFormat,
                comicType,
                noVariants,
                dateDescriptor,
                fromDate,
                toDate,
                hasDigitalIssue,
                modifiedSince,
                creatorIds,
                seriesIds,
                eventIds,
                storyIds,
                otherCharacterIds,
                collaboratorIds,
                sortBy,
                orderBy,
                limit,
                offset);

            var options = postData.ToQueryString();

            var response = await GetResponse<Response<Comic>>("comics", options, cancellationToken);

            return await response.Data.ToCollection<ComicResponse>();
        }

        /// <summary>
        /// Gets the comic asynchronous.
        /// </summary>
        /// <param name="comicId">The comic identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<ComicResponse> GetComicAsync(int comicId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var method = string.Format("comics/{0}", comicId);

            var response = await GetResponse<Response<Comic>>(method, string.Empty, cancellationToken);

            return await response.Data.ToCollection<ComicResponse>();
        }

        private static Dictionary<string, string> GetComicsDictionaryInternal(
            Dictionary<string, string> postData,
            ComicFormat? comicFormat = null,
            ComicType? comicType = null,
            bool? noVariants = null,
            DateDescriptor? dateDescriptor = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            bool? hasDigitalIssue = null,
            DateTime? modifiedSince = null,
            List<int> creatorIds = null,
            List<int> seriesIds = null,
            List<int> eventIds = null,
            List<int> storyIds = null,
            List<int> otherCharacterIds = null,
            List<int> collaboratorIds = null,
            ComicSortBy? sortBy = null,
            OrderBy? orderBy = null,
            int? limit = null,
            int? offset = null)
        {
            postData.AddIfNotNull("format", comicFormat);
            postData.AddIfNotNull("formatType", comicFormat);
            postData.AddIfNotNull("noVariants", noVariants);
            postData.AddIfNotNull("dateDescriptor", dateDescriptor);
            if (fromDate.HasValue && toDate.HasValue)
            {
                var range = string.Format("{0},{1}", fromDate.Value.ToString("YYYY-MM-DD"), toDate.Value.ToString("YYYY-MM-DD"));
                postData.Add("dateRange", range);
            }

            postData.AddIfNotNull("hasDigitalIssue", hasDigitalIssue);
            postData.AddIfNotNull("modifiedSince", modifiedSince);
            postData.AddIfNotNull("creators", creatorIds);
            postData.AddIfNotNull("series", seriesIds);
            postData.AddIfNotNull("events", eventIds);
            postData.AddIfNotNull("stories", storyIds);
            postData.AddIfNotNull("sharedAppearances", otherCharacterIds);
            postData.AddIfNotNull("collaborators", collaboratorIds);

            var sort = sortBy.ToString().ToLower();
            if (orderBy == OrderBy.Descending)
            {
                sort = "-" + sort;
            }
            postData.Add("orderBy", sort);
            postData.AddIfNotNull("limit", limit);
            postData.AddIfNotNull("offset", offset);

            return postData;
        }

        private async Task<TReturnType> GetResponse<TReturnType>(string method, string options, CancellationToken cancellationToken = default(CancellationToken), [CallerMemberName] string callingMethod = "") where TReturnType : MarvelBase
        {
            var fullOptions = string.Concat(options, GetHashAndTimeStamp());

            var url = string.Format("{0}/{1}?{2}", ApiUrl, method, fullOptions);

            _logger.Debug("Calling method: ", callingMethod);
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
