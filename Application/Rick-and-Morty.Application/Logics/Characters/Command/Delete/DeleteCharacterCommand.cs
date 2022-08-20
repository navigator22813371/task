using MediatR;
using Microsoft.EntityFrameworkCore;
using Rick_and_Morty.Application.Interfaces;
using Rick_and_Morty.Application.Responses;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Rick_and_Morty.Application.Logics.Characters.Command.Delete
{
    public class DeleteCharacterCommand : IRequest<Response<int>>
    {
        public Guid Id { get; set; }
    }

    class DeleteCharacterCommandHandler : IRequestHandler<DeleteCharacterCommand, Response<int>>
    {
        private readonly IRickAndMortyContext _context;

        public DeleteCharacterCommandHandler(IRickAndMortyContext context)
        {
            _context = context;
        }

        public async Task<Response<int>> Handle(DeleteCharacterCommand request, CancellationToken cancellationToken)
        {
            var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == request.Id);

            if (character == null)
                throw new Exception("Персонаж не найден");

            character.IsDelete = true;
            _context.Characters.Update(character);
            var result = await _context.SaveChangesAsync();

            return new Response<int>(result);
        }
    }
}
