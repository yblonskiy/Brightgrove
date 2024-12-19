namespace Brightgrove.Services.WebApi.Controllers
{
    /// <summary>
    /// Base REST API Controller
    /// </summary>
    [ApiController]
	public class BaseApiController : ControllerBase
    {
		#region Private Members

		#endregion

		#region Protected Members

		/// <summary>
		/// <see cref="WebApiSettings"/>
		/// </summary>
		protected WebApiSettings AppSettings { get; set; }

	    /// <summary>
	    /// <see cref="ILogger"/>
	    /// </summary>
	    protected ILogger Logger { get; set; }

		#endregion

		#region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="settings"></param>
        protected BaseApiController(ILogger<BaseApiController> logger, 
                                    IOptions<WebApiSettings> settings)
	    {
		    Logger		= logger;
		    AppSettings	= settings.Value;
	    }

		#endregion

		#region Protected Methods

        /// <summary>
		/// Execute Function and return OK (200) Response
		/// </summary>
		/// <typeparam name="TR"></typeparam>
		/// <param name="func">Function to get data</param>
		/// <returns><see cref="ActionResult{TR}"/></returns>
		/// <response code="200">Success</response>
		/// <response code="400">Internal Error</response>
		protected async Task<ActionResult<TR>> ExecuteWithOkResponseAsync<TR>(Func<Task<TR>> func)
	    {
		    var response = await func();

			if (response == null)
			{
				return Ok(new NullModelApiResponse
				{
					Success = false,
					Error = new ApiResponseError
					{
						Code			= StatusCodes.Status204NoContent,
						Message			= "Requested data was not found",
						HttpStatusCode	= StatusCodes.Status204NoContent
					}
				});
			}

			return Ok(response);
	    }
				
	    /// <summary>
	    /// Bad Request Custom Error
	    /// </summary>
	    /// <param name="errorCode">Error Code</param>
	    /// <param name="errorMessage">Error Message</param>
	    /// <param name="exception">Error Exception</param>
	    /// <returns><see cref="ActionResult"/></returns>
	    protected ActionResult BadRequest(int errorCode = 0, string errorMessage = null, Exception exception = null)
	    {
		    Response.StatusCode = StatusCodes.Status400BadRequest;

			var model = new ApiResponseError
			{
				Code	= errorCode,
				Message = string.IsNullOrWhiteSpace(errorMessage) ? "Bad Request" : errorMessage
			};

			if (exception != null)
		    {
			    model.Details.Add(new ApiResponseError { Code =  exception.HResult, Message = exception.Message, Target = exception.Source });

				// TODO: Remove this in production!!!
			    // model.Exception = exception;
		    }

		    return StatusCode(StatusCodes.Status400BadRequest, model);
	    }

		/// <summary>
		/// Bad Request Custom Error
		/// </summary>
		/// <param name="response">Response Object</param>
		/// <param name="exception">Error Exception</param>
		/// <returns><see cref="ActionResult"/></returns>
		protected ActionResult BadRequest(BaseResponse response, Exception exception = null)
		{
			var apiResponse = new ApiErrorResponse(response) { HttpStatusCode = StatusCodes.Status400BadRequest };

			Response.StatusCode = StatusCodes.Status400BadRequest;

			if (exception != null)
				apiResponse.ErrorMessages.Add($"{exception.HResult} {exception.Source} {exception.Message}");

			return StatusCode(StatusCodes.Status400BadRequest, apiResponse);
		}

		#endregion		
	}
}