using System.ComponentModel.DataAnnotations;

namespace Model.Entities;

public class BaseEntity
{
    [Key]
    public Guid Uid { get; set; } = Guid.NewGuid();
}
