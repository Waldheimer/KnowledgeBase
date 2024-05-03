using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KnowledgeBase.WPF.Manager;
using KnowledgeBase.WPF.Services;
using FontAwesome.WPF;
using System.Windows;

namespace KnowledgeBase.WPF.ViewModels
{
    public partial class MainViewModel : ObservableRecipient
    {
        #region Fields & Properties

        private readonly NavigationManager _navigationManager;
        private readonly NavigationService<DashboardViewModel> _dashboardNavi;
        private readonly NavigationService<CLIViewModel> _cliNavi;
        private readonly NavigationService<CodeViewModel> _codeNavi;
        private readonly NavigationService<DocsViewModel> _docsNavi;

        public ObservableRecipient CurrentViewModel => _navigationManager.CurrentViewModel;

        [ObservableProperty]
        private FontAwesomeIcon settingsIcon;

        #endregion

        #region Constructor & Base Functions

        public MainViewModel(NavigationManager navigationManager,
                                NavigationService<DashboardViewModel> dashboardNavigation,
                                NavigationService<CLIViewModel> cliNavigation, 
                                NavigationService<CodeViewModel> codeNavigation, 
                                NavigationService<DocsViewModel> docsNavigation)
        {
            _navigationManager = navigationManager;

            _navigationManager.CurrentViewModelChanged += OnCurrentViewModelChanged;


            _dashboardNavi = dashboardNavigation;
            _cliNavi = cliNavigation;
            _docsNavi = docsNavigation;
            _codeNavi = codeNavigation;

            settingsIcon = FontAwesomeIcon.Gears;
            LoadDashboard();
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        #endregion

        #region Commands
        [RelayCommand]
        public void LoadDashboard()
        {
            _dashboardNavi.Navigate();
        }
        [RelayCommand]
        public void LoadCLI()
        {
            _cliNavi.Navigate();
        }
        [RelayCommand]  
        public void LoadCode() 
        {
            _codeNavi.Navigate();
        }
        [RelayCommand]
        public void LoadDocs()
        {
            _docsNavi.Navigate();
        }


        [RelayCommand]
        public void OpenSettings()
        {
            MessageBox.Show("Settings Window to Open here !!!");
        }

        #endregion
    }
}
