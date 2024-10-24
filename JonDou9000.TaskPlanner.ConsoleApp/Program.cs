using JonDou9000.TaskPlanner.DataAccess;
using JonDou9000.TaskPlanner.Domain.Logic;
using JonDou9000.TaskPlanner.Domain.Models;
using System;

namespace JonDou9000.TaskPlanner.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            SimpleTaskPlanner planner = new SimpleTaskPlanner();

            // Створення нової задачі
            WorkItem task = new WorkItem
            {
                Title = "Написати звіт",
                Description = "Завершити звіт до п'ятниці",
                DueDate = DateTime.Now.AddDays(3)
            };

            planner.AddTask(task);

            // Збереження задачі у файл
            string filePath = "work-items.json";
            planner.SaveTasksToFile(filePath);
            Console.WriteLine("Задачі збережено у файл.");

            // Завантаження задач з файлу
            planner.LoadTasksFromFile(filePath);
            Console.WriteLine("Задачі завантажено з файлу:");

            // Виведення всіх задач на екран
            foreach (var item in planner.GetTasks())
            {
                Console.WriteLine($"Id: {item.Id}, Title: {item.Title}, Description: {item.Description}, Due Date: {item.DueDate}");
            }

            // Тестування FileWorkItemsRepository
            Console.WriteLine("\n--- Тестування FileWorkItemsRepository ---");

            var repository = new FileWorkItemsRepository();

            // Додати нову задачу через репозиторій
            var newWorkItem = new WorkItem
            {
                Title = "Тестова задача",
                Description = "Це тестова задача.",
                DueDate = DateTime.Now.AddDays(5)
            };
            Guid newId = repository.Add(newWorkItem);
            Console.WriteLine($"Додано новий WorkItem з Id: {newId}");

            // Отримання WorkItem
            var retrievedItem = repository.Get(newId);
            Console.WriteLine($"Отриманий WorkItem з Id {retrievedItem?.Id}");

            // Отримання всіх WorkItems
            var allItems = repository.GetAll();
            Console.WriteLine($"Кількість WorkItems: {allItems.Length}");

            // Оновлення WorkItem
            newWorkItem.Id = newId; // Встановлення Id для оновлення
            newWorkItem.Description = "Оновлений опис";
            bool updated = repository.Update(newWorkItem);
            Console.WriteLine($"Оновлено WorkItem: {updated}");

            // Видалення WorkItem
            bool removed = repository.Remove(newId);
            Console.WriteLine($"Видалено WorkItem: {removed}");

            // Кількість WorkItems після видалення
            allItems = repository.GetAll();
            Console.WriteLine($"Кількість WorkItems після видалення: {allItems.Length}");
        }
    }
}
