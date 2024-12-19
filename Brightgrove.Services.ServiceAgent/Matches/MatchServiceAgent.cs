namespace Brightgrove.Services.ServiceAgent.Matches
{
    public class MatchServiceAgent : BaseServiceAgent, IMatchService
    {
		#region Constructor

		public MatchServiceAgent(ILogger<MatchServiceAgent> logger, IOptions<WebApiServiceAgentSettings> settings, IHttpClientFactory httpClientFactory) : base(logger, settings, httpClientFactory)
        {
		}

        #endregion

        #region IMatchService Members

        public async Task<MatchesResponse> GetMatches(MatchesInputModel model)
        {
            return await PostAsync<MatchesInputModel, MatchesResponse>($"matches", model);
        }

        #endregion
    }
}