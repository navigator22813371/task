using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rick_and_Morty.Application.Dtos;
using Rick_and_Morty.Application.Interfaces;
using Rick_and_Morty.Application.Responses;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rick_and_Morty.Application.Logics.Episodes.Queries.GetByName
{
    public class GetByNameEpisodeQuery : IRequest<PagedResponse<IEnumerable<EpisodeDto>>>
    {
        public string Name { get; set; }
    }

    class GetByNameEpisodeQueryHandler : IRequestHandler<GetByNameEpisodeQuery, PagedResponse<IEnumerable<EpisodeDto>>>
    {
        private readonly IRickAndMortyContext _context;
        private readonly IMapper _mapper;

        public GetByNameEpisodeQueryHandler(IRickAndMortyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<EpisodeDto>>> Handle(GetByNameEpisodeQuery request, CancellationToken cancellationToken)
        {
            var episodes = await _context.Episodes
                .Where(c => c.IsDelete == false &&
                            (string.IsNullOrWhiteSpace(request.Name) ||
                            EF.Functions.Like((c.Name).ToLower(), "%" + request.Name.ToLower() + "%")))
                .OrderBy(c => c.CreateDate)
                .AsNoTracking()
                .ToListAsync();

            var dtos = _mapper.Map<IEnumerable<EpisodeDto>>(episodes);

            return new PagedResponse<IEnumerable<EpisodeDto>>(dtos, episodes.Count);
        }
    }
}
