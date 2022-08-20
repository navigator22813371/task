using Rick_and_Morty.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rick_and_Morty.Application.Dtos
{
    public class LocationShortDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
