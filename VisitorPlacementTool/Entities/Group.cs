using System.ComponentModel.DataAnnotations;

namespace VisitorPlacementTool.Entities;

public class Group
{
    [Key]
    public Guid Id { get; init; } = Guid.NewGuid();
    public DateTime RegisterTime { get; private set; }

    private readonly List<Visitor>? _visitors = new List<Visitor>();
    public IReadOnlyList<Visitor>? Visitors => _visitors.AsReadOnly();
    
    
    public Group(Guid id, DateTime registerTime, List<Visitor> visitors)
    {
        Id = id;
        RegisterTime = registerTime;
        _visitors = visitors;
    }

    
    //TODO Get Visitors
    //TODO Create Visitors



}