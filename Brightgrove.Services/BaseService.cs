namespace Brightgrove.Services
{
    /// <summary>
    /// Base service class
    /// </summary>
    public abstract class BaseService : IBaseService
	{
		#region Protected Properties

		/// <summary>
		/// <see cref="ILogger"/>
		/// </summary>
		protected ILogger Logger { get; set; }

        /// <summary>
        /// <see cref="IMapper"/>
        /// </summary>
        protected IMapper Mapper { get; set; }

        /// <summary>
        /// <see cref="WebApiSettings"/>
        /// </summary>
        protected WebApiSettings AppSettings { get; set; }

		#endregion

		#region Constructors

		protected BaseService(IMapper mapper, ILogger logger, IOptions<WebApiSettings> settings)
		{
            Mapper		= mapper;
            Logger		= logger;
			AppSettings	= settings.Value;
		}

		#endregion

		#region Public Methods

		public void SetBearerToken(string accessToken)
		{
			// Do nothing : this method is needed for REST service agent
		}

		#endregion

		#region Protected Methods

		protected void ParseAndLogException(BaseResponse result, Exception ex)
		{
			Logger.LogError(ex, ex.Message);

			if (ex.InnerException != null)
			{
				Logger.LogError(ex.InnerException, ex.InnerException.Message);

				result.ErrorMessages.Add($"Internal Error : {ex.InnerException.Message}");
			}
			else
			{
				result.ErrorMessages.Add($"Internal Error : {ex.Message}");
			}
		}

        #endregion
    }
}