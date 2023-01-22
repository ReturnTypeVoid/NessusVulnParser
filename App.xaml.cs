using Microsoft.Extensions.DependencyInjection;
using NessusVulnParser.Core;
using NessusVulnParser.MVVM.ViewModels;
using NessusVulnParser.MVVM.Views;
using NessusVulnParser.Services;
using System;
using System.Windows;

namespace NessusVulnParser
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<MainWindow>(provider => new MainWindow
            {
                DataContext = provider.GetRequiredService<MainViewModel>()
            });
            services.AddSingleton<FileLoader>(provider => new FileLoader
            {
                DataContext = provider.GetRequiredService<FileLoaderViewModel>()
            });
            services.AddSingleton<VulnList>(provider => new VulnList
            {
                DataContext = provider.GetRequiredService<VulnListViewModel>()
            });
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<FileLoaderViewModel>();
            services.AddSingleton<VulnListViewModel>();
            services.AddSingleton<INavigationService, NavigationService>();

            services.AddSingleton<Func<Type, ViewModel>>(provider => viewModelType => (ViewModel)provider.GetRequiredService(viewModelType));

            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }
}
