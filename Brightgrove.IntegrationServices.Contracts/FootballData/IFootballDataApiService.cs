namespace Brightgrove.IntegrationServices.Contracts.FootballData
{
    /// <summary>
    /// Helper class to work with Football Data API
    /// </summary>
	public interface IFootballDataApiService
    {
        /// <summary>
        /// Get Matches
        /// </summary>
        /// <param name="model"><see cref="FootballDataMatchesInputModel"/></param>
        /// <returns><see cref="FootballDataMatchesResponse"/></returns>
        Task<FootballDataMatchesResponse> GetMatches(FootballDataMatchesInputModel model);
    }
}