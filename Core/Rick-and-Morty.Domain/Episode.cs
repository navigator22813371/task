using Rick_and_Morty.Domain.Common;
using System;

namespace Rick_and_Morty.Domain
{
    public class Episode : AuditableBaseEntity<Guid>
    {
        public string Name { get; set; }
        public int Season { get; set; }
        public int Series { get; set; }
        public string Plot { get; set; }
        public DateTime Premiere { get; set; }
        public string ImageName { get; set; }
    }
}
