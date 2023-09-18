using Domain.Models;

namespace Domain.Logic
{
    public class SimpleTaskPlanner
    {
        public WorkItem[] CreatePlan(WorkItem[] workItems)
        {
            return (
                from item in workItems
                orderby item.Priority descending, item.DueDate ascending, item.Title
                select item
            ).ToArray();
        }
    }
}
