using System;
using System.Collections.Generic;

namespace Rick_and_Morty.Application.Dtos
{
    public class EpisodeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Season { get; set; }
        public int Series { get; set; }
        public string Plot { get; set; }
        public DateTime Premiere { get; set; }
        public string ImageName { get; set; }

        public ICollection<CharacterDto> Characters { get; set; }
    }
}
