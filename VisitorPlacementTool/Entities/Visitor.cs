using System.ComponentModel.DataAnnotations;

namespace VisitorPlacementTool.Entities;

public class Visitor
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Name { get; set; }
    public DateOnly Birthday { get; set; }
}
