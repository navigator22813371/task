using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rick_and_Morty.Application.Interfaces;
using Rick_and_Morty.Application.Responses;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rick_and_Morty.Application.Logics.Locations.Command.Update
{
    public class UpdateLocationCommand : IRequest<Response<int>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Measurements { get; set; }
        public string About { get; set; }
        public string ImageName { get; set; }
    }

    public class UpdateLocationCommandHandler : IRequestHandler<UpdateLocationCommand, Response<int>>
    {
        private readonly IRickAndMortyContext _context;
        private readonly IMapper _mapper;

        public UpdateLocationCommandHandler(IRickAndMortyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
        {
            var location = await _context.Locations
                .Where(c => c.IsDelete == false)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == request.Id);

            if (location == null)
                throw new Exception("Локация не найдено");

            location = _mapper.Map(request, location);
            location.UpdateDate = DateTime.Now;
            _context.Locations.Update(location);
            var result = await _context.SaveChangesAsync();

            return new Response<int>(result);
        }
    }
}
