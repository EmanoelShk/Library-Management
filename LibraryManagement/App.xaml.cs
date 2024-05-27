using LibraryManagement.Data;
using LibraryManagement.Services;
using LibraryManagement.ViewModels;
using LibraryManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using LibraryManagement.Views;

namespace LibraryManagement
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider _serviceProvider;

        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddDbContext<LibraryContext>(options =>
                options.UseSqlite("Data Source=library.db"));

            services.AddScoped<LoanService>();
            services.AddSingleton<LibraryViewModel>();
            services.AddSingleton<MainWindow>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var mainWindow = _serviceProvider.GetService<MainWindow>();
            if (mainWindow == null)
            {
                throw new Exception("MainWindow could not be resolved from the service provider.");
            }
            mainWindow.Show();
        }
    }
}
