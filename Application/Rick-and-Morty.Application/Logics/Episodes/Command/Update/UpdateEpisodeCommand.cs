using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rick_and_Morty.Application.Interfaces;
using Rick_and_Morty.Application.Responses;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rick_and_Morty.Application.Logics.Episodes.Command.Update
{
    public class UpdateEpisodeCommand : IRequest<Response<int>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Season { get; set; }
        public int Series { get; set; }
        public string Plot { get; set; }
        public DateTime Premiere { get; set; }
        public string ImageName { get; set; }
    }

    public class UpdateEpisodeCommandHandler : IRequestHandler<UpdateEpisodeCommand, Response<int>>
    {
        private readonly IRickAndMortyContext _context;
        private readonly IMapper _mapper;

        public UpdateEpisodeCommandHandler(IRickAndMortyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(UpdateEpisodeCommand request, CancellationToken cancellationToken)
        {
            var episode = await _context.Episodes
                .Where(c => c.IsDelete == false)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == request.Id);

            if (episode == null)
                throw new Exception("Эпизод не найден");

            episode = _mapper.Map(request, episode);
            episode.UpdateDate = DateTime.Now;
            _context.Episodes.Update(episode);
            var result = await _context.SaveChangesAsync();

            return new Response<int>(result);
        }
    }
}
