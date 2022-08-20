using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rick_and_Morty.Application.Dtos;
using Rick_and_Morty.Application.Interfaces;
using Rick_and_Morty.Application.Responses;
using Rick_and_Morty.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rick_and_Morty.Application.Logics.Characters.Queries.Filter
{
    public class FilterCharacterQuery : IRequest<PagedResponse<ICollection<CharacterShortDto>>>
    {
        public string Name { get; set; }
        public ICollection<StatusEnums> Status { get; set; }
        public ICollection<GenderEnums> Gender { get; set; }
    }

    class FilterCharacterQueryHandler : IRequestHandler<FilterCharacterQuery, PagedResponse<ICollection<CharacterShortDto>>>
    {
        private readonly IRickAndMortyContext _context;
        private readonly IMapper _mapper;

        public FilterCharacterQueryHandler(IRickAndMortyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagedResponse<ICollection<CharacterShortDto>>> Handle(FilterCharacterQuery request, CancellationToken cancellationToken)
        {
            //var status = new List<StatusEnums>();
            //foreach (var item in request.Status)
            //{
            //    status.Add((StatusEnums)item);
            //}

            //var genders = new List<GenderEnums>();
            //foreach (var item in request.Gender)
            //{
            //    genders.Add((GenderEnums)item);
            //}

            var characters = await _context.Characters
                .Include(c => c.Location)
                .Where(c => c.IsDelete == false &&
                            (string.IsNullOrWhiteSpace(request.Name) ||
                            EF.Functions.Like((c.LastName + " " + c.FirstName).ToLower(), "%" + request.Name.ToLower() + "%")) &&
                            (request.Status.Count == 0 || request.Status.Contains(c.Status)) &&
                            (request.Gender.Count == 0 || request.Gender.Contains(c.Gender)))
                .OrderBy(c => c.CreateDate)
                .AsNoTracking()
                .ToListAsync();

            var characterDtos = _mapper.Map<ICollection<CharacterShortDto>>(characters);

            foreach (var c in characterDtos)
            {
                c.Episodes = await GetAllEpisodeShortDtos(c.Id);
                c.PlaceOfBirth = await GetLocationNameAsync(c.PlaceOfBirthId);
            }

            return new PagedResponse<ICollection<CharacterShortDto>>(characterDtos, characterDtos.Count());
        }

        private async Task<ICollection<EpisodeShortDto>> GetAllEpisodeShortDtos(Guid characterId)
        {
            var episodes = await _context.EpisodeCharacters
                //.Where(ec => ec.CharacterId == characterId)
                .Include(ec => ec.Episode)
                .Where(ec => ec.Episode.IsDelete == false && ec.CharacterId == characterId)
                .Select(ec => ec.Episode)
                .ToListAsync();

            var episodeDtos = _mapper.Map<ICollection<EpisodeShortDto>>(episodes);

            return episodeDtos;
        }

        private async Task<string> GetLocationNameAsync(Guid? locationId)
        {
            if (locationId == null)
                return null;

            var location = await _context.Locations
                    .Where(i => i.Id == locationId && i.IsDelete == false)
                    .FirstOrDefaultAsync();

            if (location == null)
                return null;

            return location.Name;
        }
    }
}
