namespace Brightgrove.Services.Contracts.Matches
{
    /// <summary>
    /// Service to work with Matches
    /// </summary>
    public interface IMatchService : IBaseService
	{
        /// <summary>
        /// Get Matches
        /// </summary>
        /// <param name="model"><see cref="MatchesInputModel"/></param>
        /// <returns><see cref="MatchesResponse"/></returns>
        Task<MatchesResponse> GetMatches(MatchesInputModel model);
	}
}