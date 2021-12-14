using Microsoft.Extensions.DependencyInjection;
using SqlDependancyTest.Infrastructure.Strores;
using SqlDependancyTest.ViewModels.WindowsViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SqlDependancyTest
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<TaskStore>();
            services.AddSingleton<EmpStore>();

            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<MainWindow>(s => new MainWindow() { DataContext = s.GetRequiredService<MainWindowViewModel>() });

            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _serviceProvider.GetRequiredService<TaskStore>().Load();
            _serviceProvider.GetRequiredService<EmpStore>().Load();

            MainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _serviceProvider.Dispose();

            base.OnExit(e);
        }
    }
}
