using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.Http;
using System.Windows;
using KnowledgeBase.WPF.Manager;
using KnowledgeBase.WPF.Query;
using KnowledgeBase.WPF.Services;
using KnowledgeBase.WPF.ViewModels;

namespace KnowledgeBase.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        IHost _host;

        public App()
        {
            ///<summary>
            /// Creating the HostBuilder for Service-Registration
            /// </summary>

            _host = Host.CreateDefaultBuilder().ConfigureServices(services =>
            {
                //  Adding ViewModels to the Host
                services.AddSingleton<MainViewModel>();
                services.AddSingleton<DashboardViewModel>();
                services.AddSingleton<CLIViewModel>();
                services.AddSingleton<CodeViewModel>();
                services.AddSingleton<DocsViewModel>();

                //  Adding Views to the Host
                services.AddSingleton<MainWindow>(s => new MainWindow()
                {
                    DataContext = s.GetRequiredService<MainViewModel>()
                });
                //services.AddSingleton<MainWindow>();

                //  Adding ViewModelFactory Funcs to the Host
                services.AddSingleton<Func<DashboardViewModel>>((s) => () => s.GetRequiredService<DashboardViewModel>());
                services.AddSingleton<Func<CLIViewModel>>(s => () => s.GetRequiredService<CLIViewModel>());
                services.AddSingleton<Func<CodeViewModel>>(s => () => s.GetRequiredService<CodeViewModel>());
                services.AddSingleton<Func<DocsViewModel>>(s => () => s.GetRequiredService<DocsViewModel>());

                //  Adding Navigation Services to the Host
                services.AddSingleton<NavigationService<DashboardViewModel>>();
                services.AddSingleton<NavigationService<CLIViewModel>>();
                services.AddSingleton<NavigationService<CodeViewModel>>();
                services.AddSingleton<NavigationService<DocsViewModel>>();

                services.AddSingleton<ApiDataService>();

                //  Adding Manager to the Host
                services.AddSingleton<NavigationManager>();
                services.AddSingleton<ApiDataManager>();

                services.AddSingleton<HttpClient>();
                services.AddSingleton<ApiQuery>();

                
            }).Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            /// <summary>
            /// Starting the Host, Initializing and Showing the MainWindow
            /// </summary>
            _host.Start();

            MainWindow = _host.Services.GetRequiredService<MainWindow>();

            MainWindow.Show();
            
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            /// Disposing the Host on Exit
            _host.Dispose();

            base.OnExit(e);
        }
    }

}
