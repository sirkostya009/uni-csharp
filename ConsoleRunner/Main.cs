using Domain.Logic;
using Domain.Models;
using Domain.Models.Enums;

internal static class Program
{
    public static void Main(string[] args)
    {
        var planner = new SimpleTaskPlanner();
        Console.Write("Enter n: ");
        int n = Convert.ToInt32(Console.ReadLine());

        var enteredItems = from _ in Enumerable.Range(0, n)
                           select readFromConsole();

        foreach (var item in planner.CreatePlan(enteredItems.ToArray()))
        Console.WriteLine(item);
    }

    private static WorkItem readFromConsole()
    {
        Console.WriteLine("Please enter values for a new WorkItem instance.");
        var result = new WorkItem();
        result.CreationDate = DateTime.Now;
        Console.Write("Enter DueDate: ");
        result.DueDate = DateTime.Parse(Console.ReadLine());
        Console.Write("Enter Complexity: ");
        result.Complexity = Enum.Parse<Complexity>(Console.ReadLine());
        Console.Write("Enter Priority: ");
        result.Priority = Enum.Parse<Priority>(Console.ReadLine());
        Console.Write("Enter Title: ");
        result.Title = Console.ReadLine();
        Console.Write("Enter Description: ");
        result.Description = Console.ReadLine();
        return result;
    }
}
