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

namespace Rick_and_Morty.Application.Logics.Characters.Queries.GetById
{
    public class GetByIdCharacterQuery : IRequest<Response<CharacterDto>>
    {
        public Guid Id { get; set; }
    }

    class GetByIdCharacterQueryHandler : IRequestHandler<GetByIdCharacterQuery, Response<CharacterDto>>
    {
        private readonly IRickAndMortyContext _context;
        private readonly IMapper _mapper;

        public GetByIdCharacterQueryHandler(IRickAndMortyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<CharacterDto>> Handle(GetByIdCharacterQuery request, CancellationToken cancellationToken)
        {
            var character = await _context.Characters
                .Where(c => c.IsDelete == false)
                .Include(c => c.Location)
                .Include(c => c.PlaceOfBirth)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == request.Id);

            if (character == null)
                return new Response<CharacterDto>("Персонаж не найден");

            if (character.Location?.IsDelete == true)
            {
                character.Location = null;
            }

            if (character.PlaceOfBirth?.IsDelete == true)
            {
                character.PlaceOfBirth = null;
            }

            var characterDto = _mapper.Map<CharacterDto>(character);

            var characterEpisodes = await _context.EpisodeCharacters
                .Where(ec => ec.CharacterId == request.Id)
                .Include(ec => ec.Episode)
                .Where(ec => ec.Episode.IsDelete == false)
                .Select(ec => ec.Episode)
                .ToListAsync();

            var characterEpisodesDto = _mapper.Map<ICollection<EpisodeDto>>(characterEpisodes);

            characterDto.Episodes = characterEpisodesDto;

            return new Response<CharacterDto>(characterDto);
        }
    }
}
