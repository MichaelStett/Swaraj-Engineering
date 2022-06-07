﻿using System;

namespace Swaraj.Domain.Entities
{
    public interface IEntity
    {
        object Id { get; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime? LastModifiedOnUtc { get; set; }

        public string IPAddress { get; set; }

        public bool IsDeleted { get; set; }
    }
}
