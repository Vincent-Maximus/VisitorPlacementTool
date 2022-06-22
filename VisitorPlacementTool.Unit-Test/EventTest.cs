using VisitorPlacementTool.BLL.Entities;

namespace VisitorPlacementTool.Unit_Test;

[TestClass]
public class EventTest
{
    //The tests xD
    [TestMethod]
    public void CreateArea_ReturnCreateArea_Test()
    {
        // Dummy Data
        Organizer organizer = new Organizer();
        Event event_ = organizer.AddEvent("test", 200, new DateOnly(2022, 02, 24));
        int RowLength = 10;
        int RowCount = 2;

        //Run
        event_.CreateArea(RowLength, RowCount);

        //Test

        Assert.AreEqual(RowLength, event_.Areas!.First().RowLength);
    }

    //The tests xD
    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "Het Maximaal aantal stoelen in deze rij is bereikt")]
    public void CreateArea_ReturnArgumentExeption_MaxSeats_Test()
    {
        // Dummy Data
        Organizer organizer = new Organizer();
        Event event_ = organizer.AddEvent("test", 10, new DateOnly(2022, 02, 24));
        int RowLength = 20;
        int RowCount = 1;

        //Run
        event_.CreateArea(RowLength, RowCount);

        //Test
        // Assert.AreEqual(RowLength, event_.Areas!.First().RowLength);
    }

    //The tests xD
    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "Het Maximaal aantal rijen is bereikt")]
    public void CreateArea_ReturnArgumentExceptionMaxRows_Test()
    {
        // Dummy Data
        Organizer organizer = new Organizer();
        Event event_ = organizer.AddEvent("test", 10, new DateOnly(2022, 02, 24));
        int RowLength = 20;
        int RowCount = 1;

        //Run
        int count = 31;
        for (int i = 0; i < count; i++)
        {
            event_.CreateArea(RowLength, RowCount);
        }
    }

    //The tests xD
    [TestMethod]
    public void AssignGroupsToEvent_returnAddedGroup_Test()
    {
        // Dummy Data
        Organizer organizer = new Organizer();
        Event event_ = organizer.AddEvent("test", 200, new DateOnly(2022, 02, 24));
        int RowLength = 10;
        int RowCount = 2;

        event_.CreateArea(RowLength, RowCount);

        Guid visitor1 = organizer.AddVisitor("Noa", new DateOnly(1998, 08, 09)); // Adult
        Guid visitor2 = organizer.AddVisitor("Robin", new DateOnly(2015, 03, 14)); // Child
        Guid visitor3 = organizer.AddVisitor("Els", new DateOnly(1968, 05, 31)); // Adult

        Group group = new Group(new Guid(), new DateOnly(2022, 01, 17), new List<Visitor>
        {
            organizer.GetVisitorById(visitor1)!,
            organizer.GetVisitorById(visitor2)!,
            organizer.GetVisitorById(visitor3)!,
        });

        //Run
        event_.AssignGroupsToEvent(group);

        //Test
        Assert.AreEqual(1, event_.Assigned.Count);
    }

    //The tests xD
    [TestMethod]
    public void AssignGroupsToEvent_returnsAddedGroups_Test()
    {
        // Dummy Data
        Organizer organizer = new Organizer();
        Event event_ = organizer.AddEvent("test", 200, new DateOnly(2022, 02, 24));
        int RowLength = 10;
        int RowCount = 2;

        event_.CreateArea(RowLength, RowCount);

        Guid visitor1 = organizer.AddVisitor("Noa", new DateOnly(1998, 08, 09)); // Adult
        Guid visitor2 = organizer.AddVisitor("Robin", new DateOnly(2015, 03, 14)); // Child
        Guid visitor3 = organizer.AddVisitor("Els", new DateOnly(1968, 05, 31)); // Adult

        Group first_group = new Group(new Guid(), new DateOnly(2022, 01, 17), new List<Visitor>
        {
            organizer.GetVisitorById(visitor1)!,
            organizer.GetVisitorById(visitor2)!,
        });
        Group second_group = new Group(new Guid(), new DateOnly(2022, 01, 17), new List<Visitor>
        {
            organizer.GetVisitorById(visitor3)!,
        });

        //Run
        event_.AssignGroupsToEvent(first_group);
        event_.AssignGroupsToEvent(second_group);

        //Test
        Assert.AreEqual(2, event_.Assigned.Count);
    }

    //The tests xD
    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "Er moet minimaal 1 volwassene aanwezig zijn")]
    public void AssignGroupsToEvent_returnsArgumentException_Need1Adult_Test()
    {
        // Dummy Data
        Organizer organizer = new Organizer();
        Event event_ = organizer.AddEvent("test", 200, new DateOnly(2022, 02, 24));
        int RowLength = 10;
        int RowCount = 2;

        event_.CreateArea(RowLength, RowCount);

        Guid visitor1 = organizer.AddVisitor("Noa", new DateOnly(2010, 08, 09)); // Adult
        Guid visitor2 = organizer.AddVisitor("Robin", new DateOnly(2015, 03, 14)); // Child

        Group first_group = new Group(new Guid(), new DateOnly(2022, 01, 17), new List<Visitor>
        {
            organizer.GetVisitorById(visitor1)!,
            organizer.GetVisitorById(visitor2)!,
        });

        //Run
        event_.AssignGroupsToEvent(first_group);
    }

    //The tests xD
    [TestMethod]
    // [ExpectedException(typeof(ArgumentException),"Deze bezoeker is al toegevoegd aan een groep")]
    public void PlaceVisitorsFromGroupToSeats_returns_AddedPlaceVisitorsFromGroupToSeats_Test()
    {
        // Dummy Data
        Organizer organizer = new Organizer();
        Event event_ = organizer.AddEvent("test", 200, new DateOnly(2022, 02, 24));
        int RowLength = 10;
        int RowCount = 2;

        event_.CreateArea(RowLength, RowCount);

        Guid visitor1 = organizer.AddVisitor("Noa", new DateOnly(1998, 08, 09)); // Adult
        Guid visitor2 = organizer.AddVisitor("Robin", new DateOnly(2015, 03, 14)); // Child
        Guid visitor3 = organizer.AddVisitor("Els", new DateOnly(1968, 05, 31)); // Adult

        Group first_group = new Group(new Guid(), new DateOnly(2022, 01, 17), new List<Visitor>
        {
            organizer.GetVisitorById(visitor1)!,
            organizer.GetVisitorById(visitor2)!,
            organizer.GetVisitorById(visitor3)!,
        });

        event_.AssignGroupsToEvent(first_group);

        //Run
        event_.PlaceVisitorsFromGroupToSeats();

        //Test
        Assert.AreEqual(1, event_.Groups.Count);
    }
}