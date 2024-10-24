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
            var repository = new FileWorkItemsRepository();
            string command;

            do
            {
                Console.WriteLine("\nОберіть команду:");
                Console.WriteLine("[A] - Додати задачу");
                Console.WriteLine("[B] - Побудувати план");
                Console.WriteLine("[M] - Позначити задачу як завершену");
                Console.WriteLine("[R] - Видалити задачу");
                Console.WriteLine("[Q] - Вийти з програми");

                command = Console.ReadLine()?.ToUpper();

                switch (command)
                {
                    case "A":
                        AddWorkItem(repository);
                        break;
                    case "B":
                        BuildPlan(repository);
                        break;
                    case "M":
                        MarkWorkItemAsCompleted(repository);
                        break;
                    case "R":
                        RemoveWorkItem(repository);
                        break;
                    case "Q":
                        Console.WriteLine("Вихід з програми...");
                        break;
                    default:
                        Console.WriteLine("Невідома команда. Спробуйте ще раз.");
                        break;
                }
            } while (command != "Q");
        }

        static void AddWorkItem(FileWorkItemsRepository repository)
        {
            Console.WriteLine("Введіть заголовок задачі:");
            string title = Console.ReadLine();
            Console.WriteLine("Введіть опис задачі:");
            string description = Console.ReadLine();
            Console.WriteLine("Введіть дату завершення (yyyy-MM-dd):");
            DateTime dueDate = DateTime.Parse(Console.ReadLine() ?? DateTime.Now.ToString());

            var newWorkItem = new WorkItem
            {
                Title = title,
                Description = description,
                DueDate = dueDate
            };
            Guid newId = repository.Add(newWorkItem);
            Console.WriteLine($"Задача додана з ID: {newId}");
        }

        static void BuildPlan(FileWorkItemsRepository repository)
        {
            var allItems = repository.GetAll();
            Console.WriteLine("Ваші задачі:");
            foreach (var item in allItems)
            {
                Console.WriteLine($"Id: {item.Id}, Title: {item.Title}, Description: {item.Description}, Due Date: {item.DueDate}");
            }
        }

        static void MarkWorkItemAsCompleted(FileWorkItemsRepository repository)
        {
            Console.WriteLine("Введіть ID задачі, яку потрібно позначити як завершену:");
            Guid id = Guid.Parse(Console.ReadLine() ?? string.Empty);
            var workItem = repository.Get(id);

            if (workItem != null)
            {
                // Тут можна додати логіку для позначення задачі як завершеної
                Console.WriteLine($"Задача '{workItem.Title}' позначена як завершена.");
            }
            else
            {
                Console.WriteLine("Задачу не знайдено.");
            }
        }

        static void RemoveWorkItem(FileWorkItemsRepository repository)
        {
            Console.WriteLine("Введіть ID задачі, яку потрібно видалити:");
            Guid id = Guid.Parse(Console.ReadLine() ?? string.Empty);
            bool removed = repository.Remove(id);

            if (removed)
            {
                Console.WriteLine("Задачу успішно видалено.");
            }
            else
            {
                Console.WriteLine("Задачу не знайдено.");
            }
        }
    }
}
