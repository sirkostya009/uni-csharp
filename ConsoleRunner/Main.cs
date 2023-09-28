using Domain.Logic;
using Domain.Models;
using Domain.Models.Enums;
using DataAccess;

var Repository = new FileWorkItemsRepository();
var Planner = new SimpleTaskPlanner(Repository);

while (true)
{
    var command = ReadString("Enter command: ").ToUpper()[0];

    switch (command)
    {
        case 'A':
            AddWorkItem();
            break;
        case 'B':
            BuildPlan();
            break;
        case 'M':
            MarkCompleted(ReadGuid());
            break;
        case 'R':
            RemoveItem(ReadGuid());
            break;
        case 'Q':
            Environment.Exit(0);
            break;
        default:
            break;
    }
}

void AddWorkItem()
{
    var workItem = WorkItem.Of(
        dueDate:     ReadDate("Enter DueDate: "),
        complexity:  ReadEnum<Complexity>("Enter Complexity: "),
        priority:    ReadEnum<Priority>("Enter Priority: "),
        title:       ReadString("Enter Title: "),
        description: ReadString("Enter Description: ")
    );
    Repository.Add(workItem);
    Repository.SaveChanges();
}

DateTime ReadDate(string message) => DateTime.Parse(ReadString(message));

T ReadEnum<T>(string message) where T : Enum => (T)Enum.Parse(typeof(T), ReadString(message));

Guid ReadGuid() => Guid.Parse(ReadString("Enter guid: "));

string ReadString(string message)
{
    Console.Write(message);
    return Console.ReadLine();
}

void BuildPlan()
{
    foreach (var item in Planner.CreatePlan())
    Console.WriteLine(item);
}

void MarkCompleted(Guid guid)
{
    Repository.Get(guid).IsCompleted = true;
    Repository.SaveChanges();
}

void RemoveItem(Guid guid)
{
    Repository.Remove(Repository.Get(guid));
}
