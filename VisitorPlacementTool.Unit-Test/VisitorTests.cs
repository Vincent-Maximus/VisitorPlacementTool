using VisitorPlacementTool.Entities;

namespace VisitorPlacementTool.Unit_Test;

[TestClass]
public class VisitorTests
{
    private readonly Visitor _sut;
    public VisitorTests()
    {
        _sut = new Visitor();
    }
    
    
    //The tests xD
    [TestMethod]
    public void ChildCheck_ShouldReturnTrue()
    {
        // Dummy Data
        var child = new DateOnly(2020, 10, 19);
        var adult = new DateOnly(2000, 10, 19);
        
        //Run
        var isChild = _sut.ChildCheck(child);

        //Test
        Assert.IsTrue(isChild);
    }
    //The tests xD
    [TestMethod]
    public void ChildCheck_ShouldReturnFalse()
    {
        // Dummy Data
        var child = new DateOnly(2020, 10, 19);
        var adult = new DateOnly(2000, 10, 19);
        
        //Run
        var isChild = _sut.ChildCheck(adult);

        //Test
        Assert.IsFalse(isChild);
    }
}