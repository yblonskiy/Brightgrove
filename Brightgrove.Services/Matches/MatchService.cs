namespace Brightgrove.Services.Matches
{
    public class MatchService : BaseService, IMatchService
    {
		#region Private Members

		private readonly IFootballDataApiService _footballDataApiService;

		#endregion

		#region Constructor

		public MatchService(IMapper mapper,
                            ILogger<MatchService> logger,
						    IOptions<WebApiSettings> settings,
                            IFootballDataApiService footballDataApiService)
		    : base(mapper, logger, settings)
	    {
            _footballDataApiService = footballDataApiService;
        }

        #endregion

        #region IMatchService Members

        public async Task<MatchesResponse> GetMatches(MatchesInputModel model)
        {
            var result = new MatchesResponse();

            try
            {
                // Map Data
                var footballDataMatchesInputModel = Mapper.Map<FootballDataMatchesInputModel>(model);

                var matchesResponse = await _footballDataApiService.GetMatches(footballDataMatchesInputModel);

                if (matchesResponse != null && matchesResponse.IsSuccessful)
                {
                    var groupedMatches = matchesResponse.Matches
                        .GroupBy(x => x.Competition.Code)
                        .Select(s => new MatchCompetitionItem
                        {
                            Competition = Mapper.Map<MatchCompetition>(s.FirstOrDefault()?.Competition),
                            Matches     = Mapper.Map<List<MatchItem>>(s.Where(x => x.Competition.Code == s.Key))
                        })
                        .ToList();

                    result.Competitions = groupedMatches;
                }
            }
            catch (Exception ex)
            {
                ParseAndLogException(result, ex);
            }

            return result;
        }

        #endregion
    }
}