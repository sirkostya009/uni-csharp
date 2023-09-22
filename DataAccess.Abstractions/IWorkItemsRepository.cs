using Domain.Models;

namespace DataAccess.Abstractions;

public interface IWorkItemsRepository
{
    Guid Add(WorkItem item);

    WorkItem Get(Guid guid);

    WorkItem[] GetAll();

    bool Update(WorkItem item);

    bool Remove(WorkItem item);

    void SaveChanges();
}
