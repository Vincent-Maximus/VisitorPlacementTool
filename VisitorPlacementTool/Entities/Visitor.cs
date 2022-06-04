using System.ComponentModel.DataAnnotations;

namespace VisitorPlacementTool.Entities;

public class Visitor
{
    [Key] public Guid Id { get; set; } = Guid.NewGuid();
    public string? Name { get; set; }
    public DateOnly Birthday { get; set; }


    public bool ChildCheck(DateOnly Birthday)
    {
        double birthday = (int.Parse(DateTime.Now.ToString("yyyyMMdd")) - int.Parse(Birthday.ToString("yyyyMMdd")))/10000;
        if (birthday >= 12)
        {
            //Adult
            return false;
        }
        else
        {
            //Adult
            return true;
        }
    }
}