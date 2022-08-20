using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rick_and_Morty.Application.Dtos;
using Rick_and_Morty.Application.Interfaces;
using Rick_and_Morty.Application.Responses;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rick_and_Morty.Application.Logics.Locations.Queries.GetById
{
    public class GetByIdLocationQuery : IRequest<Response<LocationDto>>
    {
        public Guid Id { get; set; }
    }

    public class GetByIdLocationQueryHandler : IRequestHandler<GetByIdLocationQuery, Response<LocationDto>>
    {
        private readonly IRickAndMortyContext _context;
        private readonly IMapper _mapper;

        public GetByIdLocationQueryHandler(IRickAndMortyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<LocationDto>> Handle(GetByIdLocationQuery request, CancellationToken cancellationToken)
        {
            var location = await _context.Locations
                .Where(l => l.IsDelete == false)
                .Include(l => l.Characters)
                .Include(l => l.PlaceOfBirthCharacters)
                .AsNoTracking()
                .FirstOrDefaultAsync(l => l.Id == request.Id);

            if (location == null)
                return new Response<LocationDto>("Локация не найдена.");

            if (location.Characters.Count > 0)
            {
                location.Characters = location.Characters
                                        .Where(c => c.IsDelete == false).ToList();
            }

            if (location.PlaceOfBirthCharacters.Count > 0)
            {
                location.PlaceOfBirthCharacters = location.PlaceOfBirthCharacters
                                                    .Where(c => c.IsDelete == false).ToList();
            }

            var locationDto = _mapper.Map<LocationDto>(location);

            return new Response<LocationDto>(locationDto);
        }
    }
}
