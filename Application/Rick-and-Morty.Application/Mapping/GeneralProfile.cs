using AutoMapper;
using Rick_and_Morty.Application.Dtos;
using Rick_and_Morty.Application.Logics.Characters.Command.Create;
using Rick_and_Morty.Application.Logics.Characters.Command.Update;
using Rick_and_Morty.Application.Logics.Episodes.Command.Create;
using Rick_and_Morty.Application.Logics.Episodes.Command.Update;
using Rick_and_Morty.Application.Logics.Locations.Command.Create;
using Rick_and_Morty.Application.Logics.Locations.Command.Update;
using Rick_and_Morty.Domain;

namespace Rick_and_Morty.Application.Mapping
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<CreateCharacterCommand, Character>();
            CreateMap<UpdateCharacterCommand, Character>();
            CreateMap<Character, CharacterDto>();
            CreateMap<Character, CharacterShortDto>();

            CreateMap<CreateEpisodeCommand, Episode>();
            CreateMap<UpdateEpisodeCommand, Episode>();
            CreateMap<Episode, EpisodeDto>();
            CreateMap<Episode, EpisodeShortDto>();

            CreateMap<CreateLocationCommand, Location>();
            CreateMap<UpdateLocationCommand, Location>();
            CreateMap<Location, LocationDto>();
            CreateMap<Location, LocationShortDto>();
        }
    }
}
