namespace Brightgrove.IntegrationServices.FootballData
{
    public class FootballDataApiService : IFootballDataApiService
    {
        #region Private Members

        private readonly ILogger _logger;
        private readonly WebApiSettings _appSettings;

        #endregion

        #region Constructors

        public FootballDataApiService(ILogger<FootballDataApiService> logger,
                                      IOptions<WebApiSettings> settings)
        {
            _logger			= logger;
            _appSettings	= settings.Value;
        }

        #endregion

        #region IHubSpotCrmApiService Members

        public async Task<FootballDataMatchesResponse> GetMatches(FootballDataMatchesInputModel model)
        {
            if (_logger.IsEnabled(LogLevel.Debug))
                _logger.LogDebug("Calling FootballDataApiService::GetMatches");

            using (var client = GetHttpClientInstance())
            {
                try
                {
                    var response = await client.GetAsync($"v4/matches?{model.ToGetRequestString()}");

                    // Call successful - read returned data
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();

                        if (_logger.IsEnabled(LogLevel.Debug))
                            _logger.LogDebug($"Response : {json}");

                        var responseModel = JsonConvert.DeserializeObject<FootballDataMatchesResponse>(json);

                        if (responseModel != null && responseModel.ResultSet != null && responseModel.Matches != null && responseModel.Matches.Count > 0)
                            responseModel.IsSuccessful = true;

                        return responseModel;
                    }

                    // Call failed - read underlying error as a string
                    var errorJson = await response.Content.ReadAsStringAsync();

                    var errorResponseModel = new FootballDataMatchesResponse
                    {
                        IsSuccessful = false,
                        ErrorMessage = $"Status code {(int)response.StatusCode} : {response.ReasonPhrase}, Response : {errorJson}"
                    };

                    _logger.LogError(errorResponseModel.ErrorMessage);

                    return errorResponseModel;
                }
                catch (HttpRequestException ex)
                {
                    return new FootballDataMatchesResponse
                    {
                        IsSuccessful = false,
                        ErrorMessage = ParseHttpRequestException(ex)
                    };
                }
            }
        }

        #endregion

        #region Private Methods

        private HttpClient GetHttpClientInstance()
		{
			var client = new HttpClient
			{
				Timeout		= TimeSpan.FromMinutes(_appSettings.FootballDataSettings.WebApiClientTimeout),
				BaseAddress = new Uri(_appSettings.FootballDataSettings.BaseUrl)
			};

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Set token if it was set
            if (!string.IsNullOrEmpty(_appSettings.FootballDataSettings.Token))
                client.DefaultRequestHeaders.Add("X-Auth-Token", _appSettings.FootballDataSettings.Token);

			return client;
		}

		/// <summary>
		/// Extract error messages from <see cref="HttpRequestException"/>
		/// </summary>
		/// <param name="exception"><see cref="HttpRequestException"/></param>
		/// <returns>Full error message</returns>
		private string ParseHttpRequestException(HttpRequestException exception)
		{
			if (exception == null)
				return string.Empty;

			var errorMessage = exception.Message;

			_logger.LogError(exception, exception.Message);

			if (exception.InnerException == null)
				return errorMessage;

			errorMessage += Environment.NewLine + exception.InnerException.Message;

			_logger.LogError(exception.InnerException, exception.InnerException.Message);

			if (exception.InnerException.InnerException == null)
				return errorMessage;

			errorMessage += Environment.NewLine + exception.InnerException.InnerException.Message;

			_logger.LogError(exception.InnerException.InnerException, exception.InnerException.InnerException.Message);

			return errorMessage;
		}

		#endregion
	}
}