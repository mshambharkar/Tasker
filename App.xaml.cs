using Hardcodet.Wpf.TaskbarNotification;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using Tasker.Common;
using Tasker.Services;
using Tasker.ViewModel;

namespace Tasker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();
           
        }

       

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped(typeof(MainViewModel));
            services.AddScoped(typeof(NewTaskViewModel));
            services.AddScoped(typeof(ProgressTasksViewModel));
            services.AddScoped(typeof(DoneTaskViewModel));
            services.AddSingleton<ITaskDbService>(new TaskDbService());
            services.AddSingleton<EventAgrregator>(new EventAgrregator());
            services.AddSingleton<MainWindow>();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }
    }
}
