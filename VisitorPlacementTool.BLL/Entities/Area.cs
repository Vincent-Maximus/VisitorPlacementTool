using System.ComponentModel.DataAnnotations;
using VisitorPlacementTool.BLL.Entities;

namespace VisitorPlacementTool.BLL.Entities;

public class Area
{
    [Key] public Guid Id { get; private set; } = Guid.NewGuid();
    public char AreaNr { get; private set; }
    public int RowLength { get; private set; }
    public int RowNr { get; private set; }
// SOLID
    //interface segregation
    private readonly List<Seat>? _seats = new List<Seat>();
    public IReadOnlyList<Seat>? Seats => _seats.AsReadOnly();


    public Area(Guid id, char areaNr, int rowLength, int rowNr)
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
        return _seats!.Where(seat => seat.Visitors == null).ToList();
    }


    //Create Seats
    public void CreateSeat()
    {
        //Create seats
        for (int i = 1; i < (RowNr + 1); i++)
        {
            // RowNr++;
            for (int x = 1; x < (RowLength + 1); x++)
            {
                // RowLength++;
                _seats!.Add(new Seat(new Guid(), i, x, true));
            }
        }
    }


    //TODO MAKE freaking YR OWN

    public Group PopulateSeats(Group group, DateOnly eventDate)
    {
        // Check if enough seats are available
        if (GetSeats().Count < group.Visitors?.Count)
        {
            return group;
        }


        List<Seat> availableSeats = GetSeats();
        // List<Visitor> remainingVisitors = group.Visitors!.ToList();
        List<Visitor> pendingChildren = group.Visitors.Where(vistor => !vistor.ChildCheck(eventDate)).ToList();
        List<Visitor> pendingAdults = group.Visitors.Where(vistor => vistor.ChildCheck(eventDate)).ToList();

        //look for a better area, with more then 1 row
        if (RowNr == 1 && pendingChildren.Count == 0)
        {
            return group;
        }


        // Group will not fit on here, since other adults will be placed on second row
        if (RowNr == 1 && pendingChildren.Count > 0 && pendingAdults.Count > 1)
        {
            return group;
        }
        
        // foreach (Seat seat in availableSeats)
        // {
        //     //assign seat
        //     if (remainingVisitors.Count != 0)
        //     {
        //         seat.Visitors = remainingVisitors.First();
        //         remainingVisitors.RemoveAt(0);
        //     }
        //     else
        //     {
        //         break;
        //     }
        // }
        
        // check for kids
        if (pendingChildren.Count > 0)
        {
            List<Seat> availableSeatsOnFirstRow = availableSeats.Where(seat => seat.SeatNr == 1).ToList();

            if (availableSeatsOnFirstRow.Count == 0)
            {
                return group;
            }

            //TODO divide groups over rows
            if (availableSeatsOnFirstRow.Count < (pendingChildren.Count + 1))
            {
                return group;
            }

            //Place kids
            foreach (Seat seat in availableSeatsOnFirstRow)
            {
                if (pendingChildren.Count == 0) break;
                seat.Visitors = pendingChildren.First();
                pendingChildren.RemoveAt(0);
            }

            availableSeatsOnFirstRow.RemoveAll(seat => seat.Visitors != null);
            availableSeatsOnFirstRow.First().Visitors = pendingAdults.First();
            pendingAdults.RemoveAt(0);
            
            //Place the rest 
            availableSeats = GetSeats().Where(seat => seat.SeatNr != 1).OrderBy(seat => seat.SeatNr).ToList();
            
            foreach (Seat seat in availableSeats)
            {
                if (pendingAdults.Count == 0) break;
                seat.Visitors = pendingAdults.First();
                pendingAdults.RemoveAt(0);
            }
        }
        
        //adults only
        else
        {
            List<Seat> availableSeatsFromSecondRow = availableSeats.Where(seat => seat.SeatNr != 1).ToList();

            if (availableSeatsFromSecondRow.Count == 0)
            {
                return group;
            }

            foreach (Seat seat in availableSeatsFromSecondRow)
            {
                if (pendingAdults.Count == 0) break;
                seat.Visitors = pendingAdults.First();
                pendingAdults.RemoveAt(0);
            }
        }

        List<Visitor> unableToPlace = new List<Visitor>();

        unableToPlace.AddRange(pendingAdults);
        unableToPlace.AddRange(pendingChildren);

        return new Group(new Guid(), group.RegisterTime, unableToPlace)
        {
            Id = group.Id
        };
        
        // List<Visitor> unableToPlace = new List<Visitor>();
        //
        // unableToPlace.AddRange(remainingVisitors);
        //
        // return new Group(group.Id, date, unableToPlace);
    }

}