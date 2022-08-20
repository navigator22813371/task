using Rick_and_Morty.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rick_and_Morty.Domain
{
    public class EpisodeCharacters
    {
        public Guid? EpisodeId { get; set; }
        public Episode Episode { get; set; }

        public Guid? CharacterId { get; set; }
        public Character Character { get; set; }
    }
}
