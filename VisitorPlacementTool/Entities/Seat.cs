using System.ComponentModel.DataAnnotations;

namespace VisitorPlacementTool.Entities;

public class Seat
{
    [Key] public Guid Id { get; init; } = Guid.NewGuid();
    public int SeatNr { get; private set; }
    public int SeatRow { get; private set; }
    public bool Occupied { get; private set; }
    
    private readonly List<Visitor>? _visitors = new List<Visitor>();
    public IReadOnlyList<Visitor>? Visitors => _visitors.AsReadOnly();


    public Seat(Guid id, int seatNr, int seatRow, bool occupied)
    // public Seat(Guid id, int seatNr, bool occupied, List<Visitor> visitors)
    {
        Id = id;
        SeatNr = seatNr;
        SeatRow = seatRow;
        Occupied = occupied;
        // _visitors = visitors;
    }
}