using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using KnowledgeBase.WPF.Manager;
using KnowledgeBase.WPF.Models;
using KnowledgeBase.WPF.Services;

namespace KnowledgeBase.WPF.ViewModels
{
    public partial class DashboardViewModel : ObservableRecipient
    {

        [ObservableProperty]
        private int commandCount = 0;
        [ObservableProperty]
        private int snippetCount = 0;
        [ObservableProperty]
        private int documentationCount = 0;
        public ObservableCollection<string> Systems => _dataManager.Systems;
        public ObservableCollection<string> Technologies => _dataManager.Techs;
        public ObservableCollection<string> Languages => _dataManager.Langs;

        [RelayCommand]
        public async Task TestNew()
        {
            Command c = new Command() { CommandText = "Test ob alles laaft" };
            Descriptor d = new Descriptor() { System = "Windoof", Tech = "API", Lang = "C#" };
            Description desc = new Description() { DescriptionText = "Gugge obs funzt", Version = "1.0.0" };
            string res = await _dataService.CreateCommand(c,d,desc);
            MessageBox.Show(res);
        }

        private readonly ApiDataService _dataService;
        private readonly ApiDataManager _dataManager;

        public DashboardViewModel(ApiDataService dataService, ApiDataManager dataManager)
        {
            _dataService = dataService;
            _dataManager = dataManager;
            _dataManager.SystemListChanged += OnSystemListChanged;
            _dataManager.TechListChanged += OnTechListChanged;
            _dataManager.LangListChanged += OnLangListChanged;
        }

        private void OnSystemListChanged()
        {
            OnPropertyChanged(nameof(Systems));
        }
        private void OnTechListChanged()
        {
            OnPropertyChanged(nameof(Technologies));
        }
        private void OnLangListChanged()
        {
            OnPropertyChanged(nameof(Languages));
        }

        protected override async void OnActivated()
        {
            var commands = await _dataService.GetAllCommands();
            CommandCount = commands.Count();
            var snippets = await _dataService.GetAllSnippets();
            SnippetCount = snippets.Count();
            var docs = await _dataService.GetAllDocumentations();
            DocumentationCount = docs.Count();
            await _dataService.GetAllSystems();
            await _dataService.GetAllTechs();
            await _dataService.GetAllLangs();
            base.OnActivated();
        }
    }
}
