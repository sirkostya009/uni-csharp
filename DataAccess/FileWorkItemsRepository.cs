using DataAccess.Abstractions;
using Domain.Models;
using Newtonsoft.Json;

namespace DataAccess;

public class FileWorkItemsRepository : IWorkItemsRepository
{
    private const string dbFilePath = "../../../../work-items.json";
    private Dictionary<Guid?, WorkItem> workItems;

    public FileWorkItemsRepository()
    {
        var directory = Directory.GetCurrentDirectory();

        var json = File.ReadAllText(dbFilePath);

        var items = JsonConvert.DeserializeObject<List<WorkItem>>(json);

        workItems = items.ToDictionary(item => item.Id, item => item);
    }

    public Guid Add(WorkItem item)
    {
        var guid = Guid.NewGuid();
        var copy = item.Clone();
        copy.Id = guid;
        workItems[guid] = copy;
        return guid;
    }

    public WorkItem Get(Guid guid) => workItems[guid];

    public WorkItem[] GetAll() => workItems.Values.ToArray();

    public bool Remove(WorkItem item) => workItems.Remove(item.Id);

    public bool Update(WorkItem item)
    {
        if (item.Id.HasValue)
        {
            workItems[item.Id] = item;
            return true;
        }
        else
        {
            Add(item);
            return false;
        }
    }

    public void SaveChanges()
    {
        var json = JsonConvert.SerializeObject(workItems.Values.ToArray(), Formatting.Indented);

        File.WriteAllText(dbFilePath, json);
    }
}
