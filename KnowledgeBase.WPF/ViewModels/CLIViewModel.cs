using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.DirectoryServices.ActiveDirectory;
using System.Windows;
using KnowledgeBase.WPF.Manager;
using KnowledgeBase.WPF.Models;
using KnowledgeBase.WPF.Services;

namespace KnowledgeBase.WPF.ViewModels
{
    public partial class CLIViewModel : ObservableRecipient
    {
        private readonly ApiDataManager _dataManager;
        private readonly ApiDataService _dataService;
        public ObservableCollection<ReadCSD_DTO> Commands => _dataManager.Commands;

        #region ObservableProperties

        //  All Command related Properties
        //  - filteredCommands  : the subset of Command that is actually Shown based on Search criteria
        //  - actualIndex       : Index of the currently selected Command in the List
        //  - selectedCommand   : The ReadCSD_DTO representation of the actual selected Command
        [ObservableProperty]
        private ObservableCollection<ReadCSD_DTO> filteredCommands;
        partial void OnFilteredCommandsChanged(ObservableCollection<ReadCSD_DTO> value)
        {
            if(value == null || value.Count == 0)
            {
                SelectedCommand = null;
            }
        }

        [ObservableProperty]
        private int actualIndex; 
        partial void OnActualIndexChanged(int value)
        {
            if (FilteredCommands == null) { SelectedCommand = Commands[0]; return; }
            SelectedCommand = FilteredCommands[value];
        }

        [ObservableProperty]
        private ReadCSD_DTO selectedCommand;


        //  All Properties related to the CommandSearch-Functionality
        //  - searchItems : List of Properties to Search in
        //  - searchIndex : Selected Index in the List of Search Properties
        //  - searchField : Contains the string-Representation of the Search-Property
        //  - serachText  : The actual string that contains the Search-Text
        [ObservableProperty]
        public List<string> searchItems = new List<string>() { "Command", "System", "Technology", "Language", "Description", "Version"};
        [ObservableProperty]
        private int searchIndex;
        partial void OnSearchIndexChanged(int oldValue, int newValue)
        {
            SearchField = SearchItems[SearchIndex];
        }
        public string SearchField = string.Empty;

        [ObservableProperty]
        private string searchText = string.Empty;

        //  Enables Visibility of the NewCommand/EditCommand Panels
        [ObservableProperty]
        private bool isEditMode = false;
        [ObservableProperty]
        private bool isNewCommandMode = false;

        //  All Data for Creating a new CommandEntry
        [ObservableProperty] private string newCommandText = string.Empty;
        [ObservableProperty] private string newCommandSystem = string.Empty;
        [ObservableProperty] private string newCommandTech = string.Empty;
        [ObservableProperty] private string newCommandLang = string.Empty;
        [ObservableProperty] private string newCommandVersion = "1.0.0";
        [ObservableProperty] private string newCommandDescription = string.Empty;

        #endregion

        public CLIViewModel(ApiDataManager dataManager, ApiDataService dataService)
        {
            _dataService = dataService;
            _dataManager = dataManager;
            _dataManager.CommandsListChanged += OnCommandsListChanged;
            ActualIndex = 0;
            SearchIndex = 1;
            FilteredCommands = Commands;
            SelectedCommand = FilteredCommands[ActualIndex];
        }

        private void OnCommandsListChanged()
        {
            OnPropertyChanged(nameof(Commands));
        }

        private void ClearNewEntryData()
        {
            NewCommandText = string.Empty;
            NewCommandSystem = string.Empty;
            NewCommandTech = string.Empty;
            NewCommandLang = string.Empty;
            NewCommandVersion = "1.0.0";
            NewCommandDescription = string.Empty;
        }

        [RelayCommand]
        public void EditMode()
        {
            IsEditMode = !IsEditMode;
        }
        [RelayCommand] 
        public async Task SaveEditChanges()
        {
            string isUpdated = "false";
            UpdateCSD_DTO dto = UpdateCSD_DTO.FromReadCSD_DTO(SelectedCommand);
            var res = MessageBox.Show("Are you sure ?", "Update selected Command", MessageBoxButton.YesNo);
            if(res == MessageBoxResult.Yes)
            {
                isUpdated = await _dataService.UpdateCommandAsync(dto);
                ReadCSD_DTO update = _dataManager.Commands.First(c => c.ID == dto.ID);
                update = dto;
                ActualIndex = 0;
            }
            MessageBox.Show(isUpdated.Equals("true") ? "Command successfully updated." : "Command could not be updated.");
        }
        [RelayCommand]
        public void NewEntryMode()
        {
            IsNewCommandMode = !IsNewCommandMode;
            ClearNewEntryData();
        }
        [RelayCommand] 
        public async Task SaveNewEntry()
        {
            Command c = new Command { CommandText = NewCommandText };
            Descriptor d = new Descriptor { System = NewCommandSystem, Tech = NewCommandTech, Lang = NewCommandLang };
            Description ds = new Description { DescriptionText = NewCommandDescription, Version = NewCommandVersion };
            MessageBox.Show($"Created new Command : {await _dataService.CreateCommand(c, d, ds)}");
            IsNewCommandMode = false;
        }
        [RelayCommand]
        public async Task DeleteEntry()
        {
            string isDeleted = "false";
            var res = MessageBox.Show("Are you sure ?", "Delete Command", MessageBoxButton.YesNo);
            if (res == MessageBoxResult.Yes)
            {
                isDeleted = await _dataService.DeleteCommand(SelectedCommand.ID);
                ReadCSD_DTO toDelete = _dataManager.Commands.First(c => c.ID == SelectedCommand.ID);
                _dataManager.Commands.Remove(toDelete);
                ActualIndex = 0;
            }

            MessageBox.Show(isDeleted.Equals("true") ? "Command successfully deleted." : "Command could not be deleted");
        }

        [RelayCommand]
        public void Search()
        {
            //MessageBox.Show(SearchField);
            switch (SearchField)
            {
                case "Command":
                    FilteredCommands = new ObservableCollection<ReadCSD_DTO>(Commands.Where(c => c.Command.ToLower().Contains(SearchText.ToLower())));
                    break;
                case "System":
                    FilteredCommands = new ObservableCollection<ReadCSD_DTO>(Commands.Where(c => c.System.ToLower().Contains(SearchText.ToLower())));
                    break;
                case "Technology":
                    FilteredCommands = new ObservableCollection<ReadCSD_DTO>(Commands.Where(c => c.Tech.ToLower().Contains(SearchText.ToLower())));
                    break;
                case "Language":
                    FilteredCommands = new ObservableCollection<ReadCSD_DTO>(Commands.Where(c => c.Lang.ToLower().Contains(SearchText.ToLower())));
                    break;
                case "Version":
                    FilteredCommands = new ObservableCollection<ReadCSD_DTO>(Commands.Where(c => c.Version.ToLower().Contains(SearchText.ToLower())));
                    break;
                case "Description":
                    FilteredCommands = new ObservableCollection<ReadCSD_DTO>(Commands.Where(c => c.Description.ToLower().Contains(SearchText.ToLower())));
                    break;
            }
        }        

    }
}
