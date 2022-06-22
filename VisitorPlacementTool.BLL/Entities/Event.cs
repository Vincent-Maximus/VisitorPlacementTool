using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Linq.Expressions;

namespace VisitorPlacementTool.BLL.Entities;

public class Event
{
    [Key] public Guid Id { get; private set; } = Guid.NewGuid();
    public string? Name { get; private set; }
    public int VisitorLimit { get; private set; }
    public DateOnly Date { get; private set; }


    private readonly List<Area>? _areas = new List<Area>();
    public IReadOnlyList<Area>? Areas => _areas.AsReadOnly();


    private readonly List<Group>? _groups = new List<Group>();
    public IReadOnlyList<Group>? Groups => _groups.AsReadOnly();

    private readonly List<Group> _assigned = new List<Group>();
    public IReadOnlyList<Group> Assigned => _assigned.AsReadOnly();

    public Event(Guid id, string name, int visitorLimit, DateOnly date)
    {
        Id = id;
        Name = name;
        VisitorLimit = visitorLimit;
        Date = date;
    }


    //Get all areas
    public List<Area> GetAreas()
    {
        return _areas!;
    }


    //Create area
    public Area CreateArea(int RowLength, int RowCount)
    {
        //max row height & row length
        if (_areas!.Count == 30)
            throw new ArgumentException(nameof(Event), "Het Maximaal aantal rijen is bereikt");


        //check if visitor limit reached
        int remainingAvailableSeats = (VisitorLimit - _areas.Sum(area => area.Seats!.Count));
        if (remainingAvailableSeats == 0 || (RowCount * RowLength) > remainingAvailableSeats)
        {
            throw new ArgumentException(nameof(Event), "Het Maximaal aantal stoelen in deze rij is bereikt");
        }

        
        // int rowNr = _areas.Last().AreaNr;
        // int rowNr = 1;
        // if (_areas.Count > 0)
        // {
        //     rowNr++;
        //     
        // }
        
        //List of letters
        List<char> areaChars = "abcdefghijklmnopqrstuvwxyz".ToCharArray().ToList();

        // char currentChar = "a";
        char areaChar_ = 'a';

        //Loop through letters 
        if (_areas.Count > 0)
        {
            char areaChar = _areas.Last().AreaNr;
            int pIndex = areaChars.IndexOf(areaChar);
            areaChar_ = areaChars[pIndex + 1];
        }
        
        //Guid id, int areaNr, int rowLength, int rowNr)
        Area area = new Area(Guid.NewGuid(), areaChar_, RowLength, RowCount);
        _areas.Add(area);


        return area;
    }
    

    //Assign Groups To Event
    public void AssignGroupsToEvent(Group group)
    {
        //Check if there are already seats occupied in this Area
        //Get groups
        var groups_ = _groups.Sum(group => group.Visitors.Count) + group.Visitors.Count;
        
        //Get areas
        var area = _areas.Sum(_areas => _areas.RowNr * _areas.RowLength);
        
        // if (groups_ > area)
        // {
        //     throw new ArgumentException(nameof(Event), "Alle stoelen zijn al bezet");
        // }
        
        if (CheckDuplicate(group))
        {
            throw new ArgumentException(nameof(Event), "Deze groep is al toegevoegd aan dit evenement");
        }
        
        if (group.Visitors.Where(_visitor => _visitor.ChildCheck(Date)).Count() == 0)
        {
            throw new ArgumentException(nameof(Event), "Er moet minimaal 1 volwassene aanwezig zijn");
        }
        
        _assigned.Add(group);
        
        // int areaNr = 0;
        // //Check if there are already seats occupied in this Area
        // foreach (Area area in _areas)
        // {
        //     foreach (Seat seat in area.Seats)
        //     {
        //         areaNr++;
        //         if (seat.Occupied == false)
        //         {
        //             //Over here is a place on the front row
        //
        //             //TODO place ppl on front row
        //         }
        //
        //         throw new ArgumentException(nameof(Event), "Hier zitten al mensen, kijk naar het volgende vak");
        //     }
        //
        //     throw new ArgumentException(nameof(Event), "Overal zitten al mensen, vooraan zitten is niet meer mogelijk.");
        //     //Overal zitten al mensen, vooraan zitten is niet meer mogelijk.
        //     //TODO place ppl in the first area (behind people)
        // }
    }

    //Check for dupes in group
    public bool CheckDuplicate(Group group)
    {
        if (_groups!.Contains(group))
        {
            // throw new ArgumentException(nameof(Event), "Deze groep is al toegevoegd aan dit evenement");
            return true;
        }

        if (group.Visitors.Any(visitor =>
                _groups.SelectMany(duplicateVisitor => duplicateVisitor.Visitors).Any(_visitor => _visitor == visitor)))
        {
            // throw new ArgumentException(nameof(Event), "Deze bezoeker is al toegevoegd aan een groep");
            return true;
        }

        return false;
    }


    
    //TODO PlaceVisitorsFromGroupToSeats
    public void PlaceVisitorsFromGroupToSeats()
    {
        //foreach group
        foreach (Group group in _assigned.OrderBy(_groups => _groups.RegisterTime))
        {
            List<Visitor> childern = group.Visitors.Where(_visitor => !_visitor.ChildCheck(Date)).ToList();

            IOrderedEnumerable<Area> sortedArea = Areas.OrderBy(areas =>
            {
                if (areas.GetSeats().Count == (areas.RowLength * areas.RowNr))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            });

            Group pendingGroup = group;
            
           

            foreach (Area area in sortedArea)
            {
                
                pendingGroup = area.PopulateSeats(pendingGroup, Date);
                // if (pendingGroup.Visitors.Count == 0) break;

                //TODO re-sort to groups with kids
                //foreach group where children == true
                //foreach group where children == false
                //place those^ groups in order 
            }
            
            if (pendingGroup.Visitors.Count > 0) 
            {
                throw new ArgumentException(nameof(Event), "Deze groep pas helaas niet");
                //End of the line sir
                        
                //FUTURE ADDITION
                //TODO re-sort/ reposition group to available seats
            }
            else
            {
                _groups.Add(group);
            }
            
            
            //remove all leftovers
            _assigned.RemoveAll(_groups.Contains);
        }
    }

    
    
    //TODO Get Groups
    //TODO Create Groups
}