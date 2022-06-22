using System.ComponentModel.DataAnnotations;

namespace VisitorPlacementTool.BLL.Entities
{
    public class Visitor
    {
        [Key] public Guid Id { get; private set; } = Guid.NewGuid();
        public string? Name { get; private set; }
        public DateOnly Birthday { get; private set; }

        public Visitor(string name, DateOnly birthday)
        {
            // Id = id;
            Name = name;
            Birthday = birthday;
        }
        public bool ChildCheck(DateOnly eventDate)
        {
            double Date = (int.Parse(eventDate.ToString("yyyyMMdd")) - int.Parse(Birthday.ToString("yyyyMMdd")))/10000;
            if (Date >= 12)
            {
                //kind
                return true;
            }
            else
            {
                //Adult
                return false;
            }
        }
    }
}