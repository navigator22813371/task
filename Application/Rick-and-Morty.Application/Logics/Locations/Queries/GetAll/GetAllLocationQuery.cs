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

namespace Rick_and_Morty.Application.Logics.Locations.Queries.GetAll
{
    public class GetAllLocationQuery : IRequest<PagedResponse<IEnumerable<LocationDto>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class GetAllLocationQueryHandler : IRequestHandler<GetAllLocationQuery, PagedResponse<IEnumerable<LocationDto>>>
    {
        private readonly IRickAndMortyContext _context;
        private readonly IMapper _mapper;

        public GetAllLocationQueryHandler(IRickAndMortyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<LocationDto>>> Handle(GetAllLocationQuery request, CancellationToken cancellationToken)
        {
            var count = await _context.Locations
                .Where(c => c.IsDelete == false)
                .AsNoTracking()
                .CountAsync();

            var pages = Math.Ceiling((double.Parse(count.ToString()) / request.PageSize) - request.PageNumber);
            var nextPage = request.PageNumber + 1;

            var locations = await _context.Locations
                .Where(c => c.IsDelete == false)
                .OrderBy(c => c.CreateDate)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .AsNoTracking()
                .ToListAsync();

            foreach(var l in locations)
            {
                l.Characters = await GetCharactersByLocationId(l.Id);
                l.PlaceOfBirthCharacters = await GetCharactersByPlaceOfBirthId(l.Id);
            }


            var dtos = _mapper.Map<IEnumerable<LocationDto>>(locations);

            return new PagedResponse<IEnumerable<LocationDto>>(dtos, count, int.Parse(pages.ToString()), nextPage);
        }

        private Task<List<Character>> GetCharactersByLocationId(Guid locationId)
            => _context.Characters.Where(c => c.LocationId == locationId && c.IsDelete == false).ToListAsync();

        private Task<List<Character>> GetCharactersByPlaceOfBirthId(Guid placeOfBirthId)
            => _context.Characters.Where(c => c.PlaceOfBirthId == placeOfBirthId && c.IsDelete == false).ToListAsync();

    }
}
