using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rick_and_Morty.Application.Dtos;
using Rick_and_Morty.Application.Interfaces;
using Rick_and_Morty.Application.Responses;
using Rick_and_Morty.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rick_and_Morty.Application.Logics.Locations.Queries.Filter
{
    public class FilterLocationQuery : IRequest<PagedResponse<IEnumerable<LocationDto>>>
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Measurements { get; set; }
    }

    class FilterLocationQueryHandler : IRequestHandler<FilterLocationQuery, PagedResponse<IEnumerable<LocationDto>>>
    {
        private readonly IRickAndMortyContext _context;
        private readonly IMapper _mapper;

        public FilterLocationQueryHandler(IRickAndMortyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<LocationDto>>> Handle(FilterLocationQuery request, CancellationToken cancellationToken)
        {
            var locations = await _context.Locations
                .Where(c => c.IsDelete == false &&
                            (string.IsNullOrWhiteSpace(request.Name) || EF.Functions.Like((c.Name).ToLower(), "%" + request.Name.ToLower() + "%")) &&
                            (string.IsNullOrWhiteSpace(request.Type) || EF.Functions.Like((c.Type).ToLower(), "%" + request.Type.ToLower() + "%")) &&
                            (string.IsNullOrWhiteSpace(request.Measurements) || EF.Functions.Like((c.Measurements).ToLower(), "%" + request.Measurements.ToLower() + "%")))
                .OrderBy(c => c.CreateDate)
                .AsNoTracking()
                .ToListAsync();

            foreach (var l in locations)
            {
                l.Characters = await GetCharactersByLocationId(l.Id);
                l.PlaceOfBirthCharacters = await GetCharactersByPlaceOfBirthId(l.Id);
            }

            var dtos = _mapper.Map<IEnumerable<LocationDto>>(locations);

            return new PagedResponse<IEnumerable<LocationDto>>(dtos, locations.Count);
        }

        private Task<List<Character>> GetCharactersByLocationId(Guid locationId)
           => _context.Characters.Where(c => c.LocationId == locationId && c.IsDelete == false).ToListAsync();

        private Task<List<Character>> GetCharactersByPlaceOfBirthId(Guid placeOfBirthId)
            => _context.Characters.Where(c => c.PlaceOfBirthId == placeOfBirthId && c.IsDelete == false).ToListAsync();
    }
}
