using Domain.Models.Enums;

namespace Domain.Models
{
    public class WorkItem
    {
        public Guid? Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime DueDate { get; set; }
        public Complexity Complexity { get; set; }
        public Priority Priority { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }

        public WorkItem() { }

        public WorkItem(Guid? id, DateTime creationDate, DateTime dueDate, Complexity complexity, Priority priority, string title, string description, bool isCompleted)
        {
            Id = id;
            CreationDate = creationDate;
            DueDate = dueDate;
            Complexity = complexity;
            Priority = priority;
            Title = title;
            Description = description;
            IsCompleted = isCompleted;
        }

        public override string ToString() => $"{Title}: due {DueDate:dd.MM.yyyy}, {$"{Priority}".ToLower()} priority";

        public WorkItem Clone() => new WorkItem(
            Id,
            CreationDate,
            DueDate,
            Complexity,
            Priority,
            Title,
            Description,
            IsCompleted
        );
    }
}
