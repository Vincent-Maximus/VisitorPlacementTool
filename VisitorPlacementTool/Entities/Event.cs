using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using HostingEnvironmentExtensions = Microsoft.AspNetCore.Hosting.HostingEnvironmentExtensions;

namespace VisitorPlacementTool.Entities;

public class Event
{
    [Key] public Guid Id { get; init; } = Guid.NewGuid();
    public string? Name { get; private set; }
    public int VisitorLimit { get; private set; }
    public DateOnly Date { get; private set; }


    private readonly List<Area>? _areas = new List<Area>();
    public IReadOnlyList<Area>? Areas => _areas.AsReadOnly();


    private readonly List<Group>? _groups = new List<Group>();
    public IReadOnlyList<Group>? Groups => _groups.AsReadOnly();


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

        //give rowNr
        int rowNr = 1;
        if (_areas.Count > 0)
        {
            rowNr++;
        }

        //Guid id, int areaNr, int rowLength, int rowNr)
        Area area = new Area(Guid.NewGuid(), RowCount, RowLength, rowNr);
        _areas.Add(area);


        return area;
    }
    

    //Assign Groups To Event
    public void AssignGroupsToEvent(Group group)
    {
        int areaNr = 0;
        //Check if there are already seats occupied in this Area
        foreach (var area in _areas)
        {
            foreach (var seat in area.Seats)
            {
                areaNr++;
                if (seat.Occupied == false)
                {
                    //Over here is a place on the front row

                    //TODO place ppl on front row
                }

                throw new ArgumentException(nameof(Event), "Hier zitten al mensen, kijk naar het volgende vak");
            }

            // throw new ArgumentException(nameof(Event), "Overal zitten al mensen, vooraan zitten is niet meer mogelijk.");
            //Overal zitten al mensen, vooraan zitten is niet meer mogelijk.
            //TODO place ppl in the first area (behind people)
        }

        if (CheckDuplicate(group))
        {
            throw new ArgumentException(nameof(Event), "Deze groep is al toegevoegd aan dit evenement");
        }

        if (group.Visitors.Where(_visitor => _visitor.ChildCheck(Date)).Count() == 0)
        {
            throw new ArgumentException(nameof(Event), "Er moet minimaal 1 volwassene aanwezig zijn");
        }

        _groups.Add(group);
    }

    //Check for dupes in group
    public bool CheckDuplicate(Group group)
    {
        if (_groups!.Contains(group))
        {
            throw new ArgumentException(nameof(Event), "Deze groep is al toegevoegd aan dit evenement");
            return true;
        }

        if (group.Visitors.Any(visitor =>
                _groups.SelectMany(duplicateVisitor => duplicateVisitor.Visitors).Any(_visitor => _visitor == visitor)))
        {
            throw new ArgumentException(nameof(Event), "Deze bezoeker is al toegevoegd aan een groep");
            return true;
        }

        return false;
    }


    
    //TODO PlaceVisitorsFromGroupToSeats
    public void PlaceVisitorsFromGroupToSeats()
    {
        //foreach group
        foreach (Group group in _groups.OrderBy(_groups => _groups.RegisterTime))
        {
            List<Visitor> childern = group.Visitors.Where(_visitor => !_visitor.ChildCheck(Date)).ToList();

            IOrderedEnumerable<Area> orderdArea = Areas.OrderBy(areas =>
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

            Group groups = group;
            
           

            foreach (Area area in orderdArea)
            {
                groups = area.PopulateSeats(groups, Date);

                
                //TODO re-sort to groups with kids
                //foreach group where children == true
                //foreach group where children == false
                //place those^ groups in order 
                
                if (groups.Visitors.Count != 0) ;
                {
                    if (groups.Visitors.Count ! > 0)
                    {
                        _groups.Add(group);
                    }
                    else
                    {
                        throw new ArgumentException(nameof(Event), "Deze groep pas helaas niet");
                        //End of the line sir
                        
                        //FUTURE ADDITION
                        //TODO re-sort/ reposition group to available seats 
                    }
                }
                break;
            }
            
            //remove all leftovers
            _areas.RemoveAll(_areas.Contains);
        }
    }

    
    
    //TODO Get Groups
    //TODO Create Groups
}