﻿using Domain.Models.Enums;

namespace Domain.Models
{
    public class WorkItem
    {
        public DateTime CreationDate { get; set; }
        public DateTime DueDate { get; set; }
        public Complexity Complexity { get; set; }
        public Priority Priority { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }

        public override string ToString()
        {
            return $"{Title}: due {DueDate:dd.MM.yyyy}, {$"{Priority}".ToLower()} priority";
        }
    }
}