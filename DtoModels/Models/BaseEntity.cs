using System;
using System.ComponentModel.DataAnnotations;

namespace DtoModels.Models
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
