using System.ComponentModel.DataAnnotations;

namespace VisitorPlacementTool.Entities;

public class Area
{
    [Key] public Guid Id { get; init; } = Guid.NewGuid();
    public int AreaNr { get; private set; }
    public int RowLength { get; private set; }
    public int RowNr { get; private set; }
    
    //interface segregation
    private readonly List<Seat>? _seats = new List<Seat>();
    public IReadOnlyList<Seat>? Seats => _seats.AsReadOnly();
    
    

    public Area(Guid id, int areaNr, int rowLength, int rowNr)
    {
        //Check of rijen voldoen aan eisen
        if (rowNr < 1 || rowNr > 3)
            throw new
                ArgumentException(nameof(Area), "De rijen moeten tussen 1 en 3 blijven");
        if (rowLength < 1 || RowNr > 12)
            throw new
                ArgumentException(nameof(Area), "De rijlengte moet tussen de 1 en de 12 zijn");


        Id = id;
        AreaNr = areaNr;
        RowLength = rowLength;
        RowNr = rowNr;
        // _seats = seats;

        CreateSeat();
    }


    //Get Seats
    public List<Seat> GetSeats()
    {
        // List<Seat> seats = new List<Seat>();
        return _seats.Where(seat => seat.Visitors == null).ToList();
    }


    //Create Seats
    public void CreateSeat()
    {
        //Create seats
        for (int i = 0; i < RowNr; i++)
        {
            RowNr++;
            for (int x = 0; x < RowLength; x++)
            {
                RowLength++;
                _seats.Add(new Seat(new Guid(), i, x, true));
            }
        }
    }


    //Populate seats
    public Group PopulateSeats(Group group, DateOnly date)
    {
        if (GetSeats().Count < group.Visitors?.Count)
        {
            return group;
        }

        List<Seat> AvailableSeats = GetSeats();
        List<Visitor> childeren;
        List<Visitor> adults;


        return group;
    }
}