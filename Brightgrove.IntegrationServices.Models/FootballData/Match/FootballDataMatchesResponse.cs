namespace Brightgrove.IntegrationServices.Models.FootballData.Match
{
    public class FootballDataMatchesResponse
    {
        #region Public Properties

        public bool IsSuccessful { get; set; }

        public string ErrorMessage { get; set; }

        [JsonProperty("resultSet")]
        public FootballDataMatchesResultSet ResultSet { get; set; }

        [JsonProperty("matches")]
        public List<FootballDataMatchesItem> Matches { get; set; }

        #endregion
    }
}