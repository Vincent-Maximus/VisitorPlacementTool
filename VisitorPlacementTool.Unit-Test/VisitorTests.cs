using VisitorPlacementTool.Entities;

namespace VisitorPlacementTool.Unit_Test;

[TestClass]
public class VisitorTests
{
    private readonly Visitor _sut;
    public VisitorTests()
    {
        // _sut = new Visitor();
    }
    
    
    //The tests xD
    [TestMethod]
    public void ChildCheck_ShouldReturnTrue()
    {
        //This is an child
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
        //This is an adult
        // Dummy Data
        var child = new DateOnly(2020, 10, 19);
        var adult = new DateOnly(2000, 10, 19);
        
        //Run
        var isChild = _sut.ChildCheck(adult);

        //Test
        Assert.IsFalse(isChild);
    }
}