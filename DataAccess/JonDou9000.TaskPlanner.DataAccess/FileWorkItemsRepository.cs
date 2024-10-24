using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json; // Додай цей using
using JonDou9000.TaskPlanner.DataAccess.Abstractions;
using JonDou9000.TaskPlanner.Domain.Models;

namespace JonDou9000.TaskPlanner.DataAccess
{
    public class FileWorkItemsRepository : IWorkItemsRepository
    {
        private const string FileName = "work-items.json";
        private readonly Dictionary<Guid, WorkItem> _workItems; // Змінено на словник

        public FileWorkItemsRepository()
        {
            _workItems = new Dictionary<Guid, WorkItem>(); // Ініціалізація словника
            LoadWorkItemsFromFile();
        }

        private void LoadWorkItemsFromFile()
        {
            if (File.Exists(FileName) && new FileInfo(FileName).Length > 0) // Перевіряємо, чи файл не порожній
            {
                var json = File.ReadAllText(FileName);
                var workItemsArray = JsonConvert.DeserializeObject<List<WorkItem>>(json) ?? new List<WorkItem>();

                // Конвертуємо масив в словник
                foreach (var item in workItemsArray)
                {
                    _workItems[item.Id] = item; // Додаємо в словник
                }
            }
        }

        public Guid Add(WorkItem workItem)
        {
            var newItem = workItem.Clone(); // Створюємо копію об'єкта
            newItem.Id = Guid.NewGuid(); // Генеруємо новий Guid
            _workItems[newItem.Id] = newItem; // Додаємо копію в словник
            SaveChanges(); // Зберігаємо зміни
            return newItem.Id; // Повертаємо новий ID
        }

        public WorkItem Get(Guid id)
        {
            _workItems.TryGetValue(id, out var workItem); // Отримуємо WorkItem за ID
            return workItem;
        }

        public WorkItem[] GetAll()
        {
            return new List<WorkItem>(_workItems.Values).ToArray(); // Повертаємо масив значень словника
        }

        public bool Update(WorkItem workItem)
        {
            if (_workItems.ContainsKey(workItem.Id))
            {
                _workItems[workItem.Id] = workItem; // Оновлюємо запис в словнику
                SaveChanges(); // Зберігаємо зміни
                return true;
            }
            return false;
        }

        public bool Remove(Guid id)
        {
            var removed = _workItems.Remove(id); // Видаляємо запис зі словника
            if (removed)
            {
                SaveChanges(); // Зберігаємо зміни
            }
            return removed;
        }

        public void SaveChanges()
        {
            var json = JsonConvert.SerializeObject(new List<WorkItem>(_workItems.Values), Formatting.Indented); // Конвертуємо словник в масив
            File.WriteAllText(FileName, json); // Записуємо в файл
        }
    }
}
