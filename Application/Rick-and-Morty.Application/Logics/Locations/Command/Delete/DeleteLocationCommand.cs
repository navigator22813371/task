using MediatR;
using Microsoft.EntityFrameworkCore;
using Rick_and_Morty.Application.Interfaces;
using Rick_and_Morty.Application.Responses;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Rick_and_Morty.Application.Logics.Locations.Command.Delete
{
    public class DeleteLocationCommand : IRequest<Response<int>>
    {
        public Guid Id { get; set; }
    }

    public class DeleteLocationCommandHandler : IRequestHandler<DeleteLocationCommand, Response<int>>
    {
        private readonly IRickAndMortyContext _context;

        public DeleteLocationCommandHandler(IRickAndMortyContext context)
        {
            _context = context;
        }

        public async Task<Response<int>> Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
        {
            var location = await _context.Locations.FirstOrDefaultAsync(c => c.Id == request.Id);

            if (location == null)
                throw new Exception("Локация не найдено");

            location.IsDelete = true;
            _context.Locations.Update(location);
            var result = await _context.SaveChangesAsync();

            return new Response<int>(result);
        }
    }
}
