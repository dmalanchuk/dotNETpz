using System;
using System.Collections.Generic;
using JonDou9000.TaskPlanner.Domain.Models;
using JonDou9000.TaskPlanner.Domain.Logic;
using JonDou9000.TaskPlanner.Domain.Models.Enums;

internal static class Program
{
    public static void Main(string[] args)
    {
        List<WorkItem> workItems = new List<WorkItem>();
        string input;

        Console.WriteLine("Введіть кількість WorkItem, які потрібно створити:");
        int count = int.Parse(Console.ReadLine());

        for (int i = 0; i < count; i++)
        {
            Console.WriteLine($"Введіть назву для WorkItem {i + 1}:");
            string title = Console.ReadLine();

            Console.WriteLine("Введіть термін виконання (yyyy-MM-dd):");
            DateTime dueDate = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Введіть пріоритет (None, Low, Medium, High, Urgent):");
            Priority priority = (Priority)Enum.Parse(typeof(Priority), Console.ReadLine(), true);

            Console.WriteLine("Введіть складність (None, Minutes, Hours, Days, Weeks):");
            Complexity complexity = (Complexity)Enum.Parse(typeof(Complexity), Console.ReadLine(), true);

            Console.WriteLine("Введіть опис (необов'язково):");
            string description = Console.ReadLine();

            var workItem = new WorkItem(DateTime.Now, dueDate, priority, complexity, title, description, false);
            workItems.Add(workItem);
        }

        SimpleTaskPlanner planner = new SimpleTaskPlanner();
        var sortedWorkItems = planner.CreatePlan(workItems.ToArray());

        Console.WriteLine("\nВпорядковані WorkItems:");
        foreach (var item in sortedWorkItems)
        {
            Console.WriteLine(item.ToString());
        }
    }
}