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

namespace Rick_and_Morty.Application.Logics.Episodes.Queries.GetById
{
    public class GetByIdEpisodeQuery : IRequest<Response<EpisodeDto>>
    {
        public Guid Id { get; set; }
    }

    class GetByIdEpisodeQueryHandler : IRequestHandler<GetByIdEpisodeQuery, Response<EpisodeDto>>
    {
        private readonly IRickAndMortyContext _context;
        private readonly IMapper _mapper;

        public GetByIdEpisodeQueryHandler(IRickAndMortyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<EpisodeDto>> Handle(GetByIdEpisodeQuery request, CancellationToken cancellationToken)
        {
            var episode = await _context.Episodes
                .Where(c => c.IsDelete == false)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == request.Id);

            if (episode == null)
                return new Response<EpisodeDto>("Эпизод не найден");

            var episodeDto = _mapper.Map<EpisodeDto>(episode);

            var episodeCharacters = await _context.EpisodeCharacters
                .Where(ec => ec.EpisodeId == request.Id)
                .Include(ec => ec.Character)
                .Where(ec => ec.Character.IsDelete == false)
                .Select(ec => ec.Character)
                .ToListAsync();

            var episodeCharactersDto = _mapper.Map<ICollection<CharacterDto>>(episodeCharacters);

            episodeDto.Characters = episodeCharactersDto;

            return new Response<EpisodeDto>(episodeDto);
        }
    }
}
