namespace Brightgrove.CoreLibrary.Models.Errors
{
	/// <summary>
	/// API Error Response Model
	/// </summary>
	public class ApiErrorResponse : BaseResponse
	{
		#region Public Properties

		/// <summary>
		/// HTP Status Code
		/// </summary>
		public int HttpStatusCode { get; set; }

		#endregion

		#region Constructor

		/// <summary>
		/// Default Constructor
		/// </summary>
		public ApiErrorResponse() : base()
		{
		}

		/// <summary>
		/// Copy Constructor
		/// </summary>
		/// <param name="response"></param>
		public ApiErrorResponse(BaseResponse response)
		{
			if (response != null)
			{
				ErrorMessages	= response.ErrorMessages;
				WarningMessages = response.WarningMessages;
			}
		}

		#endregion
	}
}