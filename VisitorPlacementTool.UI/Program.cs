using VisitorPlacementTool.BLL.Entities;
using VisitorPlacementTool.UI;


class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Aanschouw het 8e wereld wonder! de Visitor Placement Tool, Door: Vincent Maximus.");


        Organizer organizer = new Organizer();
        Event event_ = organizer.AddEvent("F1 Zantvoord", 200, new DateOnly(2022, 02, 24));

        //Remove dummy data
        //TODO Create loop with Random data
        event_.CreateArea(10, 2);
        event_.CreateArea(5, 1);
        event_.CreateArea(6, 2);
        event_.CreateArea(7, 3);
        event_.CreateArea(9, 2);
        event_.CreateArea(10, 1);
        
        //Remove dummy data
        //TODO Create loop with Random data
        Guid visitor1 = organizer.AddVisitor("Noa", new DateOnly(1998, 08, 09)); // Adult
        Guid visitor2 = organizer.AddVisitor("Robin", new DateOnly(2015, 03, 14)); // Child
        Guid visitor3 = organizer.AddVisitor("Els", new DateOnly(1968, 05, 31)); // Adult
        Guid visitor4 = organizer.AddVisitor("Suzanna", new DateOnly(2018, 02, 06)); // Child
        Guid visitor5 = organizer.AddVisitor("Dana", new DateOnly(2020, 11, 26)); // Child
        Guid visitor6 = organizer.AddVisitor("Diantha", new DateOnly(2005, 12, 03)); // Adult
        Guid visitor7 = organizer.AddVisitor("Regina", new DateOnly(2006, 07, 10)); // Adult
        Guid visitor8 = organizer.AddVisitor("Max", new DateOnly(2021, 02, 23)); // Child
        Guid visitor9 = organizer.AddVisitor("Elyse", new DateOnly(1997, 04, 12)); // Adult
        Guid visitor10 = organizer.AddVisitor("Rachel", new DateOnly(1997, 04, 12)); // Adult
        
        // Guid visitor10 = organizer.AddVisitor("Mendy", new DateOnly(2000, 01, 03)); // Adult
        // Guid visitor11 = organizer.AddVisitor("hendy", new DateOnly(2000, 01, 03)); // Adult
        // Guid visitor12 = organizer.AddVisitor("Vincent", new DateOnly(2021, 02, 23)); // Child
        // Guid visitor13 = organizer.AddVisitor("Maximus", new DateOnly(2021, 02, 23)); // Child
        // Guid visitor14 = organizer.AddVisitor("Wilbert", new DateOnly(2021, 02, 23)); // Child

        //Remove dummy data
        //TODO Create loop with Random data
        Group first_group = new Group(new Guid(), new DateOnly(2022, 01, 17), new List<Visitor>
        {
            organizer.GetVisitorById(visitor1)!,
            organizer.GetVisitorById(visitor2)!,
            organizer.GetVisitorById(visitor3)!,
            organizer.GetVisitorById(visitor4)!,
            organizer.GetVisitorById(visitor5)!,
            organizer.GetVisitorById(visitor6)!,
            organizer.GetVisitorById(visitor7)!,
            organizer.GetVisitorById(visitor8)!,
            organizer.GetVisitorById(visitor9)!,
            organizer.GetVisitorById(visitor10)!,
        });
        
        // Group second_group = new Group(new Guid(), new DateOnly(2022, 01, 17), new List<Visitor>
        // {
        //     organizer.GetVisitorById(visitor10)!,
        //     organizer.GetVisitorById(visitor11)!,
        //     organizer.GetVisitorById(visitor12)!,
        //     organizer.GetVisitorById(visitor13)!,
        //     organizer.GetVisitorById(visitor14)!,
        // });


        event_.AssignGroupsToEvent(first_group);
        event_.PlaceVisitorsFromGroupToSeats();

        new UserInterface().VisualizeToConsole(event_);
    }
}


//TODO add event

//TODO create areas

//20x
//TODO add Visitors

//4x - 5x
//TODO Add visitors to Group


// Show Layout
// new UserInterface().VisualizeToConsole(event1);