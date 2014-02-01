using System;
using System.Collections.Generic;
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
        private const string ApiUrl = "http://gateway.marvel.com/v1/public";

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
            string name = null,
            DateTime? modifiedSince = null,
            List<int> comicIds = null,
            List<int> seriesIds = null,
            List<int> eventIds = null,
            List<int> storyIds = null,
            SortBy? sortBy = null,
            OrderBy? orderBy = null,
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

            if (sortBy.HasValue)
            {
                var sort = sortBy.Value.GetDescription().ToLower();
                if (orderBy.HasValue && orderBy == OrderBy.Descending)
                {
                    sort = "-" + sort;
                }
                postData.Add("orderBy", sort);
            }

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
            var postData = new Dictionary<string, string>();
            postData.AddIfNotNull("name", eventName);
            postData.AddIfNotNull("modifiedSince", modifiedSince);
            postData.AddIfNotNull("creators", creatorIds);
            postData.AddIfNotNull("series", seriesIds);
            postData.AddIfNotNull("comics", comicIds);
            postData.AddIfNotNull("stories", storyIds);

            if (sortBy.HasValue)
            {
                var sort = sortBy.Value.GetDescription().ToLower();
                if (orderBy.HasValue && orderBy == OrderBy.Descending)
                {
                    sort = "-" + sort;
                }
                postData.Add("orderBy", sort);
            }
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
            var postData = new Dictionary<string, string>();
            postData.AddIfNotNull("modifiedSince", modifiedSince);
            postData.AddIfNotNull("creators", creatorIds);
            postData.AddIfNotNull("series", seriesIds);
            postData.AddIfNotNull("comics", comicIds);

            if (sortBy.HasValue)
            {
                var sort = sortBy.Value.GetDescription().ToLower();
                if (orderBy.HasValue && orderBy == OrderBy.Descending)
                {
                    sort = "-" + sort;
                }
                postData.Add("orderBy", sort);
            }
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

        public async Task<CharacterResponse> GetCharactersForComicAsync(
            int comicId,
            string name = null,
            DateTime? modifiedSince = null,
            List<int> seriesIds = null,
            List<int> eventIds = null,
            List<int> storyIds = null,
            SortBy? sortBy = null,
            OrderBy? orderBy = null,
            int? limit = null,
            int? offset = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var postData = new Dictionary<string, string>();
            postData.AddIfNotNull("name", name);
            postData.AddIfNotNull("modifiedSince", modifiedSince);
            postData.AddIfNotNull("series", seriesIds);
            postData.AddIfNotNull("events", eventIds);
            postData.AddIfNotNull("stories", storyIds);
            if (sortBy.HasValue)
            {
                var sort = sortBy.Value.GetDescription().ToLower();
                if (orderBy.HasValue && orderBy == OrderBy.Descending)
                {
                    sort = "-" + sort;
                }
                postData.Add("orderBy", sort);
            }
            postData.AddIfNotNull("limit", limit);
            postData.AddIfNotNull("offset", offset);

            var options = postData.ToQueryString();
            var method = string.Format("comics/{0}/characters", comicId);

            var response = await GetResponse<Response<Character>>(method, options, cancellationToken);
            return await response.Data.ToCollection<CharacterResponse>();
        }

        public async Task<CreatorResponse> GetCreatorsForComicAsync(
            int comicId,
            string firstName = null,
            string middleName = null,
            string lastName = null,
            string suffix = null,
            DateTime? modifiedSince = null,
            List<int> comicIds = null,
            List<int> seriesIds = null,
            List<int> storyIds = null,
            CreatorSortBy? sortBy = null,
            OrderBy? orderBy = null,
            int? limit = null,
            int? offset = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var postData = new Dictionary<string, string>();
            postData.AddIfNotNull("firstName", firstName);
            postData.AddIfNotNull("middleName", middleName);
            postData.AddIfNotNull("lastName", lastName);
            postData.AddIfNotNull("suffix", suffix);
            postData.AddIfNotNull("modifiedSince", modifiedSince);
            postData.AddIfNotNull("comics", comicIds);
            postData.AddIfNotNull("series", seriesIds);
            postData.AddIfNotNull("stories", storyIds);

            if (sortBy.HasValue)
            {
                var sort = sortBy.Value.GetDescription().ToLower();
                if (orderBy.HasValue && orderBy == OrderBy.Descending)
                {
                    sort = "-" + sort;
                }
                postData.Add("orderBy", sort);
            }
            postData.AddIfNotNull("limit", limit);
            postData.AddIfNotNull("offset", offset);

            var options = postData.ToQueryString();
            var method = string.Format("comics/{0}/creators", comicId);

            var response = await GetResponse<Response<Creator>>(method, options, cancellationToken);
            return await response.Data.ToCollection<CreatorResponse>();
        }

        /// <summary>
        /// Gets the stories for comic asynchronous.
        /// </summary>
        /// <param name="comicId">The comic identifier.</param>
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
        public async Task<StoryResponse> GetStoriesForComicAsync(
            int comicId,
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
            var postData = new Dictionary<string, string>();
            postData.AddIfNotNull("modifiedSince", modifiedSince);
            postData.AddIfNotNull("creators", creatorIds);
            postData.AddIfNotNull("series", seriesIds);
            postData.AddIfNotNull("comics", comicIds);

            if (sortBy.HasValue)
            {
                var sort = sortBy.Value.GetDescription().ToLower();
                if (orderBy.HasValue && orderBy == OrderBy.Descending)
                {
                    sort = "-" + sort;
                }
                postData.Add("orderBy", sort);
            }
            postData.AddIfNotNull("limit", limit);
            postData.AddIfNotNull("offset", offset);

            var options = postData.ToQueryString();
            var method = string.Format("comic/{0}/stories", comicId);

            var response = await GetResponse<Response<Story>>(method, options, cancellationToken);

            return await response.Data.ToCollection<StoryResponse>();
        }

        /// <summary>
        /// Gets the events for comic asynchronous.
        /// </summary>
        /// <param name="comicId">The comic identifier.</param>
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
        public async Task<EventResponse> GetEventsForComicAsync(
            int comicId,
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
            var postData = new Dictionary<string, string>();
            postData.AddIfNotNull("name", eventName);
            postData.AddIfNotNull("modifiedSince", modifiedSince);
            postData.AddIfNotNull("creators", creatorIds);
            postData.AddIfNotNull("series", seriesIds);
            postData.AddIfNotNull("comics", comicIds);
            postData.AddIfNotNull("stories", storyIds);

            if (sortBy.HasValue)
            {
                var sort = sortBy.Value.GetDescription().ToLower();
                if (orderBy.HasValue && orderBy == OrderBy.Descending)
                {
                    sort = "-" + sort;
                }
                postData.Add("orderBy", sort);
            }
            postData.AddIfNotNull("limit", limit);
            postData.AddIfNotNull("offset", offset);

            var options = postData.ToQueryString();
            var method = string.Format("comic/{0}/events", comicId);

            var response = await GetResponse<Response<Event>>(method, options, cancellationToken);

            return await response.Data.ToCollection<EventResponse>();
        }

        public async Task<CreatorResponse> GetCreatorsAsync(
            string firstName = null,
            string middleName = null,
            string lastName = null,
            string suffix = null,
            DateTime? modifiedSince = null,
            List<int> comicIds = null,
            List<int> seriesIds = null,
            List<int> eventIds = null,
            List<int> storyIds = null,
            CreatorSortBy? sortBy = null,
            OrderBy? orderBy = null,
            int? limit = null,
            int? offset = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var postData = new Dictionary<string, string>();
            postData.AddIfNotNull("firstName", firstName);
            postData.AddIfNotNull("middleName", middleName);
            postData.AddIfNotNull("lastName", lastName);
            postData.AddIfNotNull("suffix", suffix);
            postData.AddIfNotNull("modifiedSince", modifiedSince);
            postData.AddIfNotNull("comics", comicIds);
            postData.AddIfNotNull("series", seriesIds);
            postData.AddIfNotNull("stories", storyIds);
            if (sortBy.HasValue)
            {
                var sort = sortBy.Value.GetDescription().ToLower();
                if (orderBy.HasValue && orderBy == OrderBy.Descending)
                {
                    sort = "-" + sort;
                }
                postData.Add("orderBy", sort);
            }
            postData.AddIfNotNull("limit", limit);
            postData.AddIfNotNull("offset", offset);

            var options = postData.ToQueryString();

            var response = await GetResponse<Response<Creator>>("creators", options, cancellationToken);
            return await response.Data.ToCollection<CreatorResponse>();
        }

        /// <summary>
        /// Gets the creator asynchronous.
        /// </summary>
        /// <param name="creatorId">The creator identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<CreatorResponse> GetCreatorAsync(int creatorId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var method = string.Format("creators/{0}", creatorId);

            var response = await GetResponse<Response<Creator>>(method, string.Empty, cancellationToken);

            return await response.Data.ToCollection<CreatorResponse>();
        }

        /// <summary>
        /// Gets the comics for creator asynchronous.
        /// </summary>
        /// <param name="creatorId">The creator identifier.</param>
        /// <param name="comicFormat">The comic format.</param>
        /// <param name="comicType">Type of the comic.</param>
        /// <param name="noVariants">The no variants.</param>
        /// <param name="dateDescriptor">The date descriptor.</param>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <param name="hasDigitalIssue">The has digital issue.</param>
        /// <param name="modifiedSince">The modified since.</param>
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
        public async Task<ComicResponse> GetComicsForCreatorAsync(
            int creatorId,
            ComicFormat? comicFormat = null,
            ComicType? comicType = null,
            bool? noVariants = null,
            DateDescriptor? dateDescriptor = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            bool? hasDigitalIssue = null,
            DateTime? modifiedSince = null,
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
                null,
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
            var method = string.Format("creators/{0}/comics", creatorId);

            var response = await GetResponse<Response<Comic>>(method, options, cancellationToken);

            return await response.Data.ToCollection<ComicResponse>();
        }

        /// <summary>
        /// Gets the events for creator asynchronous.
        /// </summary>
        /// <param name="comicId">The comic identifier.</param>
        /// <param name="eventName">Name of the event.</param>
        /// <param name="modifiedSince">The modified since.</param>
        /// <param name="characterIds">The character ids.</param>
        /// <param name="seriesIds">The series ids.</param>
        /// <param name="comicIds">The comic ids.</param>
        /// <param name="storyIds">The story ids.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<EventResponse> GetEventsForCreatorAsync(
            int comicId,
            string eventName = null,
            DateTime? modifiedSince = null,
            List<int> characterIds = null,
            List<int> seriesIds = null,
            List<int> comicIds = null,
            List<int> storyIds = null,
            SortBy? sortBy = null,
            OrderBy? orderBy = null,
            int? limit = null,
            int? offset = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var postData = new Dictionary<string, string>();
            postData.AddIfNotNull("name", eventName);
            postData.AddIfNotNull("modifiedSince", modifiedSince);
            postData.AddIfNotNull("characters", characterIds);
            postData.AddIfNotNull("series", seriesIds);
            postData.AddIfNotNull("comics", comicIds);
            postData.AddIfNotNull("stories", storyIds);

            if (sortBy.HasValue)
            {
                var sort = sortBy.Value.GetDescription().ToLower();
                if (orderBy.HasValue && orderBy == OrderBy.Descending)
                {
                    sort = "-" + sort;
                }
                postData.Add("orderBy", sort);
            }
            postData.AddIfNotNull("limit", limit);
            postData.AddIfNotNull("offset", offset);

            var options = postData.ToQueryString();
            var method = string.Format("creator/{0}/events", comicId);

            var response = await GetResponse<Response<Event>>(method, options, cancellationToken);

            return await response.Data.ToCollection<EventResponse>();
        }

        /// <summary>
        /// Gets the stories for creator asynchronous.
        /// </summary>
        /// <param name="creatorId">The creator identifier.</param>
        /// <param name="modifiedSince">The modified since.</param>
        /// <param name="comicIds">The comic ids.</param>
        /// <param name="seriesIds">The series ids.</param>
        /// <param name="eventIds">The event ids.</param>
        /// <param name="characterIds">The character ids.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<StoryResponse> GetStoriesForCreatorAsync(
            int creatorId,
            DateTime? modifiedSince = null,
            List<int> comicIds = null,
            List<int> seriesIds = null,
            List<int> eventIds = null,
            List<int> characterIds = null,
            SortBy? sortBy = null,
            OrderBy? orderBy = null,
            int? limit = null,
            int? offset = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var postData = new Dictionary<string, string>();
            postData.AddIfNotNull("modifiedSince", modifiedSince);
            postData.AddIfNotNull("creators", eventIds);
            postData.AddIfNotNull("series", seriesIds);
            postData.AddIfNotNull("comics", comicIds);
            postData.AddIfNotNull("characters", characterIds);

            if (sortBy.HasValue)
            {
                var sort = sortBy.Value.GetDescription().ToLower();
                if (orderBy.HasValue && orderBy == OrderBy.Descending)
                {
                    sort = "-" + sort;
                }
                postData.Add("orderBy", sort);
            }
            postData.AddIfNotNull("limit", limit);
            postData.AddIfNotNull("offset", offset);

            var options = postData.ToQueryString();
            var method = string.Format("creators/{0}/stories", creatorId);

            var response = await GetResponse<Response<Story>>(method, options, cancellationToken);

            return await response.Data.ToCollection<StoryResponse>();
        }

        /// <summary>
        /// Gets the events asynchronous.
        /// </summary>
        /// <param name="eventName">Name of the event.</param>
        /// <param name="modifiedSince">The modified since.</param>
        /// <param name="creatorIds">The creator ids.</param>
        /// <param name="characterIds">The character ids.</param>
        /// <param name="seriesIds">The series ids.</param>
        /// <param name="comicIds">The comic ids.</param>
        /// <param name="storyIds">The story ids.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<EventResponse> GetEventsAsync(
            string eventName = null,
            DateTime? modifiedSince = null,
            List<int> creatorIds = null,
            List<int> characterIds = null,
            List<int> seriesIds = null,
            List<int> comicIds = null,
            List<int> storyIds = null,
            SortBy? sortBy = null,
            OrderBy? orderBy = null,
            int? limit = null,
            int? offset = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var postData = new Dictionary<string, string>();
            postData.AddIfNotNull("name", eventName);
            postData.AddIfNotNull("modifiedSince", modifiedSince);
            postData.AddIfNotNull("creators", creatorIds);
            postData.AddIfNotNull("characters", characterIds);
            postData.AddIfNotNull("series", seriesIds);
            postData.AddIfNotNull("comics", comicIds);
            postData.AddIfNotNull("stories", storyIds);

            if (sortBy.HasValue)
            {
                var sort = sortBy.Value.GetDescription().ToLower();
                if (orderBy.HasValue && orderBy == OrderBy.Descending)
                {
                    sort = "-" + sort;
                }
                postData.Add("orderBy", sort);
            }
            postData.AddIfNotNull("limit", limit);
            postData.AddIfNotNull("offset", offset);

            var options = postData.ToQueryString();

            var response = await GetResponse<Response<Event>>("events", options, cancellationToken);

            return await response.Data.ToCollection<EventResponse>();
        }

        /// <summary>
        /// Gets the event asynchronous.
        /// </summary>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<EventResponse> GetEventAsync(int eventId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var method = string.Format("events/{0}", eventId);

            var response = await GetResponse<Response<Event>>(method, string.Empty, cancellationToken);

            return await response.Data.ToCollection<EventResponse>();
        }

        /// <summary>
        /// Gets the characters for event asynchronous.
        /// </summary>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="modifiedSince">The modified since.</param>
        /// <param name="seriesIds">The series ids.</param>
        /// <param name="comicIds">The comic ids.</param>
        /// <param name="storyIds">The story ids.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<CharacterResponse> GetCharactersForEventAsync(
            int eventId,
            string name = null,
            DateTime? modifiedSince = null,
            List<int> seriesIds = null,
            List<int> comicIds = null,
            List<int> storyIds = null,
            SortBy? sortBy = null,
            OrderBy? orderBy = null,
            int? limit = null,
            int? offset = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var postData = new Dictionary<string, string>();
            postData.AddIfNotNull("name", name);
            postData.AddIfNotNull("modifiedSince", modifiedSince);
            postData.AddIfNotNull("series", seriesIds);
            postData.AddIfNotNull("comics", comicIds);
            postData.AddIfNotNull("stories", storyIds);
            if (sortBy.HasValue)
            {
                var sort = sortBy.Value.GetDescription().ToLower();
                if (orderBy.HasValue && orderBy == OrderBy.Descending)
                {
                    sort = "-" + sort;
                }
                postData.Add("orderBy", sort);
            }
            postData.AddIfNotNull("limit", limit);
            postData.AddIfNotNull("offset", offset);

            var options = postData.ToQueryString();
            var method = string.Format("events/{0}/characters", eventId);

            var response = await GetResponse<Response<Character>>(method, options, cancellationToken);
            return await response.Data.ToCollection<CharacterResponse>();
        }

        /// <summary>
        /// Gets the comics for event asynchronous.
        /// </summary>
        /// <param name="eventId">The event identifier.</param>
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
        public async Task<ComicResponse> GetComicsForEventAsync(
            int eventId,
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
            var method = string.Format("events/{0}/comics", eventId);

            var response = await GetResponse<Response<Comic>>(method, options, cancellationToken);

            return await response.Data.ToCollection<ComicResponse>();
        }

        /// <summary>
        /// Gets the creators for event asynchronous.
        /// </summary>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="middleName">Name of the middle.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="suffix">The suffix.</param>
        /// <param name="modifiedSince">The modified since.</param>
        /// <param name="comicIds">The comic ids.</param>
        /// <param name="seriesIds">The series ids.</param>
        /// <param name="storyIds">The story ids.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<CreatorResponse> GetCreatorsForEventAsync(
            int eventId,
            string firstName = null,
            string middleName = null,
            string lastName = null,
            string suffix = null,
            DateTime? modifiedSince = null,
            List<int> comicIds = null,
            List<int> seriesIds = null,
            List<int> storyIds = null,
            CreatorSortBy? sortBy = null,
            OrderBy? orderBy = null,
            int? limit = null,
            int? offset = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var postData = new Dictionary<string, string>();
            postData.AddIfNotNull("firstName", firstName);
            postData.AddIfNotNull("middleName", middleName);
            postData.AddIfNotNull("lastName", lastName);
            postData.AddIfNotNull("suffix", suffix);
            postData.AddIfNotNull("modifiedSince", modifiedSince);
            postData.AddIfNotNull("comics", comicIds);
            postData.AddIfNotNull("series", seriesIds);
            postData.AddIfNotNull("stories", storyIds);
            if (sortBy.HasValue)
            {
                var sort = sortBy.Value.GetDescription().ToLower();
                if (orderBy.HasValue && orderBy == OrderBy.Descending)
                {
                    sort = "-" + sort;
                }
                postData.Add("orderBy", sort);
            }
            postData.AddIfNotNull("limit", limit);
            postData.AddIfNotNull("offset", offset);

            var options = postData.ToQueryString();
            var method = string.Format("events/{0}/creators", eventId);

            var response = await GetResponse<Response<Creator>>(method, options, cancellationToken);
            return await response.Data.ToCollection<CreatorResponse>();
        }

        /// <summary>
        /// Gets the stories for event asynchronous.
        /// </summary>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="modifiedSince">The modified since.</param>
        /// <param name="creatorIds">The creator ids.</param>
        /// <param name="seriesIds">The series ids.</param>
        /// <param name="comicIds">The comic ids.</param>
        /// <param name="characterIds">The character ids.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<StoryResponse> GetStoriesForEventAsync(
            int eventId,
            DateTime? modifiedSince = null,
            List<int> creatorIds = null,
            List<int> seriesIds = null,
            List<int> comicIds = null,
            List<int> characterIds = null,
            SortBy? sortBy = null,
            OrderBy? orderBy = null,
            int? limit = null,
            int? offset = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var postData = new Dictionary<string, string>();
            postData.AddIfNotNull("modifiedSince", modifiedSince);
            postData.AddIfNotNull("creators", creatorIds);
            postData.AddIfNotNull("series", seriesIds);
            postData.AddIfNotNull("comics", comicIds);
            postData.AddIfNotNull("characters", characterIds);

            if (sortBy.HasValue)
            {
                var sort = sortBy.Value.GetDescription().ToLower();
                if (orderBy.HasValue && orderBy == OrderBy.Descending)
                {
                    sort = "-" + sort;
                }
                postData.Add("orderBy", sort);
            }
            postData.AddIfNotNull("limit", limit);
            postData.AddIfNotNull("offset", offset);

            var options = postData.ToQueryString();
            var method = string.Format("comic/{0}/stories", eventId);

            var response = await GetResponse<Response<Story>>(method, options, cancellationToken);

            return await response.Data.ToCollection<StoryResponse>();
        }

        /// <summary>
        /// Gets the series asynchronous.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="modifiedSince">The modified since.</param>
        /// <param name="comicIds">The comic ids.</param>
        /// <param name="storyIds">The story ids.</param>
        /// <param name="eventIds">The event ids.</param>
        /// <param name="creatorIds">The creator ids.</param>
        /// <param name="characterIds">The character ids.</param>
        /// <param name="seriesType">Type of the series.</param>
        /// <param name="formats">The formats.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<SeriesResponse> GetSeriesAsync(
            string title = null,
            DateTime? modifiedSince = null,
            List<int> comicIds = null,
            List<int> storyIds = null,
            List<int> eventIds = null,
            List<int> creatorIds = null,
            List<int> characterIds = null,
            SeriesType? seriesType = null,
            List<ComicFormat> formats = null,
            SortBy? sortBy = null,
            OrderBy? orderBy = null,
            int? limit = null,
            int? offset = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var postData = new Dictionary<string, string>();
            postData.AddIfNotNull("title", title);
            postData.AddIfNotNull("modifiedSince", modifiedSince);
            postData.AddIfNotNull("comics", comicIds);
            postData.AddIfNotNull("stories", storyIds);
            postData.AddIfNotNull("events", eventIds);
            postData.AddIfNotNull("creators", creatorIds);
            postData.AddIfNotNull("characters", characterIds);
            postData.AddIfNotNull("seriesType", seriesType);
            postData.AddIfNotNull("contains", formats);
            if (sortBy.HasValue)
            {
                var sort = sortBy.Value.GetDescription().ToLower();
                if (orderBy.HasValue && orderBy == OrderBy.Descending)
                {
                    sort = "-" + sort;
                }
                postData.Add("orderBy", sort);
            }
            postData.AddIfNotNull("limit", limit);
            postData.AddIfNotNull("offset", offset);

            var options = postData.ToQueryString();

            var response = await GetResponse<Response<Series>>("series", options, cancellationToken);

            return await response.Data.ToCollection<SeriesResponse>();
        }

        /// <summary>
        /// Gets the series asynchronous.
        /// </summary>
        /// <param name="seriesId">The series identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<SeriesResponse> GetSeriesAsync(int seriesId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var method = string.Format("series/{0}", seriesId);

            var response = await GetResponse<Response<Series>>(method, string.Empty, cancellationToken);
            return await response.Data.ToCollection<SeriesResponse>();
        }

        /// <summary>
        /// Gets the characters for series asynchronous.
        /// </summary>
        /// <param name="seriesId">The series identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="modifiedSince">The modified since.</param>
        /// <param name="comicIds">The comic ids.</param>
        /// <param name="eventIds">The event ids.</param>
        /// <param name="storyIds">The story ids.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<CharacterResponse> GetCharactersForSeriesAsync(
            int seriesId,
            string name = null,
            DateTime? modifiedSince = null,
            List<int> comicIds = null,
            List<int> eventIds = null,
            List<int> storyIds = null,
            SortBy? sortBy = null,
            OrderBy? orderBy = null,
            int? limit = null,
            int? offset = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var postData = new Dictionary<string, string>();
            postData.AddIfNotNull("name", name);
            postData.AddIfNotNull("modifiedSince", modifiedSince);
            postData.AddIfNotNull("comics", comicIds);
            postData.AddIfNotNull("events", eventIds);
            postData.AddIfNotNull("stories", storyIds);
            if (sortBy.HasValue)
            {
                var sort = sortBy.Value.GetDescription().ToLower();
                if (orderBy.HasValue && orderBy == OrderBy.Descending)
                {
                    sort = "-" + sort;
                }
                postData.Add("orderBy", sort);
            }
            postData.AddIfNotNull("limit", limit);
            postData.AddIfNotNull("offset", offset);

            var options = postData.ToQueryString();
            var method = string.Format("series/{0}/characters", seriesId);

            var response = await GetResponse<Response<Character>>(method, options, cancellationToken);
            return await response.Data.ToCollection<CharacterResponse>();
        }

        /// <summary>
        /// Gets the comics for series asynchronous.
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
        public async Task<ComicResponse> GetComicsForSeriesAsync(
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
                null,
                eventIds,
                storyIds,
                otherCharacterIds,
                collaboratorIds,
                sortBy,
                orderBy,
                limit,
                offset);

            var options = postData.ToQueryString();
            var method = string.Format("series/{0}/comics", characterId);

            var response = await GetResponse<Response<Comic>>(method, options, cancellationToken);

            return await response.Data.ToCollection<ComicResponse>();
        }

        /// <summary>
        /// Gets the creators for series asynchronous.
        /// </summary>
        /// <param name="seriesId">The series identifier.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="middleName">Name of the middle.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="suffix">The suffix.</param>
        /// <param name="modifiedSince">The modified since.</param>
        /// <param name="comicIds">The comic ids.</param>
        /// <param name="eventIds">The event ids.</param>
        /// <param name="storyIds">The story ids.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<CreatorResponse> GetCreatorsForSeriesAsync(
            int seriesId,
            string firstName = null,
            string middleName = null,
            string lastName = null,
            string suffix = null,
            DateTime? modifiedSince = null,
            List<int> comicIds = null,
            List<int> eventIds = null,
            List<int> storyIds = null,
            CreatorSortBy? sortBy = null,
            OrderBy? orderBy = null,
            int? limit = null,
            int? offset = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var postData = new Dictionary<string, string>();
            postData.AddIfNotNull("firstName", firstName);
            postData.AddIfNotNull("middleName", middleName);
            postData.AddIfNotNull("lastName", lastName);
            postData.AddIfNotNull("suffix", suffix);
            postData.AddIfNotNull("modifiedSince", modifiedSince);
            postData.AddIfNotNull("comics", comicIds);
            postData.AddIfNotNull("stories", storyIds);
            postData.AddIfNotNull("events", eventIds);
            if (sortBy.HasValue)
            {
                var sort = sortBy.Value.GetDescription().ToLower();
                if (orderBy.HasValue && orderBy == OrderBy.Descending)
                {
                    sort = "-" + sort;
                }
                postData.Add("orderBy", sort);
            }
            postData.AddIfNotNull("limit", limit);
            postData.AddIfNotNull("offset", offset);

            var options = postData.ToQueryString();
            var method = string.Format("series/{0}/creators", seriesId);

            var response = await GetResponse<Response<Creator>>(method, options, cancellationToken);
            return await response.Data.ToCollection<CreatorResponse>();
        }

        public async Task<EventResponse> GetEventsForSeriesAsync(
            int seriesId,
            string eventName = null,
            DateTime? modifiedSince = null,
            List<int> creatorIds = null,
            List<int> characterIds = null,
            List<int> comicIds = null,
            List<int> storyIds = null,
            SortBy? sortBy = null,
            OrderBy? orderBy = null,
            int? limit = null,
            int? offset = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var postData = new Dictionary<string, string>();
            postData.AddIfNotNull("name", eventName);
            postData.AddIfNotNull("modifiedSince", modifiedSince);
            postData.AddIfNotNull("creators", creatorIds);
            postData.AddIfNotNull("characters", characterIds);
            postData.AddIfNotNull("comics", comicIds);
            postData.AddIfNotNull("stories", storyIds);

            if (sortBy.HasValue)
            {
                var sort = sortBy.Value.GetDescription().ToLower();
                if (orderBy.HasValue && orderBy == OrderBy.Descending)
                {
                    sort = "-" + sort;
                }
                postData.Add("orderBy", sort);
            }
            postData.AddIfNotNull("limit", limit);
            postData.AddIfNotNull("offset", offset);

            var options = postData.ToQueryString();
            var method = string.Format("series/{0}/events", seriesId);

            var response = await GetResponse<Response<Event>>(method, options, cancellationToken);

            return await response.Data.ToCollection<EventResponse>();
        }

        /// <summary>
        /// Gets the stories for series asynchronous.
        /// </summary>
        /// <param name="seriesId">The series identifier.</param>
        /// <param name="modifiedSince">The modified since.</param>
        /// <param name="creatorIds">The creator ids.</param>
        /// <param name="characterIds">The character ids.</param>
        /// <param name="comicIds">The comic ids.</param>
        /// <param name="eventIds">The event ids.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<StoryResponse> GetStoriesForSeriesAsync(
            int seriesId,
            DateTime? modifiedSince = null,
            List<int> creatorIds = null,
            List<int> characterIds = null,
            List<int> comicIds = null,
            List<int> eventIds = null,
            SortBy? sortBy = null,
            OrderBy? orderBy = null,
            int? limit = null,
            int? offset = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var postData = new Dictionary<string, string>();
            postData.AddIfNotNull("modifiedSince", modifiedSince);
            postData.AddIfNotNull("creators", creatorIds);
            postData.AddIfNotNull("characters", characterIds);
            postData.AddIfNotNull("comics", comicIds);
            postData.AddIfNotNull("events", eventIds);

            if (sortBy.HasValue)
            {
                var sort = sortBy.Value.GetDescription().ToLower();
                if (orderBy.HasValue && orderBy == OrderBy.Descending)
                {
                    sort = "-" + sort;
                }
                postData.Add("orderBy", sort);
            }
            postData.AddIfNotNull("limit", limit);
            postData.AddIfNotNull("offset", offset);

            var options = postData.ToQueryString();
            var method = string.Format("characters/{0}/stories", seriesId);

            var response = await GetResponse<Response<Story>>(method, options, cancellationToken);

            return await response.Data.ToCollection<StoryResponse>();
        }

        /// <summary>
        /// Gets the stories asynchronous.
        /// </summary>
        /// <param name="modifiedSince">The modified since.</param>
        /// <param name="creatorIds">The creator ids.</param>
        /// <param name="characterIds">The character ids.</param>
        /// <param name="seriesIds">The series ids.</param>
        /// <param name="comicIds">The comic ids.</param>
        /// <param name="storyIds">The story ids.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<StoryResponse> GetStoriesAsync(
            DateTime? modifiedSince = null,
            List<int> creatorIds = null,
            List<int> characterIds = null,
            List<int> seriesIds = null,
            List<int> comicIds = null,
            List<int> storyIds = null,
            SortBy? sortBy = null,
            OrderBy? orderBy = null,
            int? limit = null,
            int? offset = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var postData = new Dictionary<string, string>();
            postData.AddIfNotNull("modifiedSince", modifiedSince);
            postData.AddIfNotNull("creators", creatorIds);
            postData.AddIfNotNull("characters", characterIds);
            postData.AddIfNotNull("series", seriesIds);
            postData.AddIfNotNull("comics", comicIds);
            postData.AddIfNotNull("stories", storyIds);

            if (sortBy.HasValue)
            {
                var sort = sortBy.Value.GetDescription().ToLower();
                if (orderBy.HasValue && orderBy == OrderBy.Descending)
                {
                    sort = "-" + sort;
                }
                postData.Add("orderBy", sort);
            }
            postData.AddIfNotNull("limit", limit);
            postData.AddIfNotNull("offset", offset);

            var options = postData.ToQueryString();

            var response = await GetResponse<Response<Story>>("stories", options, cancellationToken);
            return await response.Data.ToCollection<StoryResponse>();
        }

        /// <summary>
        /// Gets the story asynchronous.
        /// </summary>
        /// <param name="storyId">The story identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<StoryResponse> GetStoryAsync(int storyId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var method = string.Format("stories/{0}", storyId);
            var response = await GetResponse<Response<Story>>(method, string.Empty, cancellationToken);
            return await response.Data.ToCollection<StoryResponse>();
        }

        /// <summary>
        /// Gets the characters for story asynchronous.
        /// </summary>
        /// <param name="storyId">The story identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="modifiedSince">The modified since.</param>
        /// <param name="seriesIds">The series ids.</param>
        /// <param name="eventIds">The event ids.</param>
        /// <param name="comicIds">The comic ids.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<CharacterResponse> GetCharactersForStoryAsync(
            int storyId,
            string name = null,
            DateTime? modifiedSince = null,
            List<int> seriesIds = null,
            List<int> eventIds = null,
            List<int> comicIds = null,
            SortBy? sortBy = null,
            OrderBy? orderBy = null,
            int? limit = null,
            int? offset = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var postData = new Dictionary<string, string>();
            postData.AddIfNotNull("name", name);
            postData.AddIfNotNull("modifiedSince", modifiedSince);
            postData.AddIfNotNull("series", seriesIds);
            postData.AddIfNotNull("events", eventIds);
            postData.AddIfNotNull("comics", comicIds);
            if (sortBy.HasValue)
            {
                var sort = sortBy.Value.GetDescription().ToLower();
                if (orderBy.HasValue && orderBy == OrderBy.Descending)
                {
                    sort = "-" + sort;
                }
                postData.Add("orderBy", sort);
            }
            postData.AddIfNotNull("limit", limit);
            postData.AddIfNotNull("offset", offset);

            var options = postData.ToQueryString();
            var method = string.Format("stories/{0}/characters", storyId);

            var response = await GetResponse<Response<Character>>(method, options, cancellationToken);
            return await response.Data.ToCollection<CharacterResponse>();
        }

        /// <summary>
        /// Gets the comics for story asynchronous.
        /// </summary>
        /// <param name="storyId">The character identifier.</param>
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
        public async Task<ComicResponse> GetComicsForStoryAsync(
            int storyId,
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
                null,
                otherCharacterIds,
                collaboratorIds,
                sortBy,
                orderBy,
                limit,
                offset);

            var options = postData.ToQueryString();
            var method = string.Format("stories/{0}/comics", storyId);

            var response = await GetResponse<Response<Comic>>(method, options, cancellationToken);

            return await response.Data.ToCollection<ComicResponse>();
        }

        /// <summary>
        /// Gets the creators for story asynchronous.
        /// </summary>
        /// <param name="storyId">The story identifier.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="middleName">Name of the middle.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="suffix">The suffix.</param>
        /// <param name="modifiedSince">The modified since.</param>
        /// <param name="comicIds">The comic ids.</param>
        /// <param name="seriesIds">The series ids.</param>
        /// <param name="eventsIds">The events ids.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<CreatorResponse> GetCreatorsForStoryAsync(
            int storyId,
            string firstName = null,
            string middleName = null,
            string lastName = null,
            string suffix = null,
            DateTime? modifiedSince = null,
            List<int> comicIds = null,
            List<int> seriesIds = null,
            List<int> eventsIds = null,
            CreatorSortBy? sortBy = null,
            OrderBy? orderBy = null,
            int? limit = null,
            int? offset = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var postData = new Dictionary<string, string>();
            postData.AddIfNotNull("firstName", firstName);
            postData.AddIfNotNull("middleName", middleName);
            postData.AddIfNotNull("lastName", lastName);
            postData.AddIfNotNull("suffix", suffix);
            postData.AddIfNotNull("modifiedSince", modifiedSince);
            postData.AddIfNotNull("comics", comicIds);
            postData.AddIfNotNull("series", seriesIds);
            postData.AddIfNotNull("events", eventsIds);

            if (sortBy.HasValue)
            {
                var sort = sortBy.Value.GetDescription().ToLower();
                if (orderBy.HasValue && orderBy == OrderBy.Descending)
                {
                    sort = "-" + sort;
                }
                postData.Add("orderBy", sort);
            }
            postData.AddIfNotNull("limit", limit);
            postData.AddIfNotNull("offset", offset);

            var options = postData.ToQueryString();
            var method = string.Format("stories/{0}/creators", storyId);

            var response = await GetResponse<Response<Creator>>(method, options, cancellationToken);
            return await response.Data.ToCollection<CreatorResponse>();
        }

        public async Task<EventResponse> GetEventsForStoryAsync(
            int storyId,
            string eventName = null,
            DateTime? modifiedSince = null,
            List<int> creatorIds = null,
            List<int> seriesIds = null,
            List<int> comicIds = null,
            List<int> characterIds = null,
            SortBy? sortBy = null,
            OrderBy? orderBy = null,
            int? limit = null,
            int? offset = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var postData = new Dictionary<string, string>();
            postData.AddIfNotNull("name", eventName);
            postData.AddIfNotNull("modifiedSince", modifiedSince);
            postData.AddIfNotNull("creators", creatorIds);
            postData.AddIfNotNull("series", seriesIds);
            postData.AddIfNotNull("comics", comicIds);
            postData.AddIfNotNull("characters", characterIds);

            if (sortBy.HasValue)
            {
                var sort = sortBy.Value.GetDescription().ToLower();
                if (orderBy.HasValue && orderBy == OrderBy.Descending)
                {
                    sort = "-" + sort;
                }
                postData.Add("orderBy", sort);
            }
            postData.AddIfNotNull("limit", limit);
            postData.AddIfNotNull("offset", offset);

            var options = postData.ToQueryString();
            var method = string.Format("stories/{0}/events", storyId);

            var response = await GetResponse<Response<Event>>(method, options, cancellationToken);

            return await response.Data.ToCollection<EventResponse>();
        }


        #region Internal methods
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
            postData.AddIfNotNull("formatType", comicType);
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

            if (sortBy.HasValue)
            {
                var sort = sortBy.Value.GetDescription().ToLower();
                if (orderBy.HasValue && orderBy == OrderBy.Descending)
                {
                    sort = "-" + sort;
                }
                postData.Add("orderBy", sort);
            }
            postData.AddIfNotNull("limit", limit);
            postData.AddIfNotNull("offset", offset);

            return postData;
        }
        #endregion

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
