using Domain.Models;
using DataAccess.Abstractions;

namespace Domain.Logic
{
    public class SimpleTaskPlanner
    {
        private IWorkItemsRepository Repository;

        public SimpleTaskPlanner(IWorkItemsRepository repository)
        {
            Repository = repository;
        }

        public WorkItem[] CreatePlan() => (
            from item in Repository.GetAll()
            where !item.IsCompleted
            orderby item.Priority descending, item.DueDate ascending, item.Title
            select item
        ).ToArray();
    }
}
