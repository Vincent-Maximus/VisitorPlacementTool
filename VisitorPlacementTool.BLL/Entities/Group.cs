using System.ComponentModel.DataAnnotations;

namespace VisitorPlacementTool.BLL.Entities
{
    public class Group
    {
        [Key]
        public Guid Id { get; init; } = Guid.NewGuid();
        public DateOnly RegisterTime { get; init; }

        private readonly List<Visitor>? _visitors = new List<Visitor>();
        public IReadOnlyList<Visitor>? Visitors => _visitors.AsReadOnly();
    
    
        public Group(Guid id, DateOnly registerTime, List<Visitor> visitors)
        {
            Id = id;
            RegisterTime = registerTime;
            _visitors.AddRange(visitors);
        }

        //Moved responsibility to organizer
        //TODO Get Visitors
        //TODO Create Visitors
    }
}