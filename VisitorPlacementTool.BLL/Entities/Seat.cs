using System.ComponentModel.DataAnnotations;

namespace VisitorPlacementTool.BLL.Entities
{
    public class Seat
    {
        [Key] public Guid Id { get; private set; } = Guid.NewGuid();
        public int SeatNr { get; private set; }
        public int SeatRow { get; private set; }
        public bool Occupied { get; private set; }
        public Visitor? Visitors { get; set; }
    
        // private readonly List<Visitor>? _visitors = new List<Visitor>();
        // public IReadOnlyList<Visitor>? Visitors => _visitors.AsReadOnly();


        public Seat(Guid id, int seatNr, int seatRow, bool occupied)
        {
            Id = id;
            SeatNr = seatNr;
            SeatRow = seatRow;
            Occupied = occupied;
        }
    }
}