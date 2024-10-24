using JonDou9000.TaskPlanner.Domain.Models.Enums;
using System;

namespace JonDou9000.TaskPlanner.Domain.Models
{
    public class WorkItem
    {
        public Guid Id { get; set; } // Властивість Id для кожної задачі
        public string Title { get; set; } // Назва задачі
        public string Description { get; set; } // Опис задачі
        public DateTime DueDate { get; set; } // Крайній термін
        public bool IsCompleted { get; set; } // Властивість для позначення статусу виконання

        // Конструктор, який автоматично генерує унікальний Id
        public WorkItem()
        {
            Id = Guid.NewGuid();
            IsCompleted = false; // За замовчуванням задачі не виконані
        }

        // Метод для клонування об'єкта
        public WorkItem Clone()
        {
            return new WorkItem
            {
                Id = this.Id, // Копіює існуючий Id
                Title = this.Title,
                Description = this.Description,
                DueDate = this.DueDate,
                IsCompleted = this.IsCompleted // Копіює статус виконання
            };
        }
    }
}
