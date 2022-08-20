using Rick_and_Morty.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rick_and_Morty.Application.Dtos
{
    public class LocationDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Measurements { get; set; }
        public string About { get; set; }
        public string ImageName { get; set; }

        public ICollection<CharacterDto> Characters { get; set; }
        public ICollection<CharacterDto> PlaceOfBirthCharacters { get; set; }
    }
}
