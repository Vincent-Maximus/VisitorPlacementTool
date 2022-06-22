using VisitorPlacementTool.BLL.Entities;

namespace VisitorPlacementTool.Unit_Test;

[TestClass]
public class OrganizerTests
{
    //The tests xD
    [TestMethod]
    public void AddEvent_ReturnsAddedEvent_Test()
    {
        // Dummy Data
        string name = "F2 Spa Racetrack";
        int visitorLimit = 200;
        DateOnly date = new DateOnly(2022, 02, 24);


        //Run
        Organizer organizer = new Organizer();
        Event event_ = organizer.AddEvent(name, visitorLimit, date);
        
        //Test
        Assert.AreEqual(name, event_.Name);
    }
    
    //The tests xD
    [TestMethod]
    [ExpectedException(typeof(ArgumentException),"Dit evenement is al toegevoerd.")]
    public void AddEvent_ReturnsDuplicateArgumentException_Test()
    {
        // Dummy Data
        string name = "F2 Spa Racetrack";
        int visitorLimit = 200;
        DateOnly date = new DateOnly(2022, 02, 24);


        Organizer organizer = new Organizer();
        //Run
        Event event_ = organizer.AddEvent(name, visitorLimit, date);
        Event event_2 = organizer.AddEvent(name, visitorLimit, date);
    }
    
  
    //The tests xD
    [TestMethod]
    public void AddVisitor_ReturnsAddVisitor_Test()
    {
        // Dummy Data
        string name = "Noah";
        DateOnly date = new DateOnly(1998, 08, 09);


        //Run
        Organizer organizer = new Organizer();
        Guid visitor1 = organizer.AddVisitor(name , date); // Adult
        
        
        //Test
        Assert.AreEqual(name, organizer.Visitors.First().Name);
    }
    
    //The tests xD
    [TestMethod]
    [ExpectedException(typeof(ArgumentException),"Deze persoon is al toegevoerd")]
    public void AddVisitor_ReturnsDuplicateArgumentException_Test()
    {
        // Dummy Data
        string name = "Noah";
        DateOnly date = new DateOnly(1998, 08, 09);


        //Run
        Organizer organizer = new Organizer();
        Guid visitor1 = organizer.AddVisitor(name , date); // Adult
        Guid visitor2 = organizer.AddVisitor(name , date); // Adult
        
    }
    
    //The tests xD
    [TestMethod]
    public void GetEventById_returnsRequested_id()
    {
        // Dummy Data
        string name = "F2 Spa Racetrack";
        int visitorLimit = 200;
        DateOnly date = new DateOnly(2022, 02, 24);

        
        //Run
        Organizer organizer = new Organizer();
        
        //Create Event
        Event event_ = organizer.AddEvent(name, visitorLimit, date);
        Guid EventId = organizer.Events.First().Id;
        
        
        Event event_1 = organizer.GetEventById(EventId);
        
        //Test
        Assert.AreEqual(EventId, event_1.Id);
    }

}