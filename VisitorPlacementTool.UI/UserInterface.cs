using Spectre.Console;
using VisitorPlacementTool.BLL.Entities;

namespace VisitorPlacementTool.UI;

public class UserInterface
{
    public void VisualizeToConsole(Event _event)
    {
        Tree tree = new Tree(_event.Name);
        TreeNode areasNode = tree.AddNode("[yellow] areas [/]");
        TreeNode groupsNode = tree.AddNode("[yellow] Groepen [/]");
        
        
        foreach (Area area in _event.Areas)
        {
            TreeNode areaNode = areasNode.AddNode($"Vakken {area.AreaNr} (Rijen: {area.RowNr}, Stoelen per rij: {area.RowLength})");

            int index = 1;
            foreach (Seat seat in area.Seats.OrderBy(seat_ => seat_.SeatRow))
            {
                if (seat.Visitors != null)
                {
                    Group? joinedGroup = _event.Groups.Where(group => group.Visitors.Contains(seat.Visitors)).FirstOrDefault();
                    areaNode.AddNode($"[gray]StoelNr: [/]{area.AreaNr}{area.RowNr} - {index}  - [gray]naam:[/] {seat.Visitors.Name} - {(seat.Visitors.ChildCheck(_event.Date) ? "[green]Volwassenen [/]" : "[yellow]Kind [/]")}");
                    
                }
                else
                {
                    // areaNode.AddNode($"[gray]StoelNr: [/]{area.AreaNr}{area.RowNr} - {index}  - [gray]naam:[/] {seat.Visitors.Name} - {(seat.Visitors.ChildCheck(_event.Date) ? "[green]Volwassenen [/]" : "[yellow]Kind [/]")}");
                    areaNode.AddNode($"[gray]StoelNr: {area.AreaNr}{area.RowNr} - {index} (Empty)[/]");
                }

                index++;
            }
        }

        foreach (Group group in _event.Groups)
        {
            TreeNode groupNode = groupsNode.AddNode($"(Geregistreert po: {group.RegisterTime})");

            foreach (Visitor visitor in group.Visitors)
            {
                // groupNode.AddNode($"{visitor.Name} ({(visitor.ChildCheck(_event.Date) ? "[yellow] Volwassenen [/]" : "Kind")}, Verjaardag: {visitor.Birthday})");

                if (visitor.ChildCheck(_event.Date))
                {
                    groupNode.AddNode($"{visitor.Name} ([green] Volwassenen [/], Verjaardag: {visitor.Birthday})");
                }
                else
                {
                    groupNode.AddNode($"{visitor.Name} ([yellow] Kind [/], Verjaardag: {visitor.Birthday})");
                }
            }
        }


        TreeNode accessDeniedNode = tree.AddNode("[red] Access Denied [/]");

        foreach (Group group in _event.Groups)
        {
            TreeNode groupNode = accessDeniedNode.AddNode($"groep {group.Id} (Geregistreert op: {group.RegisterTime})");

            foreach (Visitor visitor in group.Visitors)
            {
                groupNode.AddNode(
                    $"{visitor.Name} ({(visitor.ChildCheck(_event.Date) ? "Volwassenen" : "Kind")}, Verjaardag: {visitor.Birthday})");
            }
        }


        AnsiConsole.WriteLine();
        AnsiConsole.Write(tree);
    }
}