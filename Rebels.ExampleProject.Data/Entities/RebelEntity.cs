

namespace Rebels.ExampleProject.Data.Entities;
using System.ComponentModel.DataAnnotations;

public class RebelEntity
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or sets the name of the Rebel.
    /// </summary>
    [Required]
    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;
}
