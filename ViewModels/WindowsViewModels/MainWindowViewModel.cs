using SqlDependancyTest.Infrastructure.Strores;
using SqlDependancyTest.Models;
using SqlDependancyTest.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace SqlDependancyTest.ViewModels.WindowsViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly TaskStore _taskStore;
        private readonly EmpStore _empStore;

        private string _title;
        public string Title
        {
            get => _title;
            set => OnPropertyChanged(ref _title, value);
        }

        public IEnumerable<Task> Tasks => _taskStore.Tasks;
        public IEnumerable<Emp> Emps => _empStore.Emps;

        public MainWindowViewModel(TaskStore taskStore, EmpStore empStore)
        {
            _taskStore = taskStore;
            _empStore = empStore;

            Title = "123";
        }
    }
}
