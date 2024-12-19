namespace Brightgrove.CoreLibrary.Models.Errors
{
	[JsonObject("error")]
	public class ApiResponseError
	{
		[JsonProperty("code")]
		public int Code { get; set; }

		[JsonProperty("message")]
		public string Message { get; set; }

		[JsonProperty("target")]
		public string Target { get; set; }

		[JsonProperty("httpStatusCode")]
		public int HttpStatusCode { get; set; }

		[JsonProperty("details")]
		public IList<ApiResponseError> Details { get; set; }

		[JsonProperty("exception")]
		public Exception Exception { get; set; }

		public ApiResponseError()
		{
			Details = new List<ApiResponseError>();
		}

		public override string ToString()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}