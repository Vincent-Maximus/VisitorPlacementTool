using System.ComponentModel.DataAnnotations;

namespace VisitorPlacementTool.Entities;

public class Seat
{
    [Key] public Guid Id { get; set; } = Guid.NewGuid();
    public int SeatNr { get; set; }
    public bool Occupied { get; set; }
    
    private readonly List<Visitor>? _visitors = new List<Visitor>();
    public IReadOnlyList<Visitor>? Visitors => _visitors.AsReadOnly();


    public Seat(Guid id, int seatNr, bool occupied, List<Visitor> visitors)
    {
        Id = id;
        SeatNr = seatNr;
        Occupied = occupied;
        _visitors = visitors;
    }
}