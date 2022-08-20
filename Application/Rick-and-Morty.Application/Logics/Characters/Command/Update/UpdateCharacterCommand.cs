using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rick_and_Morty.Application.Interfaces;
using Rick_and_Morty.Application.Responses;
using Rick_and_Morty.Domain.Enums;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rick_and_Morty.Application.Logics.Characters.Command.Update
{
    public class UpdateCharacterCommand : IRequest<Response<int>>
    {
        public Guid Id { get; set; }
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

    public class UpdateCharacterCommandHandler : IRequestHandler<UpdateCharacterCommand, Response<int>>
    {
        private readonly IRickAndMortyContext _context;
        private readonly IMapper _mapper;

        public UpdateCharacterCommandHandler(IRickAndMortyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(UpdateCharacterCommand request, CancellationToken cancellationToken)
        {
            var character = await _context.Characters
                .Where(c => c.IsDelete == false)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == request.Id);

            if (character == null)
                throw new Exception("Персонаж не найден");

            character = _mapper.Map(request, character);
            character.UpdateDate = DateTime.Now;
            _context.Characters.Update(character);
            var result = await _context.SaveChangesAsync();

            return new Response<int>(result);
        }
    }
}
