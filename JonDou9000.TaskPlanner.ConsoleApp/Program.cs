using JonDou9000.TaskPlanner.DataAccess;
using JonDou9000.TaskPlanner.DataAccess.Abstractions;
using JonDou9000.TaskPlanner.Domain.Logic;
using JonDou9000.TaskPlanner.Domain.Models;
using System;

namespace JonDou9000.TaskPlanner.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var repository = new FileWorkItemsRepository(); // Ініціалізуємо репозиторій
            SimpleTaskPlanner planner = new SimpleTaskPlanner(repository); // Передаємо репозиторій у конструктор

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
                        planner.BuildPlan(); // Використовуємо метод з SimpleTaskPlanner
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

        static void AddWorkItem(IWorkItemsRepository repository)
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

        static void MarkWorkItemAsCompleted(IWorkItemsRepository repository)
        {
            Console.WriteLine("Введіть ID задачі, яку потрібно позначити як завершену:");
            Guid id = Guid.Parse(Console.ReadLine() ?? string.Empty);
            var workItem = repository.Get(id);

            if (workItem != null)
            {
                workItem.IsCompleted = true; // Позначаємо задачу як виконану
                repository.Update(workItem); // Оновлюємо задачу в репозиторії
                Console.WriteLine($"Задача '{workItem.Title}' позначена як завершена.");
            }
            else
            {
                Console.WriteLine("Задачу не знайдено.");
            }
        }

        static void RemoveWorkItem(IWorkItemsRepository repository)
        {
            Console.WriteLine("Введіть ID задачі, яку потрібно видалити:");
            Guid id = Guid.Parse(Console.ReadLine() ?? string.Empty);

            if (repository.Remove(id))
            {
                Console.WriteLine("Задачу видалено.");
            }
            else
            {
                Console.WriteLine("Задачу не знайдено.");
            }
        }
    }
}
