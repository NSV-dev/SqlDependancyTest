using Microsoft.EntityFrameworkCore;
using SqlDependancyTest.Models;
using SqlDependancyTest.Models.Contexts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.Enums;
using TableDependency.SqlClient.Base.EventArgs;

namespace SqlDependancyTest.Infrastructure.Strores
{
    public class TaskStore : IDisposable
    {        
        private SqlTableDependency<Task> _depependency;

        private ObservableCollection<Task> _tasks;
        public IEnumerable<Task> Tasks => _tasks;

        public TaskStore()
        {
            _tasks = new();

            _depependency = new("Server=DESKTOP-O05LV9C\\SQLEXPRESS;Database=Курсовая;Trusted_Connection=True;", "tasks");
            _depependency.OnChanged += OnChanged;
            _depependency.Start();
        }

        private void OnChanged(object sender, RecordChangedEventArgs<Task> e)
        {
            if (Tasks != null)
                if (e.ChangeType != ChangeType.None)
                    switch (e.ChangeType)
                    {
                        case ChangeType.Delete:
                            OnDeleted(e.Entity);
                            break;
                        case ChangeType.Insert:
                            OnAdded(e.Entity);
                            break;
                        case ChangeType.Update:
                            OnChanged(e.Entity);
                            break;
                    }
        }

        private void OnAdded(Task entity) => App.Current.Dispatcher.Invoke(() => _tasks.Add(entity));

        private void OnDeleted(Task entity) => App.Current.Dispatcher.Invoke(() => 
        {
            int index = new List<Task>(_tasks).FindIndex(t => t.Id == entity.Id);
            _tasks.RemoveAt(index);
        });

        private void OnChanged(Task entity) => App.Current.Dispatcher.Invoke(() =>
        {
            int index = new List<Task>(_tasks).FindIndex(t => t.Id == entity.Id);
            _tasks[index] = entity;
        });

        public void Load()
        {
            _tasks.Clear();
            using (dbContext db = new())
            {
                foreach (Task task in db.Tasks.Include(t => t.Emp).ThenInclude(e=>e.Personality))
                    _tasks.Add(task);
            }
        }

        public void Dispose() => _depependency.Stop();
    }
}
