using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rick_and_Morty.Application.Dtos;
using Rick_and_Morty.Application.Interfaces;
using Rick_and_Morty.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rick_and_Morty.Application.Logics.Characters.Queries.GetAll
{
    public class GetAllCharacterQuery : IRequest<PagedResponse<IEnumerable<CharacterDto>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    class GetAllCharacterQueryHandler : IRequestHandler<GetAllCharacterQuery,
        PagedResponse<IEnumerable<CharacterDto>>>
    {
        private readonly IRickAndMortyContext _context;
        private readonly IMapper _mapper;

        public GetAllCharacterQueryHandler(IRickAndMortyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<CharacterDto>>> Handle(GetAllCharacterQuery request, CancellationToken cancellationToken)
        {
            var count = await _context.Characters
                .Where(c => c.IsDelete == false)
                .AsNoTracking()
                .CountAsync();

            var pages = Math.Ceiling((double.Parse(count.ToString()) / request.PageSize) - request.PageNumber);
            var nextPage = request.PageNumber + 1;

            var characters = await _context.Characters
                .Where(c => c.IsDelete == false)
                .Include(c => c.Location)
                .OrderBy(c => c.CreateDate)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .AsNoTracking()
                .ToListAsync();

            var characterDtos = _mapper.Map<IEnumerable<CharacterDto>>(characters);

            foreach (var c in characterDtos)
            {
                c.Episodes = await GetAllEpisodeShortDtos(c.Id);
                c.PlaceOfBirth = await GetLocationNameAsync(c.PlaceOfBirthId);
            }   

            return new PagedResponse<IEnumerable<CharacterDto>>(characterDtos, count, int.Parse(pages.ToString()), nextPage);
        }

        private async Task<ICollection<EpisodeDto>> GetAllEpisodeShortDtos(Guid characterId)
        {
            var episodes = await _context.EpisodeCharacters
                .Include(ec => ec.Episode)
                .Where(ec => ec.Episode.IsDelete == false && ec.CharacterId == characterId)
                .Select(ec => ec.Episode)
                .ToListAsync();

            var episodeDtos = _mapper.Map<ICollection<EpisodeDto>>(episodes);

            return episodeDtos;
        }
        
        private async Task<LocationDto> GetLocationNameAsync(Guid? locationId)
        {
            if (locationId == null)
                return null;

            var location = await _context.Locations
                .Where(i => i.Id == locationId && i.IsDelete == false)
                .FirstOrDefaultAsync();

            if (location == null)
                return null;

            var dto = _mapper.Map<LocationDto>(location);

            return dto;
        }
    }
}
