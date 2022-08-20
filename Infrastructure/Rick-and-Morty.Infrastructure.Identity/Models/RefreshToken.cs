﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Rick_and_Morty.Infrastructure.Identity.Models
{
    public class RefreshToken
    {
        [Key]
        public Guid Token { get; set; }
        public string JwtId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool Used { get; set; }
        public bool Invalidated { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
