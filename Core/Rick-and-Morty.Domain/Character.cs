using Rick_and_Morty.Domain.Common;
using Rick_and_Morty.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rick_and_Morty.Domain
{
    public class Character : AuditableBaseEntity<Guid>
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
        [ForeignKey("LocationId")]
        public Location Location { get; set; }

        public Guid? PlaceOfBirthId { get; set; }
        [ForeignKey("PlaceOfBirthId")]
        public Location PlaceOfBirth { get; set; }
    }
}
