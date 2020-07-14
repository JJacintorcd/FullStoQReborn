using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Recodme.RD.FullStoQReborn.Data.Base
{
    public abstract class Entity
    {
        [Key]
        public Guid Id { get; private set; }

        [Required]
        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; private set; }

        [Required]
        [Display(Name = "Updated At")]
        public DateTime UpdatedAt { get; protected set; }

        private bool _isDeleted;

        [Required]
        [Display(Name = "Deleted")]
        public bool IsDeleted
        {
            get => _isDeleted;

            set
            {
                _isDeleted = value;
                RegisterChange();
            }
        }

        protected virtual void RegisterChange()
        {
            UpdatedAt = DateTime.UtcNow;
        }

        protected Entity()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = CreatedAt;
            _isDeleted = false;

        }

        protected Entity(Guid id, DateTime createdAt, DateTime updatedAt, bool isDeleted)
        {
            Id = id;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            _isDeleted = isDeleted;

        }
    }
}
