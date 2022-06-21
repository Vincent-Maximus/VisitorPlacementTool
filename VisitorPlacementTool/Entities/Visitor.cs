using System.ComponentModel.DataAnnotations;

namespace VisitorPlacementTool.Entities;

public class Visitor
{
    [Key] public Guid Id { get; init; } = Guid.NewGuid();
    public string? Name { get; private set; }
    public DateOnly Birthday { get; private set; }

    public Visitor(Guid id, string name, DateOnly birthday)
    {
        Id = id;
        Name = name;
        Birthday = birthday;
    }
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