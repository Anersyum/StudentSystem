using System.ComponentModel.DataAnnotations;

namespace Domain.Abstractions.Classes;

public abstract class Entity
{
    [Required]
    public int Id { get; set; }
}
