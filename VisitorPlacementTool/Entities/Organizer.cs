using System.ComponentModel.DataAnnotations;

namespace VisitorPlacementTool.Entities;

public class Organizer
{
    [Key] public Guid Id { get; set; } = Guid.NewGuid();
    public string? Name { get; set; }

    private readonly List<Visitor>? _visitors = new List<Visitor>();
    public IReadOnlyList<Visitor>? Visitors => _visitors.AsReadOnly();

    private readonly List<Event>? _events = new List<Event>();
    public IReadOnlyList<Event>? Events => _events.AsReadOnly();

    public Organizer(Guid id, string name, List<Visitor> visitors, List<Event> events)
    {
        Id = id;
        Name = name;
        _visitors = visitors;
        _events = events;
    }
    
    //TODO Get Events
    //TODO Create Events
}