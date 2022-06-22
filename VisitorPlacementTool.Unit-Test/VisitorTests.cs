// using VisitorPlacementTool..Entities;

using VisitorPlacementTool.BLL.Entities;

namespace VisitorPlacementTool.Unit_Test;

[TestClass]
public class VisitorTests
{
    // private readonly Visitor _sut;
    public VisitorTests()
    {
        // _sut = new Visitor();
    }
    
    
    //The tests xD
    [TestMethod]
    public void ChildCheck_ShouldReturnTrue()
    {
        // Dummy Data
        
        //This is an Adult
        string Birthdaystr = "01/10/2000";
        string currentDateString = "02/5/2022";

        if (!DateOnly.TryParse(Birthdaystr, out DateOnly BirthdayResult)) Assert.Fail("Could not parse birthday");
        if (!DateOnly.TryParse(currentDateString, out DateOnly currentDateResult)) Assert.Fail("Could not parse current date");

        Visitor visitor = new Visitor("Name", BirthdayResult); 
        
        //Run
        bool isAdult = visitor.ChildCheck(currentDateResult);

        //Test
        Assert.IsTrue(isAdult);
    }
    
    [TestMethod]
    public void ChildCheck_ShouldReturnFalse()
    {
        // Dummy Data
        
        //This is an Adult
        string Birthdaystr = "01/10/2020";
        string currentDateString = "02/5/2022";

        if (!DateOnly.TryParse(Birthdaystr, out DateOnly BirthdayResult)) Assert.Fail("Could not parse birthday");
        if (!DateOnly.TryParse(currentDateString, out DateOnly currentDateResult)) Assert.Fail("Could not parse current date");

        Visitor visitor = new Visitor("Name", BirthdayResult); 
        
        //Run
        bool isAdult = visitor.ChildCheck(currentDateResult);

        //Test
        Assert.IsFalse(isAdult);
    }
    [TestMethod]
    public void ChildCheck_ShouldReturnTrue_SameDay()
    {
        // Dummy Data
        
        //This is an Adult
        string Birthdaystr = "02/05/2010";
        string currentDateString = "02/05/2022";

        if (!DateOnly.TryParse(Birthdaystr, out DateOnly BirthdayResult)) Assert.Fail("Could not parse birthday");
        if (!DateOnly.TryParse(currentDateString, out DateOnly currentDateResult)) Assert.Fail("Could not parse current date");

        Visitor visitor = new Visitor("Name", BirthdayResult); 
        
        //Run
        bool isAdult = visitor.ChildCheck(currentDateResult);

        //Test
        Assert.IsTrue(isAdult);
    }
}