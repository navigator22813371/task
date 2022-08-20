using System;
using System.ComponentModel.DataAnnotations;

namespace Rick_and_Morty.Domain.Common
{
    public abstract class AuditableBaseEntity<T>
    {
        [Key]
        public virtual T Id { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool IsDelete { get; set; }
        public Guid CreatedUserId { get; set; }
        public Guid UpdatedUserId { get; set; }
    }
}
