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

        // Конструктор, який автоматично генерує унікальний Id
        public WorkItem()
        {
            Id = Guid.NewGuid();
        }

        // Метод для клонування об'єкта
        public WorkItem Clone()
        {
            return new WorkItem
            {
                Id = this.Id, // Копіює існуючий Id
                Title = this.Title,
                Description = this.Description,
                DueDate = this.DueDate
            };
        }
    }
}
