using Brightgrove.Models.Matches;
namespace Brightgrove.Web.App.Services
{
    public interface IMatchService
    {
        Task<CompetitionsMatchesResponse> GetMatches();
    }
}