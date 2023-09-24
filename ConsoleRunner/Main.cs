using Domain.Logic;
using Domain.Models;
using Domain.Models.Enums;
using DataAccess.Abstractions;
using DataAccess;

static class Program
{
    private static IWorkItemsRepository Repository = new FileWorkItemsRepository();
    private static SimpleTaskPlanner Planner = new SimpleTaskPlanner(Repository);

    public static void Main(string[] args)
    {
        Console.WriteLine(Environment.CurrentDirectory);
        Console.WriteLine(new WorkItem().Id);
        do
        {
            var command = Console.ReadLine().ToUpper()[0];

            switch (command)
            {
                case 'A':
                    AddWorkItem();
                    break;
                case 'B':
                    BuildPlan();
                    break;
                case 'M':
                    MarkCompleted(Guid.Parse(Console.ReadLine()));
                    break;
                case 'R':
                    RemoveItem(Guid.Parse(Console.ReadLine()));
                    break;
                case 'Q':
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }
        }
        while (true);
    }

    private static void AddWorkItem()
    {
        var workItem = new WorkItem(
            id: Guid.Empty,
            creationDate: DateTime.Now,
            dueDate: ReadDate("Enter DueDate: "),
            complexity: ReadEnum<Complexity>("Enter Complexity: "),
            priority: ReadEnum<Priority>("Enter Priority: "),
            title: ReadString("Enter Title: "),
            description: ReadString("Enter Description: "),
            isCompleted: false
        );
        Repository.Add(workItem);
        Repository.SaveChanges();
    }

    private static DateTime ReadDate(string message) {
        Console.Write(message);
        return DateTime.Parse(Console.ReadLine());
    }

    private static T ReadEnum<T>(string message) where T : Enum
    {
        Console.Write(message);
        return (T)Enum.Parse(typeof(T), Console.ReadLine());
    }

    private static string ReadString(string message) { 
        Console.Write(message);
        return Console.ReadLine();
    }

    private static void BuildPlan()
    {
        var plan = Planner.CreatePlan();
        foreach (var item in plan)
        Console.WriteLine(item);
    }

    private static void MarkCompleted(Guid guid)
    {
        Repository.Get(guid).IsCompleted = true;
        Repository.SaveChanges();
    }

    private static void RemoveItem(Guid guid)
    {
        Repository.Remove(Repository.Get(guid));
    }
}
