using AutoMapper;
using MediatR;
using Rick_and_Morty.Application.Interfaces;
using Rick_and_Morty.Application.Responses;
using Rick_and_Morty.Domain;
using Rick_and_Morty.Domain.Enums;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rick_and_Morty.Application.Logics.Characters.Command.Create
{
    public class CreateCharacterCommand : IRequest<Response<int>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual string FullName => this.LastName + " " + this.FirstName;
        public StatusEnums Status { get; set; }
        public string About { get; set; }
        public GenderEnums Gender { get; set; }
        public string Race { get; set; }
        public string ImageName { get; set; }
        public Guid? LocationId { get; set; }
        public Guid? PlaceOfBirthId { get; set; }
    }

    public class CreateCharacterCommandHandler : IRequestHandler<CreateCharacterCommand, Response<int>>
    {
        private readonly IRickAndMortyContext _context;
        private readonly IMapper _mapper;

        public CreateCharacterCommandHandler(IRickAndMortyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateCharacterCommand request, CancellationToken cancellationToken)
        {
            var isExist = _context.Characters
                .Any(c => c.IsDelete == false
                          && c.FirstName == request.FirstName
                          && c.LastName == c.LastName);

            if (isExist)
            {
                throw new Exception("Персонаж с таким именем уже существует");
            }

            var character = _mapper.Map<Character>(request);
            character.CreateDate = DateTime.Now;
            character.UpdateDate = DateTime.Now;
            _context.Characters.Add(character);
            var result = await _context.SaveChangesAsync();

            return new Response<int>(result);
        }
    }
}
