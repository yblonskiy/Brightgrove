namespace Brightgrove.IntegrationServices.Models.FootballData.Match
{
    public class FootballDataMatchScore
    {
        #region Public Properties

        public string Winner { get; set; }

        public string Duration { get; set; }

        public FootballDataMatchTime FullTime { get; set; }

        public FootballDataMatchTime HalfTime { get; set; }

        #endregion
    }
}