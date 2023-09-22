using Domain.Logic;
using Domain.Models;
using Domain.Models.Enums;
using DataAccess.Abstractions;
using DataAccess;

static class Program
{
    private static SimpleTaskPlanner Planner = new SimpleTaskPlanner();
    private static IWorkItemsRepository Repository = new FileWorkItemsRepository();

    public static void Main(string[] args)
    {
        do
        {
            var command = Console.ReadLine()[0];

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
            dueDate: DateTime.Parse(Console.ReadLine()),
            complexity: Enum.Parse<Complexity>(Console.ReadLine()),
            priority: Enum.Parse<Priority>(Console.ReadLine()),
            title: Console.ReadLine(),
            description: Console.ReadLine(),
            isCompleted: false
        );
        Repository.Add(workItem);
        Repository.SaveChanges();
    }

    private static void BuildPlan()
    {
        foreach (var item in Planner.CreatePlan(Repository.GetAll().ToArray()))
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
