using VisitorPlacementTool.Entities;

namespace VisitorPlacementTool.Unit_Test;

[TestClass]
public class AreaTests
{
    private readonly Area _sut;

    public AreaTests()
    {
        // public Area(Guid id, int areaNr, int rowLength, int rowNr)

        Guid id = new Guid();
        int areaNr = 2;
        int rowLength = 8;
        int rowNr = 3;
        
        _sut = new Area(id, areaNr, rowLength, rowNr);
    }


    //The tests xD
    [TestMethod]
    public void GetSeats_ShouldReturn_AmountOfAvailableSeats()
    {
        // Dummy Data
        Guid id = new Guid();
        int areaNr = 2;
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
    public void GetSeats_ShouldReturn_Null()
    {
        // Dummy Data
        Guid id = new Guid();
        int areaNr = 2;
        int rowLength = 20;
        int rowNr = 4;
        
        Area area = new Area(id, areaNr, rowLength, rowNr);

        //Run
        int getSeats = area.GetSeats().Count;
        

        //Test
        Assert.IsNull(getSeats);
    }
}