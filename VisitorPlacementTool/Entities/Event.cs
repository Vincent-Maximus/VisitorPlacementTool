using System.ComponentModel.DataAnnotations;
using HostingEnvironmentExtensions = Microsoft.AspNetCore.Hosting.HostingEnvironmentExtensions;

namespace VisitorPlacementTool.Entities;

public class Event
{
    [Key] public Guid Id { get; set; } = Guid.NewGuid();
    public string? Name { get; set; }
    public int AmountAreas { get; set; }
    public DateTime DateTime { get; set; }

    private readonly List<Area>? _areas = new List<Area>();
    public IReadOnlyList<Area>? Areas => _areas.AsReadOnly();
    
    private readonly List<Group>? _groups = new List<Group>();
    public IReadOnlyList<Group>? Groups => _groups.AsReadOnly();


    public Event(Guid id, string name, int amountAreas, DateTime dateTime)
    // public Event(Guid id, string name, int amountAreas, DateTime dateTime, List<Area> areas, List<Group> groups)
    {
        Id = id;
        Name = name;
        AmountAreas = amountAreas;
        DateTime = dateTime;
        // _areas = areas;
        // _groups = groups;
    }


    //Get all areas
    public List<Area> GetAreas()
    {
        return _areas;
    }


    //TODO Create new area
    
    
    
    //TODO Get Groups
    //TODO Create Groups
    
}