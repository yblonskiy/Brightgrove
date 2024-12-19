namespace Brightgrove.CoreLibrary.Configuration
{
    public class ApiSettings
	{
		#region Public Properties
		
		public string ApiName { get; set; }
		
		public string ApiSecret { get; set; }

        public string ApiSubscriptionKey { get; set; }

        public string ApiFullAccessScope { get; set; }

		public string ApiReadOnlyScope { get; set; }

		public string SwaggerAuthorizationUrl { get; set; }

		public string SwaggerTokenUrl { get; set; }

		public string SwaggerClientName { get; set; }

        public string SwaggerClientId { get; set; }

		public string SwaggerClientSecret { get; set; }

        public string SwaggerApiFullScope { get; set; }

        public string SwaggerApiReadScope { get; set; }

        #endregion
    }
}