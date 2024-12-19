namespace Brightgrove.Services.WebApi.Controllers.Competitions
{
    /// <summary>
    /// Matches Records API
    /// </summary>
	[Produces("application/json")]
	[Route("api/v1/matches")]
	public class MatchController : BaseApiController
	{
		#region Private Members

		private readonly IMatchService _matchService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="settings"></param>
        /// <param name="matchService"></param>
        public MatchController(ILogger<MatchController> logger,
							   IOptions<WebApiSettings> settings,
                               IMatchService matchService)
			: base(logger, settings)
		{
            _matchService = matchService;
		}

        #endregion

        /// <summary>
        /// Get Matches
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(MatchesResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponseError), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MatchesResponse>> GetMatches(MatchesInputModel model)
        {
            if (model == null)
                return BadRequest(400, "Incorrect input parameter [model]");

            return await ExecuteWithOkResponseAsync(async () => await _matchService.GetMatches(model));
        }
	}
}