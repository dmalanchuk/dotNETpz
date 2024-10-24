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
        private List<WorkItem> _workItems;

        public FileWorkItemsRepository()
        {
            LoadWorkItemsFromFile();
        }

        private void LoadWorkItemsFromFile()
        {
            if (File.Exists(FileName) && new FileInfo(FileName).Length > 0) // Перевіряємо, чи файл не порожній
            {
                var json = File.ReadAllText(FileName);
                _workItems = JsonConvert.DeserializeObject<List<WorkItem>>(json) ?? new List<WorkItem>();
            }
            else
            {
                _workItems = new List<WorkItem>();
            }
        }

        public Guid Add(WorkItem workItem)
        {
            workItem.Id = Guid.NewGuid();
            _workItems.Add(workItem);
            SaveChanges();
            return workItem.Id;
        }

        public WorkItem Get(Guid id)
        {
            return _workItems.Find(w => w.Id == id);
        }

        public WorkItem[] GetAll()
        {
            return _workItems.ToArray();
        }

        public bool Update(WorkItem workItem)
        {
            var existingItem = Get(workItem.Id);
            if (existingItem != null)
            {
                existingItem.Title = workItem.Title;
                existingItem.Description = workItem.Description;
                existingItem.DueDate = workItem.DueDate;
                SaveChanges();
                return true;
            }
            return false;
        }

        public bool Remove(Guid id)
        {
            var workItem = Get(id);
            if (workItem != null)
            {
                _workItems.Remove(workItem);
                SaveChanges();
                return true;
            }
            return false;
        }

        public void SaveChanges()
        {
            var json = JsonConvert.SerializeObject(_workItems, Formatting.Indented);
            File.WriteAllText(FileName, json);
        }
    }
}
