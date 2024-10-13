using System;
using JonDou9000.TaskPlanner.Domain.Models.Enums;

namespace JonDou9000.TaskPlanner.Domain.Models
{
    public class WorkItem
    {
        public DateTime CreationDate { get; set; }
        public DateTime DueDate { get; set; }
        public Priority Priority { get; set; }
        public Complexity Complexity { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }

        public WorkItem(DateTime creationDate, DateTime dueDate, Priority priority, Complexity complexity, string title, string description, bool isCompleted)
        {
            CreationDate = creationDate;
            DueDate = dueDate;
            Priority = priority;
            Complexity = complexity;
            Title = title;
            Description = description;
            IsCompleted = isCompleted;
        }

        public override string ToString()
        {
            return $"{Title}: due {DueDate:dd.MM.yyyy}, {Priority.ToString().ToLower()} priority";
        }
    }
}
