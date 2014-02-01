using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MarvelPortable.Model;

namespace MarvelPortable
{
    public interface IMarvelClient
    {
        HttpClient HttpClient { get; }
        string ApiKey { get; }
        string PrivateKey { get; }

        /// <summary>
        /// Gets the characters.
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
        Task<CharacterResponse> GetCharactersAsync(
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
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the character.
        /// </summary>
        /// <param name="characterId">The character identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<CharacterResponse> GetCharacterAsync(int characterId, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the comics for character.
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
        Task<ComicResponse> GetComicsForCharacterAsync(
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
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the events for character.
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
        Task<EventResponse> GetEventsForCharacterAsync(
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
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the stories for character.
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
        Task<StoryResponse> GetStoriesForCharacterAsync(
            int characterId,
            DateTime? modifiedSince = null,
            List<int> creatorIds = null,
            List<int> seriesIds = null,
            List<int> comicIds = null,
            SortBy? sortBy = null,
            OrderBy? orderBy = null,
            int? limit = null,
            int? offset = null,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the comics.
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
        Task<ComicResponse> GetComicsAsync(
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
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the comic.
        /// </summary>
        /// <param name="comicId">The comic identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<ComicResponse> GetComicAsync(int comicId, CancellationToken cancellationToken = default(CancellationToken));

        Task<CharacterResponse> GetCharactersForComicAsync(
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
            CancellationToken cancellationToken = default(CancellationToken));

        Task<CreatorResponse> GetCreatorsForComicAsync(
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
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the stories for comic.
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
        Task<StoryResponse> GetStoriesForComicAsync(
            int comicId,
            DateTime? modifiedSince = null,
            List<int> creatorIds = null,
            List<int> seriesIds = null,
            List<int> comicIds = null,
            SortBy? sortBy = null,
            OrderBy? orderBy = null,
            int? limit = null,
            int? offset = null,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the events for comic.
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
        Task<EventResponse> GetEventsForComicAsync(
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
            CancellationToken cancellationToken = default(CancellationToken));

        Task<CreatorResponse> GetCreatorsAsync(
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
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the creator.
        /// </summary>
        /// <param name="creatorId">The creator identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<CreatorResponse> GetCreatorAsync(int creatorId, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the comics for creator.
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
        Task<ComicResponse> GetComicsForCreatorAsync(
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
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the events for creator.
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
        Task<EventResponse> GetEventsForCreatorAsync(
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
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the stories for creator.
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
        Task<StoryResponse> GetStoriesForCreatorAsync(
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
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the events.
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
        Task<EventResponse> GetEventsAsync(
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
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the event.
        /// </summary>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<EventResponse> GetEventAsync(int eventId, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the characters for event.
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
        Task<CharacterResponse> GetCharactersForEventAsync(
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
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the comics for event.
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
        Task<ComicResponse> GetComicsForEventAsync(
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
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the creators for event.
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
        Task<CreatorResponse> GetCreatorsForEventAsync(
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
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the stories for event.
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
        Task<StoryResponse> GetStoriesForEventAsync(
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
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the series.
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
        Task<SeriesResponse> GetSeriesAsync(
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
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the series.
        /// </summary>
        /// <param name="seriesId">The series identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<SeriesResponse> GetSeriesAsync(int seriesId, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the characters for series.
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
        Task<CharacterResponse> GetCharactersForSeriesAsync(
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
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the comics for series.
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
        Task<ComicResponse> GetComicsForSeriesAsync(
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
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the creators for series.
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
        Task<CreatorResponse> GetCreatorsForSeriesAsync(
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
            CancellationToken cancellationToken = default(CancellationToken));

        Task<EventResponse> GetEventsForSeriesAsync(
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
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the stories for series.
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
        Task<StoryResponse> GetStoriesForSeriesAsync(
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
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the stories.
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
        Task<StoryResponse> GetStoriesAsync(
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
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the story.
        /// </summary>
        /// <param name="storyId">The story identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<StoryResponse> GetStoryAsync(int storyId, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the characters for story.
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
        Task<CharacterResponse> GetCharactersForStoryAsync(
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
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the comics for story.
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
        /// <param name="otherCharacterIds">The other character ids.</param>
        /// <param name="collaboratorIds">The collaborator ids.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<ComicResponse> GetComicsForStoryAsync(
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
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the creators for story.
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
        Task<CreatorResponse> GetCreatorsForStoryAsync(
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
            CancellationToken cancellationToken = default(CancellationToken));

        Task<EventResponse> GetEventsForStoryAsync(
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
            CancellationToken cancellationToken = default(CancellationToken));
    }
}