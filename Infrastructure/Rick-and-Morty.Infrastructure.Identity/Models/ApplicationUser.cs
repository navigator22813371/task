using Microsoft.AspNetCore.Identity;
using System;

namespace Rick_and_Morty.Infrastructure.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public virtual string FullName => this.LastName + ' ' + this.FirstName + ' ' + this.Patronymic;
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool IsDelete { get; set; }
        public string Avatar { get; set; }
    }
}
