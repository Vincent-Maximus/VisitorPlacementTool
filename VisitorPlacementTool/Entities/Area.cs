using System.ComponentModel.DataAnnotations;

namespace VisitorPlacementTool.Entities;

public class Area
{
    [Key] public Guid Id { get; set; } = Guid.NewGuid();
    public int AreaNr { get; set; }
    public int RowLength { get; set; }
    public int RowNr { get; set; }
    
    private readonly List<Seat>? _seats = new List<Seat>();
    public IReadOnlyList<Seat>? Seats => _seats.AsReadOnly();

    public Area(Guid id, int areaNr, int rowLength, int rowNr, List<Seat> seats)
    {
        Id = id;
        AreaNr = areaNr;
        RowLength = rowLength;
        RowNr = rowNr;
        _seats = seats;
    }
    
    
    //TODO Get Seats
    //TODO Create Seats
}