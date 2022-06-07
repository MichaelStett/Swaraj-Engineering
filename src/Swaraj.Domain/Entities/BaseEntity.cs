using Swaraj.Domain.Entities.Identifiers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Swaraj.Domain.Entities
{
    public abstract class BaseEntity<T>
        : IEntity
        where T : EntityIdentifier
    {
        public BaseEntity()
        {
            
        }

        [Key]
        [Required]
        public T Id { get; set; }

        object IEntity.Id { get { return Id.Value; } }

        [Required]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedOnUtc { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime? LastModifiedOnUtc { get; set; }

        public string IPAddress { get; set; }

        public bool IsDeleted { get; set; }
    }
}
