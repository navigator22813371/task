using Rick_and_Morty.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rick_and_Morty.Domain
{
    public class Location : AuditableBaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Measurements { get; set; }
        public string About { get; set; }
        public string ImageName { get; set; }

        [InverseProperty("Location")]
        public ICollection<Character> Characters { get; set; }

        [InverseProperty("PlaceOfBirth")]
        public ICollection<Character> PlaceOfBirthCharacters { get; set; }
    }
}
