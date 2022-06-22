using System.ComponentModel.DataAnnotations;

namespace VisitorPlacementTool.BLL.Entities
{
    public class Organizer
    {
        [Key] public Guid Id { get; private set; } = Guid.NewGuid();
        public string? Name { get; private set; }

        private readonly List<Visitor>? _visitors = new List<Visitor>();
        public IReadOnlyList<Visitor>? Visitors => _visitors?.AsReadOnly();

        private readonly List<Event>? _events = new List<Event>();
        public IReadOnlyList<Event>? Events => _events?.AsReadOnly();

        // public Organizer(Guid id, string name, List<Visitor> visitors, List<Event> events)
        // {
        //     Id = id;
        //     Name = name;
        //     _visitors = visitors;
        //     _events = events;
        // }
        //
    
        //Get Event by ID
        public Event? GetEventById(Guid id)
        {
            return _events!.FirstOrDefault(event_ => event_.Id == id);
        }

        //Get Visitor by ID 
        public Visitor? GetVisitorById(Guid id)
        {
            return _visitors!.FirstOrDefault(visitor_ => visitor_.Id == id);
        }

    
        //Create Events
        public Event AddEvent(string Name, int VisitorLimit, DateOnly dateTime)
        {
            //validation
            if (_events!.Any(_events => _events.Name == Name))
            {
                throw new ArgumentException(nameof(Event), "Dit evenement is al toegevoerd.");
            }

            // Guid id, string name, DateOnly date)
            Event addedEvent = new Event(new Guid(), Name, VisitorLimit, dateTime);
            _events!.Add(addedEvent);

            return addedEvent;
        }
    
        //Create Visitor
        public Guid AddVisitor(string Name, DateOnly dateTime)
        {
            //validation
            if (_visitors!.Any(_visitors => _visitors.Name == Name))
            {
                throw new ArgumentException(nameof(Event), "Deze persoon is al toegevoerd");
            }

            // Guid id, string name, DateOnly birthday)
            Visitor addedvisitor = new Visitor( Name, dateTime);
            _visitors!.Add(addedvisitor);

            return addedvisitor.Id;
        }

    }
}