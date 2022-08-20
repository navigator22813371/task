using AutoMapper;
using MediatR;
using Rick_and_Morty.Application.Interfaces;
using Rick_and_Morty.Application.Responses;
using Rick_and_Morty.Domain;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rick_and_Morty.Application.Logics.Episodes.Command.Create
{
    public class CreateEpisodeCommand : IRequest<Response<int>>
    {
        public string Name { get; set; }
        public int Season { get; set; }
        public int Series { get; set; }
        public string Plot { get; set; }
        public DateTime Premiere { get; set; }
        public string ImageName { get; set; }
    }

    public class CreateEpisodeCommandHandler : IRequestHandler<CreateEpisodeCommand, Response<int>>
    {
        private readonly IRickAndMortyContext _context;
        private readonly IMapper _mapper;

        public CreateEpisodeCommandHandler(IRickAndMortyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateEpisodeCommand request, CancellationToken cancellationToken)
        {
            var isExist = _context.Episodes
                .Any(c => c.IsDelete == false
                          && c.Name == request.Name
                          && c.Season == c.Season
                          && c.Series == c.Series);

            if (isExist)
            {
                throw new Exception("Эпизод с такими данными уже существует");
            }

            var episode = _mapper.Map<Episode>(request);
            episode.CreateDate = DateTime.Now;
            episode.UpdateDate = DateTime.Now;
            _context.Episodes.Add(episode);
            var result = await _context.SaveChangesAsync();

            return new Response<int>(result);
        }
    }
}
