using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Documents;
using System.Windows.Forms;
using KnowledgeBase.WPF.Manager;
using KnowledgeBase.WPF.Models;
using static System.Net.Mime.MediaTypeNames;
using Md = MdXaml;

namespace KnowledgeBase.WPF.ViewModels
{
    public partial class CodeViewModel : ObservableRecipient
    {
        private readonly ApiDataManager _dataManager;
        private readonly Md.Markdown _mdEngine;


        public ObservableCollection<ReadCSD_DTO> Snippets => _dataManager.Snippets;

        #region ObservableProperties

        [ObservableProperty]
        private ObservableCollection<ReadCSD_DTO> filteredSnippets;
        partial void OnFilteredSnippetsChanged(ObservableCollection<ReadCSD_DTO> value)
        {
            if (value == null || value.Count == 0)
            {
                SelectedSnippet = null;
            }
        }


        [ObservableProperty]
        private int actualIndex;
        partial void OnActualIndexChanged(int value)
        {
            string text = "";
            if (FilteredSnippets == null) { SelectedSnippet = Snippets[0]; return; }
            SelectedSnippet = FilteredSnippets[value];
            CurrentDocument = generateFlowContent(selectedSnippet.Command);
        }

        [ObservableProperty]
        private ReadCSD_DTO selectedSnippet;
        partial void OnSelectedSnippetChanged(ReadCSD_DTO value)
        {
            CurrentDocument = generateFlowContent(value.Command);
        }

        [ObservableProperty]
        private FlowDocument currentDocument = new FlowDocument();

        [ObservableProperty]
        public List<string> searchItems = new List<string>() { "Snippets", "System", "Technology", "Language", "Description", "Version" };
        [ObservableProperty]
        private int searchIndex;
        partial void OnSearchIndexChanged(int oldValue, int newValue)
        {
            SearchField = SearchItems[SearchIndex];
        }
        public string SearchField = string.Empty;

        [ObservableProperty]
        private string searchText = string.Empty;

        #endregion

        public CodeViewModel(ApiDataManager dataManager)
        {
            _mdEngine = new();
            _dataManager = dataManager;
            _dataManager.SnippetsListChanged += OnSnippetsListChanged;
            ActualIndex = 0;
            SearchIndex = 1;
            FilteredSnippets = Snippets;
            SelectedSnippet = FilteredSnippets[ActualIndex];
        }

        private void OnSnippetsListChanged()
        {
            OnPropertyChanged(nameof(Snippets));
        }

        [RelayCommand]
        public void Search()
        {
            switch (SearchField)
            {
                case "Command":
                    FilteredSnippets = new ObservableCollection<ReadCSD_DTO>(Snippets.Where(c => c.Command.ToLower().Contains(SearchText.ToLower())));
                    break;
                case "System":
                    FilteredSnippets = new ObservableCollection<ReadCSD_DTO>(Snippets.Where(c => c.System.ToLower().Contains(SearchText.ToLower())));
                    break;
                case "Technology":
                    FilteredSnippets = new ObservableCollection<ReadCSD_DTO>(Snippets.Where(c => c.Tech.ToLower().Contains(SearchText.ToLower())));
                    break;
                case "Language":
                    FilteredSnippets = new ObservableCollection<ReadCSD_DTO>(Snippets.Where(c => c.Lang.ToLower().Contains(SearchText.ToLower())));
                    break;
                case "Version":
                    FilteredSnippets = new ObservableCollection<ReadCSD_DTO>(Snippets.Where(c => c.Version.ToLower().Contains(SearchText.ToLower())));
                    break;
                case "Description":
                    FilteredSnippets = new ObservableCollection<ReadCSD_DTO>(Snippets.Where(c => c.Description.ToLower().Contains(SearchText.ToLower())));
                    break;
            }
        }

        private FlowDocument generateFlowContent(string content)
        {
            string text = string.Empty;
            try
            {
                text = File.ReadAllText(SelectedSnippet.Command);

            }
            catch (Exception ex)
            {
                text = $"```cs \n {SelectedSnippet.Command} \n ```";
            }
            return _mdEngine.Transform(text);
        }
    }
}
