using System;
using System.Collections.Generic;
using System.Linq;
using JonDou9000.TaskPlanner.Domain.Models;

namespace JonDou9000.TaskPlanner.Domain.Logic
{
    public class SimpleTaskPlanner
    {
        public WorkItem[] CreatePlan(WorkItem[] items)
        {
            var itemsAsList = items.ToList();
            itemsAsList.Sort(CompareWorkItems);
            return itemsAsList.ToArray();
        }

        private static int CompareWorkItems(WorkItem firstItem, WorkItem secondItem)
        {
            // Порівнюємо за пріоритетом (спаданням)
            if (firstItem.Priority != secondItem.Priority)
                return secondItem.Priority.CompareTo(firstItem.Priority);

            // Потім за терміном виконання (зростанням)
            if (firstItem.DueDate != secondItem.DueDate)
                return firstItem.DueDate.CompareTo(secondItem.DueDate);

            // Нарешті, за назвою (алфавітний порядок)
            return string.Compare(firstItem.Title, secondItem.Title, StringComparison.Ordinal);
        }
    }
}
