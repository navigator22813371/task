using MediatR;
using Rick_and_Morty.Application.Interfaces;
using Rick_and_Morty.Application.Responses;
using Rick_and_Morty.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rick_and_Morty.Application.Logics.Episodes.Command.Create
{
    public class AddCharactersInEpisodeCommand : IRequest<Response<int>>
    {
        public Guid EpisodeId { get; set; }
        public ICollection<Guid> CharactersId { get; set; }
    }

    public class AddCharactersInEpisodeCommandHandler : IRequestHandler<AddCharactersInEpisodeCommand, Response<int>>
    {
        private readonly IRickAndMortyContext _context;

        public AddCharactersInEpisodeCommandHandler(IRickAndMortyContext context)
        {
            _context = context;
        }

        public async Task<Response<int>> Handle(AddCharactersInEpisodeCommand request, CancellationToken cancellationToken)
        {
            foreach (var item in request.CharactersId)
            {
                var isExist = _context.EpisodeCharacters
                    .Any(ec => ec.EpisodeId == request.EpisodeId 
                               && ec.CharacterId == item);

                if (!isExist)
                {
                    var episodeCharacter = new EpisodeCharacters()
                    {
                        EpisodeId = request.EpisodeId,
                        CharacterId = item
                    };

                    _context.EpisodeCharacters.Add(episodeCharacter);
                }
            }

            var result = await _context.SaveChangesAsync();

            return new Response<int>(result);
        }
    }
}
