namespace Brightgrove.CoreLibrary.Models.Base
{
	public class BaseResponse
	{
		#region Public Properties

		public bool IsSuccessful => ErrorMessages.Count == 0;

		public bool HasWarnings => WarningMessages.Count > 0;

		public List<string> ErrorMessages { get; set; } = new();

        public List<string> WarningMessages { get; set; } = new();

        #endregion

		#region Public Methods

        public string GetErrorsAsFormattedString()
        {
            var error = new StringBuilder();

            foreach (var message in ErrorMessages)
            {
                error.AppendLine(message);
            }

            return error.ToString();
        }

        public string GetWarningsAsFormattedString()
        {
            var warning = new StringBuilder();

            foreach (var message in WarningMessages)
            {
                warning.AppendLine(message);
            }

            return warning.ToString();
        }

		#endregion
	}
}