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
        }
    }
}
