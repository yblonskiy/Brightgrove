namespace Brightgrove.CoreLibrary.Models.Errors
{
	public class UnauthorizedApiResponse : BaseResponse
	{
		#region Public Properties

		[JsonProperty("httpStatusCode")]
		public int HttpStatusCode { get; set; }

		#endregion

		#region Constructor

		public UnauthorizedApiResponse(string errorMessage, int httpStatusCode)
		{
			ErrorMessages.Add(errorMessage);

			HttpStatusCode = httpStatusCode;			
		}

		#endregion
	}
}