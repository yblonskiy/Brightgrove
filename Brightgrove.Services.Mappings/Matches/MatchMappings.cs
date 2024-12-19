namespace Brightgrove.Services.Mappings.Matches
{
    public class MatchMappings : Profile
	{
		public new string ProfileName => "MatchMappings";

		public MatchMappings()
		{
            CreateMap<MatchesInputModel, FootballDataMatchesInputModel>()
                .ForMember(dest => dest.CompetitionIds,                                 opt => opt.MapFrom(src => src.CompetitionIds))
                .ForMember(dest => dest.DateFrom,                                       opt => opt.MapFrom(src => src.DateFrom))
                .ForMember(dest => dest.DateTo,                                         opt => opt.MapFrom(src => src.DateTo))
                .ForMember(dest => dest.MatchIds,                                       opt => opt.MapFrom(src => src.MatchIds))
                .ForMember(dest => dest.Status,                                         opt => opt.MapFrom(src => src.Status))
                .ForAllOtherMembers(dest => dest.Ignore());

            CreateMap<FootballDataMatchesCompetition, MatchCompetition>()
                .ForMember(dest => dest.Id,                                             opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name,                                           opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Code,                                           opt => opt.MapFrom(src => src.Code))
                .ForMember(dest => dest.Type,                                           opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.Emblem,                                         opt => opt.MapFrom(src => src.Emblem))
                .ForAllOtherMembers(dest => dest.Ignore());

            CreateMap<FootballDataMatchTime, MatchTime>()
               .ForMember(dest => dest.Home,                                            opt => opt.MapFrom(src => src.Home))
               .ForMember(dest => dest.Away,                                            opt => opt.MapFrom(src => src.Away))
               .ForAllOtherMembers(dest => dest.Ignore());

            CreateMap<FootballDataMatchTeam, MatchTeam>()
               .ForMember(dest => dest.Id,                                              opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Name,                                            opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.ShortName,                                       opt => opt.MapFrom(src => src.ShortName))
               .ForMember(dest => dest.Tla,                                             opt => opt.MapFrom(src => src.Tla))
               .ForMember(dest => dest.Crest,                                           opt => opt.MapFrom(src => src.Crest))
               .ForAllOtherMembers(dest => dest.Ignore());

            CreateMap<FootballDataMatchScore, MatchScore>()
               .ForMember(dest => dest.Winner,                                          opt => opt.MapFrom(src => src.Winner))
               .ForMember(dest => dest.Duration,                                        opt => opt.MapFrom(src => src.Duration))
               .ForMember(dest => dest.FullTime,                                        opt => opt.MapFrom(src => src.FullTime))
               .ForMember(dest => dest.HalfTime,                                        opt => opt.MapFrom(src => src.HalfTime))
               .ForAllOtherMembers(dest => dest.Ignore());

            CreateMap<FootballDataMatchesItem, MatchItem>()
               .ForMember(dest => dest.Id,                                              opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.UtcDate,                                         opt => opt.MapFrom(src => src.UtcDate))
               .ForMember(dest => dest.Status,                                          opt => opt.MapFrom(src => src.Status))
               .ForMember(dest => dest.Matchday,                                        opt => opt.MapFrom(src => src.Matchday))
               .ForMember(dest => dest.Stage,                                           opt => opt.MapFrom(src => src.Stage))
               .ForMember(dest => dest.LastUpdated,                                     opt => opt.MapFrom(src => src.LastUpdated))
               .ForMember(dest => dest.HomeTeam,                                        opt => opt.MapFrom(src => src.HomeTeam))
               .ForMember(dest => dest.AwayTeam,                                        opt => opt.MapFrom(src => src.AwayTeam))
               .ForMember(dest => dest.Score,                                           opt => opt.MapFrom(src => src.Score))
               .ForAllOtherMembers(dest => dest.Ignore());
        }
    }
}