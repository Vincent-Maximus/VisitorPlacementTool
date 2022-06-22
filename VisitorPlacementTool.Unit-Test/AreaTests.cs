using VisitorPlacementTool.BLL.Entities;

namespace VisitorPlacementTool.Unit_Test;

[TestClass]
public class AreaTests
{
    private readonly Area _sut;
    
    //The tests xD
    [TestMethod]
    public void GetSeats_ShouldReturn_AmountOfAvailableSeats()
    {
        // Dummy Data
        Guid id = new Guid();
        char areaNr = 'b';
        int rowLength = 10;
        int rowNr = 3;
        
        Area area = new Area(id, areaNr, rowLength, rowNr);

        //Run
        int getSeats = area.GetSeats().Count;
        

        //Test
        Assert.AreEqual(rowLength * rowNr, getSeats);
    }
    
    
    //The tests xD
    [TestMethod]
    public void GetAvailableSeats_ReturnsAllTenSeats()
    {
        // Dummy Data
        Organizer organizer = new Organizer();
        Event event_ = organizer.AddEvent("F1 Zantvoord", 200, new DateOnly(2022, 02, 24));
        
        
        Guid id = new Guid();
        char areaNr = 'b';
        int rowLength = 5;
        int rowNr = 2;
        Area area = new Area(id, areaNr, rowLength, rowNr);
        
        
        //Run
        int getSeats = area.GetSeats().Count;

        //Test
        Assert.AreEqual(10, getSeats);
    }
    
    
    //The tests xD
    [TestMethod]
    public void GetAvailableSeats_ReturnsNineSeats()
    {
        // Dummy Data
        Organizer organizer = new Organizer();
        Event event_ = organizer.AddEvent("F1 Zantvoord", 200, new DateOnly(2022, 02, 24));
        
        int rowLength = 5;
        int rowNr = 2;
        //int RowLength, int RowCount
        event_.CreateArea(rowLength, rowNr);
        
        
        Guid visitor1 = organizer.AddVisitor("John Wick", new DateOnly(2001, 02, 12)); // Adult

        Group group = new Group(new Guid(), new DateOnly(2022, 01, 17), new List<Visitor>
        {
            organizer.GetVisitorById(visitor1)!,
        });

        event_.AssignGroupsToEvent(group);
        event_.PlaceVisitorsFromGroupToSeats();

        //Run
        // int getSeats = area.GetSeats().Count;
        int getSeats = event_.Areas.First().GetSeats().Count;

        //Test
        Assert.AreEqual(9, getSeats);
    }
}