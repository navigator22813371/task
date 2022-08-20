using MediatR;
using Microsoft.EntityFrameworkCore;
using Rick_and_Morty.Application.Interfaces;
using Rick_and_Morty.Application.Responses;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Rick_and_Morty.Application.Logics.Episodes.Command.Delete
{
    public class DeleteEpisodeCommand : IRequest<Response<int>>
    {
        public Guid Id { get; set; }
    }

    class DeleteEpisodeCommandHandler : IRequestHandler<DeleteEpisodeCommand, Response<int>>
    {
        private readonly IRickAndMortyContext _context;

        public DeleteEpisodeCommandHandler(IRickAndMortyContext context)
        {
            _context = context;
        }

        public async Task<Response<int>> Handle(DeleteEpisodeCommand request, CancellationToken cancellationToken)
        {
            var episode = await _context.Episodes.FirstOrDefaultAsync(c => c.Id == request.Id);

            if (episode == null)
                throw new Exception("Эпизод не найден");

            episode.IsDelete = true;
            _context.Episodes.Update(episode);
            var result = await _context.SaveChangesAsync();

            return new Response<int>(result);
        }
    }
}
