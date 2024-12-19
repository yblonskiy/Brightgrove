namespace Brightgrove.Services.ServiceAgent
{
    /// <summary>
    /// Base class for Core Service Agents
    /// </summary>
    public abstract class BaseServiceAgent : IBaseService
	{
        #region Private Members

        private readonly IHttpClientFactory _httpClientFactory;

        #endregion

        #region Protected Members

        /// <summary>
        /// REST Service Base URL
        /// </summary>
        protected string RestServicesBaseUrl { get; private set; }

		/// <summary>
		/// Timeout in minutes for REST Services
		/// </summary>
		protected int RestServicesTimeoutMinutes { get; private set; }

		/// <summary>
		/// <see cref="WebApiServiceAgentSettings"/>
		/// </summary>
		protected WebApiServiceAgentSettings ServiceAgentSettings { get; set; }

		/// <summary>
		/// <see cref="ILogger"/>
		/// </summary>
		protected ILogger Logger { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Protected Constructor
        /// </summary>
        /// <param name="logger"><see cref="ILogger"/></param>
        /// <param name="settings"><see cref="IOptions{WebApiServiceAgentSettings}"/></param>
        /// <param name="httpClientFactory"><see cref="IHttpClientFactory"/></param>
        protected BaseServiceAgent(ILogger<BaseServiceAgent> logger, IOptions<WebApiServiceAgentSettings> settings, IHttpClientFactory httpClientFactory)
		{
			Logger					= logger;
			ServiceAgentSettings	= settings.Value;
			_httpClientFactory		= httpClientFactory;
        }

		#endregion

        #region Protected Methods

        /// <summary>
        /// Get instance of <see cref="HttpClient"/>
        /// </summary>
        /// <returns></returns>
        protected HttpClient GetHttpClientInstance()
		{
            var serviceUri = ServiceAgentSettings.WebApiHost.Trim();

            RestServicesBaseUrl			= serviceUri.EndsWith("/") ? serviceUri : $"{serviceUri}/";
            RestServicesTimeoutMinutes	= ServiceAgentSettings.WebApiTimeout;
			
            var client = _httpClientFactory.CreateClient();

            client.Timeout		= TimeSpan.FromMinutes(RestServicesTimeoutMinutes);
            client.BaseAddress	= new Uri(RestServicesBaseUrl);

			// Add an Accept header for JSON format.
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
		}

	    /// <summary>
		/// Generic Get Async Method
		/// </summary>
		/// <typeparam name="T">Output Model Type</typeparam>
		/// <param name="requestUri">Request URI</param>
		/// <returns>Output Model</returns>
		protected async Task<T> GetAsync<T>(string requestUri)
		{
			if (Logger.IsEnabled(LogLevel.Debug))
				Logger.LogDebug($"Calling HTTP GET : {RestServicesBaseUrl}{requestUri}");

			using (var client = GetHttpClientInstance())
			{
				try
				{
					var response = await client.GetAsync(requestUri);

					// Call successful - read returned data
					if (response.IsSuccessStatusCode)
					{
						var json = await response.Content.ReadAsStringAsync();

						return JsonConvert.DeserializeObject<T>(json);
					}

					// Call failed - read underlying error as a string
					var errorMessage = await response.Content.ReadAsStringAsync();

					if (string.IsNullOrWhiteSpace(errorMessage))
						errorMessage = $"Status code {(int)response.StatusCode} : {response.ReasonPhrase}";

					Logger.LogError(errorMessage);

					// Throw exception
					throw new InvalidOperationException(errorMessage);
				}
				catch (HttpRequestException ex)
				{
					throw new InvalidOperationException(ParseHttpRequestException(ex));
				}
			}
		}

		/// <summary>
		/// Generic Get Sync Method
		/// </summary>
		/// <typeparam name="T">Output Model Type</typeparam>
		/// <param name="requestUri">Request URI</param>
		/// <returns>Output Model</returns>
		protected T GetSync<T>(string requestUri)
		{
			if (Logger.IsEnabled(LogLevel.Debug))
				Logger.LogDebug($"Calling HTTP GET : {RestServicesBaseUrl}{requestUri}");

			using (var client = GetHttpClientInstance())
			{
				try
				{
					var response = client.GetAsync(requestUri).Result;

					// Call successful - read returned data
					if (response.IsSuccessStatusCode)
					{
						var json = response.Content.ReadAsStringAsync().Result;

						return JsonConvert.DeserializeObject<T>(json);
					}

					// Call failed - read underlying error as a string
					var errorMessage = response.Content.ReadAsStringAsync().Result;

					if (string.IsNullOrWhiteSpace(errorMessage))
						errorMessage = $"Status code {(int)response.StatusCode} : {response.ReasonPhrase}";

					Logger.LogError(errorMessage);

					// Throw exception
					throw new InvalidOperationException(errorMessage);
				}
				catch (HttpRequestException ex)
				{
					throw new InvalidOperationException(ParseHttpRequestException(ex));
				}
			}
		}

		/// <summary>
		/// Generic Post Async Method
		/// </summary>
		/// <typeparam name="T">Input Model Type</typeparam>
		/// <typeparam name="TR">Output Model Type</typeparam>
		/// <param name="requestUri">Request URI</param>
		/// <param name="model">Input Model</param>
		/// <returns>Output Model</returns>
		protected async Task<TR> PostAsync<T, TR>(string requestUri, T model)
		{
			if (Logger.IsEnabled(LogLevel.Debug))
				Logger.LogDebug($"Calling HTTP POST : {RestServicesBaseUrl}{requestUri}");

			using (var client = GetHttpClientInstance())
			{
				try
				{
					var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

					// Post model to REST service
					var response = await client.PostAsync(requestUri, content);

					// Call successful - read returned data
					if (response.IsSuccessStatusCode)
					{
						var responseContext = await response.Content.ReadAsStringAsync();

						return JsonConvert.DeserializeObject<TR>(responseContext);
					}

					// Call failed - read underlying error as a string
					var errorMessage = await response.Content.ReadAsStringAsync();

					if (string.IsNullOrWhiteSpace(errorMessage))
						errorMessage = $"Status code {(int)response.StatusCode} : {response.ReasonPhrase}";

					Logger.LogError(errorMessage);

					// Throw exception
					throw new InvalidOperationException(errorMessage);
				}
				catch (HttpRequestException ex)
				{
					throw new InvalidOperationException(ParseHttpRequestException(ex));
				}
			}
		}

		/// <summary>
		/// Generic Post Sync Method
		/// </summary>
		/// <typeparam name="T">Input Model Type</typeparam>
		/// <typeparam name="TR">Output Model Type</typeparam>
		/// <param name="requestUri">Request URI</param>
		/// <param name="model">Input Model</param>
		/// <returns>Output Model</returns>
		protected TR PostSync<T, TR>(string requestUri, T model)
		{
			if (Logger.IsEnabled(LogLevel.Debug))
				Logger.LogDebug($"Calling HTTP POST : {RestServicesBaseUrl}{requestUri}");

			using (var client = GetHttpClientInstance())
			{
				try
				{
					var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

					// Post model to REST service
					var response = client.PostAsync(requestUri, content).Result;

					// Call successful - read returned data
					if (response.IsSuccessStatusCode)
					{
						var responseContext = response.Content.ReadAsStringAsync().Result;

						return JsonConvert.DeserializeObject<TR>(responseContext);
					}

					// Call failed - read underlying error as a string
					var errorMessage = response.Content.ReadAsStringAsync().Result;

					if (string.IsNullOrWhiteSpace(errorMessage))
						errorMessage = $"Status code {(int)response.StatusCode} : {response.ReasonPhrase}";

					Logger.LogError(errorMessage);

					// Throw exception
					throw new InvalidOperationException(errorMessage);
				}
				catch (HttpRequestException ex)
				{
					throw new InvalidOperationException(ParseHttpRequestException(ex));
				}
			}
		}
			
		/// <summary>
		/// Generic Put Async Method
		/// </summary>
		/// <typeparam name="T">Input Model Type</typeparam>
		/// <typeparam name="TR">Output Model Type</typeparam>
		/// <param name="requestUri">Request URI</param>
		/// <param name="model">Input Model</param>
		/// <returns>Output Model</returns>
		protected async Task<TR> PutAsync<T, TR>(string requestUri, T model)
		{
			if (Logger.IsEnabled(LogLevel.Debug))
				Logger.LogDebug($"Calling HTTP PUT : {RestServicesBaseUrl}{requestUri}");

			using (var client = GetHttpClientInstance())
			{
				try
				{
					var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

					// Post model to REST service
					var response = await client.PutAsync(requestUri, content);

					// Call successful - read returned data
					if (response.IsSuccessStatusCode)
					{
						var responseContext = await response.Content.ReadAsStringAsync();

						return JsonConvert.DeserializeObject<TR>(responseContext);
					}

					// Call failed - read underlying error as a string
					var errorMessage = await response.Content.ReadAsStringAsync();

					if (string.IsNullOrWhiteSpace(errorMessage))
						errorMessage = $"Status code {(int)response.StatusCode} : {response.ReasonPhrase}";

					Logger.LogError(errorMessage);

					// Throw exception
					throw new InvalidOperationException(errorMessage);
				}
				catch (HttpRequestException ex)
				{
					throw new InvalidOperationException(ParseHttpRequestException(ex));
				}
			}
		}

		/// <summary>
		/// Generic Put Sync Method
		/// </summary>
		/// <typeparam name="T">Input Model Type</typeparam>
		/// <typeparam name="TR">Output Model Type</typeparam>
		/// <param name="requestUri">Request URI</param>
		/// <param name="model">Input Model</param>
		/// <returns>Output Model</returns>
		protected TR PutSync<T, TR>(string requestUri, T model)
		{
			if (Logger.IsEnabled(LogLevel.Debug))
				Logger.LogDebug($"Calling HTTP PUT : {RestServicesBaseUrl}{requestUri}");

			using (var client = GetHttpClientInstance())
			{
				try
				{
					var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

					// Post model to REST service
					var response = client.PutAsync(requestUri, content).Result;

					// Call successful - read returned data
					if (response.IsSuccessStatusCode)
					{
						var responseContext = response.Content.ReadAsStringAsync().Result;

						return JsonConvert.DeserializeObject<TR>(responseContext);
					}

					// Call failed - read underlying error as a string
					var errorMessage = response.Content.ReadAsStringAsync().Result;

					if (string.IsNullOrWhiteSpace(errorMessage))
						errorMessage = $"Status code {(int)response.StatusCode} : {response.ReasonPhrase}";

					Logger.LogError(errorMessage);

					// Throw exception
					throw new InvalidOperationException(errorMessage);
				}
				catch (HttpRequestException ex)
				{
					throw new InvalidOperationException(ParseHttpRequestException(ex));
				}
			}
		}

		/// <summary>
		/// Generic Delete Async Method
		/// </summary>
		/// <typeparam name="T">Output Model Type</typeparam>
		/// <param name="requestUri">Request URI</param>
		/// <returns>Output Model</returns>
		protected async Task<T> DeleteAsync<T>(string requestUri)
		{
			if (Logger.IsEnabled(LogLevel.Debug))
				Logger.LogDebug($"Calling HTTP DELETE : {RestServicesBaseUrl}{requestUri}");

			using (var client = GetHttpClientInstance())
			{
				try
				{
					var response = await client.DeleteAsync(requestUri);

					// Call successful - read returned data
					if (response.IsSuccessStatusCode)
					{
						var responseContext = await response.Content.ReadAsStringAsync();

						return JsonConvert.DeserializeObject<T>(responseContext);
					}

					// Call failed - read underlying error as a string
					var errorMessage = await response.Content.ReadAsStringAsync();

					if (string.IsNullOrWhiteSpace(errorMessage))
						errorMessage = $"Status code {(int)response.StatusCode} : {response.ReasonPhrase}";

					Logger.LogError(errorMessage);

					// Throw exception
					throw new InvalidOperationException(errorMessage);
				}
				catch (HttpRequestException ex)
				{
					throw new InvalidOperationException(ParseHttpRequestException(ex));
				}
			}
		}

		/// <summary>
		/// Generic Delete Sync Method
		/// </summary>
		/// <typeparam name="T">Output Model Type</typeparam>
		/// <param name="requestUri">Request URI</param>
		/// <returns>Output Model</returns>
		protected T DeleteSync<T>(string requestUri)
		{
			if (Logger.IsEnabled(LogLevel.Debug))
				Logger.LogDebug($"Calling HTTP DELETE : {RestServicesBaseUrl}{requestUri}");

			using (var client = GetHttpClientInstance())
			{
				try
				{
					var response = client.DeleteAsync(requestUri).Result;

					// Call successful - read returned data
					if (response.IsSuccessStatusCode)
					{
						var responseContext = response.Content.ReadAsStringAsync().Result;

						return JsonConvert.DeserializeObject<T>(responseContext);
					}

					// Call failed - read underlying error as a string
					var errorMessage = response.Content.ReadAsStringAsync().Result;

					if (string.IsNullOrWhiteSpace(errorMessage))
						errorMessage = $"Status code {(int)response.StatusCode} : {response.ReasonPhrase}";

					Logger.LogError(errorMessage);

					// Throw exception
					throw new InvalidOperationException(errorMessage);
				}
				catch (HttpRequestException ex)
				{
					throw new InvalidOperationException(ParseHttpRequestException(ex));
				}
			}
		}

		/// <summary>
		/// Extract error messages from <see cref="HttpRequestException"/>
		/// </summary>
		/// <param name="exception"><see cref="HttpRequestException"/></param>
		/// <returns>Full error message</returns>
		protected string ParseHttpRequestException(HttpRequestException exception)
		{
			if (exception == null)
				return string.Empty;

			var errorMessage = exception.Message;

			Logger.LogError(exception, exception.Message);

			if (exception.InnerException == null) 
				return errorMessage;
			
			errorMessage += Environment.NewLine + exception.InnerException.Message;

			Logger.LogError(exception.InnerException, exception.InnerException.Message);

			if (exception.InnerException.InnerException == null) 
				return errorMessage;
				
			errorMessage += Environment.NewLine + exception.InnerException.InnerException.Message;

			Logger.LogError(exception.InnerException.InnerException, exception.InnerException.InnerException.Message);

			return errorMessage;
		}

		#endregion
	}
}