using Microsoft.EntityFrameworkCore;
using SqlDependancyTest.Models;
using SqlDependancyTest.Models.Contexts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.Enums;
using TableDependency.SqlClient.Base.EventArgs;

namespace SqlDependancyTest.Infrastructure.Strores
{
    public class EmpStore : IDisposable
    {
        private ObservableCollection<Emp> _emps;
        private readonly SqlTableDependency<Emp> _dependency;
        private readonly SqlTableDependency<Personality> _persdependency;

        public IEnumerable<Emp> Emps => _emps;

        public EmpStore()
        {
            _emps = new();

            _dependency = new("Server=DESKTOP-O05LV9C\\SQLEXPRESS;Database=Курсовая;Trusted_Connection=True;", "emp");
            _dependency.OnChanged += OnChanged;
            _dependency.Start();

            _persdependency = new("Server=DESKTOP-O05LV9C\\SQLEXPRESS;Database=Курсовая;Trusted_Connection=True;", "personality");
            _persdependency.OnChanged += OnPersChanged;
            _persdependency.Start();
        }

        private void OnPersChanged(object sender, RecordChangedEventArgs<Personality> e)
        {
            if (Emps != null) 
                if (e.ChangeType == ChangeType.Update)
                {
                    Emp emp = _emps[new List<Emp>(_emps).FindIndex(emp => emp.PersonalityId == e.Entity.Id)];
                        App.Current.Dispatcher.Invoke (() =>
                        _emps[new List<Emp>(_emps).FindIndex(e => e.Id == emp.Id)] = new Emp()
                        {
                            Id = emp.Id,
                            Login = emp.Login,
                            Password = emp.Password,
                            Personality = e.Entity,
                            PersonalityId = e.Entity.Id,
                            Company = emp.Company,
                            CompanyId = emp.CompanyId,
                            Role = emp.Role,
                            RoleId = emp.RoleId,
                            Tasks = emp.Tasks
                        });
                }
        }

        private void OnChanged(object sender, RecordChangedEventArgs<Emp> e)
        {
            if (Emps != null)
            {
                using (dbContext db = new())
                {
                    e.Entity.Personality = db.Personalities.First(p => p.Id == e.Entity.PersonalityId);
                }
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
        }

        private void OnAdded(Emp entity) => App.Current.Dispatcher.Invoke(() => _emps.Add(entity));

        private void OnDeleted(Emp entity) => App.Current.Dispatcher.Invoke(() =>
        {
            int index = new List<Emp>(_emps).FindIndex(e => e.Id == entity.Id);
            _emps.RemoveAt(index);
        });

        private void OnChanged(Emp entity) => App.Current.Dispatcher.Invoke(() =>
        {
            int index = new List<Emp>(_emps).FindIndex(e => e.Id == entity.Id);
            _emps[index] = entity;
        });

        public void Load()
        {
            _emps.Clear();
            using (dbContext db = new())
            {
                foreach (Emp emp in db.Emps
                    .Include(e => e.Personality).ThenInclude(p => p.Gender)
                    .Include(e => e.Tasks)
                    .Include(e => e.Role)
                    .Include(e => e.Company).ThenInclude(c => c.Admin).ThenInclude(a => a.Personality).ThenInclude(p => p.Gender)
                    .Include(e => e.Company).ThenInclude(c => c.Admin).ThenInclude(a => a.Role))
                    _emps.Add(emp);
            }
        }

        public void Dispose()
        {
            _dependency.Stop();
            _persdependency.Stop();
        }
    }
}
