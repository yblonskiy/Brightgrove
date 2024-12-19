namespace Brightgrove.CoreLibrary.Configuration
{
	public class WebApiSettings
	{
        #region Public Properties

        public FootballDataSettings FootballDataSettings { get; set; }

        #endregion

        #region Constructors

        public WebApiSettings()
		{
            FootballDataSettings = new FootballDataSettings();
        }

		#endregion
	}
}