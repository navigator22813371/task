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

namespace Rick_and_Morty.Application.Logics.Episodes.Queries.GetAll
{
    public class GetAllEpisodeQuery : IRequest<PagedResponse<IEnumerable<EpisodeDto>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int Season { get; set; }
    }

    class GetAllEpisodeQueryHandler : IRequestHandler<GetAllEpisodeQuery,
        PagedResponse<IEnumerable<EpisodeDto>>>
    {
        private readonly IRickAndMortyContext _context;
        private readonly IMapper _mapper;

        public GetAllEpisodeQueryHandler(IRickAndMortyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<EpisodeDto>>> Handle(GetAllEpisodeQuery request, CancellationToken cancellationToken)
        {
            var count = await _context.Episodes
                .Where(c => c.IsDelete == false &&
                            (request.Season == 0 || c.Season == request.Season))
                .AsNoTracking()
                .CountAsync();

            double pages = Math.Ceiling((double.Parse(count.ToString()) / request.PageSize) - request.PageNumber);
            int nextPage = request.PageNumber + 1;
            var episodes = new List<Episode>();

            if (request.PageNumber == 0 && request.PageSize == 0)
            {
                pages = 0;
                episodes = await _context.Episodes
                    .Where(c => c.IsDelete == false &&
                                (request.Season == 0 || c.Season == request.Season))
                    .OrderBy(c => c.CreateDate)
                    .AsNoTracking()
                    .ToListAsync();
            }
            else
            {
                if (request.PageNumber == 0 || request.PageSize == 0)
                {
                    pages = 0;
                }

                episodes = await _context.Episodes
                    .Where(c => c.IsDelete == false &&
                                (request.Season == 0 || c.Season == request.Season))
                    .OrderBy(c => c.CreateDate)
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .AsNoTracking()
                    .ToListAsync();
            }

            var dtos = _mapper.Map<IEnumerable<EpisodeDto>>(episodes);

            foreach (var dto in dtos)
            {
                var characters = await _context.EpisodeCharacters
                    .Include(c => c.Character)
                    .Where(c => c.EpisodeId == dto.Id &&
                                c.Character.IsDelete == false)
                    .Select(c => c.Character)
                    .ToListAsync();

                var charactersDto = _mapper.Map<ICollection<CharacterDto>>(characters);
                dto.Characters = charactersDto;
            }

            return new PagedResponse<IEnumerable<EpisodeDto>>(dtos, count, int.Parse(pages.ToString()), nextPage);
        }
    }
}
