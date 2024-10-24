using JonDou9000.TaskPlanner.Domain.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace JonDou9000.TaskPlanner.Domain.Logic
{
    public class SimpleTaskPlanner
    {
        private List<WorkItem> tasks = new List<WorkItem>(); // Список задач

        // Метод для додавання нової задачі до списку
        public void AddTask(WorkItem task)
        {
            tasks.Add(task);
        }

        // Метод для збереження задач у файл work-items.json
        public void SaveTasksToFile(string filePath)
        {
            string json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

        // Метод для завантаження задач із файлу
        public void LoadTasksFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                tasks = JsonSerializer.Deserialize<List<WorkItem>>(json);
            }
        }

        // Метод для отримання списку задач
        public List<WorkItem> GetTasks()
        {
            return tasks;
        }
    }
}
