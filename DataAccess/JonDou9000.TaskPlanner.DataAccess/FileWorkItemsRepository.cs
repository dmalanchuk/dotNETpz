using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
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
            if (File.Exists(FileName))
            {
                var json = File.ReadAllText(FileName);
                _workItems = JsonSerializer.Deserialize<List<WorkItem>>(json) ?? new List<WorkItem>();
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
            var json = JsonSerializer.Serialize(_workItems);
            File.WriteAllText(FileName, json);
        }
    }
}
