using AutoMapper;
using MediatR;
using Rick_and_Morty.Application.Interfaces;
using Rick_and_Morty.Application.Responses;
using Rick_and_Morty.Domain;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rick_and_Morty.Application.Logics.Locations.Command.Create
{
    public class CreateLocationCommand : IRequest<Response<int>>
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Measurements { get; set; }
        public string About { get; set; }
        public string ImageName { get; set; }
    }

    public class CreateLocationCommandHandler : IRequestHandler<CreateLocationCommand, Response<int>>
    {
        private readonly IRickAndMortyContext _context;
        private readonly IMapper _mapper;

        public CreateLocationCommandHandler(IRickAndMortyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
        {
            var isExist = _context.Locations
                .Any(c => c.IsDelete == false
                          && c.Name == request.Name
                          && c.Type == c.Type
                          && c.Measurements == c.Measurements);

            if (isExist)
            {
                throw new Exception("Локация с такими данными уже существует");
            }

            var location = _mapper.Map<Location>(request);
            location.CreateDate = DateTime.Now;
            location.UpdateDate = DateTime.Now;
            _context.Locations.Add(location);
            var result = await _context.SaveChangesAsync();

            return new Response<int>(result);
        }
    }
}
