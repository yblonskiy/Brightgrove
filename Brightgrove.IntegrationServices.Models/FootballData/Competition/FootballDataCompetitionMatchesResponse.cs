namespace Brightgrove.IntegrationServices.Models.FootballData.Competition
{
    public class FootballDataCompetitionMatchesResponse
    {
        #region Public Properties

        public bool IsSuccessful { get; set; }

        public string ErrorMessage { get; set; }

        [JsonProperty("resultSet")]
        public FootballDataCompetitionResultSet ResultSet { get; set; }

        [JsonProperty("competition")]
        public FootballDataCompetition Competition { get; set; }

        [JsonProperty("matches")]
        public List<FootballDataCompetitionMatch> Matches { get; set; }

        #endregion
    }
}