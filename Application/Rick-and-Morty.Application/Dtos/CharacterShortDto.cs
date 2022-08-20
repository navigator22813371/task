using Rick_and_Morty.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Rick_and_Morty.Application.Dtos
{
    public class CharacterShortDto
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
        public LocationShortDto Location { get; set; }

        public Guid? PlaceOfBirthId { get; set; }
        public string PlaceOfBirth { get; set; }

        public ICollection<EpisodeShortDto> Episodes { get; set; }
    }
}
