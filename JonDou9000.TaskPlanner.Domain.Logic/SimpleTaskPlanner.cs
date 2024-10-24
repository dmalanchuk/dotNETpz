using JonDou9000.TaskPlanner.Domain.Models;
using JonDou9000.TaskPlanner.DataAccess.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JonDou9000.TaskPlanner.Domain.Logic
{
    public class SimpleTaskPlanner
    {
        private readonly IWorkItemsRepository _repository;

        public SimpleTaskPlanner(IWorkItemsRepository repository)
        {
            _repository = repository;
        }

        // Метод для побудови плану задач
        public void BuildPlan()
        {
            var tasks = _repository.GetAll()
                .Where(task => !task.IsCompleted) // Ігноруємо виконані задачі
                .ToArray();

            foreach (var task in tasks)
            {
                Console.WriteLine($"Id: {task.Id}, Title: {task.Title}, Description: {task.Description}, Due Date: {task.DueDate}");
            }
        }

        // Метод для додавання нової задачі
        public void AddTask(WorkItem task)
        {
            _repository.Add(task); // Додаємо задачу через репозиторій
        }
    }
}
