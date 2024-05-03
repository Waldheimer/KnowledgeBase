
using System.Collections.ObjectModel;
using System.Net.NetworkInformation;
using KnowledgeBase.WPF.Models;

namespace KnowledgeBase.WPF.Manager
{
    /// <summary>
    /// Handling the global State of all ApiData
    /// </summary>
    public class ApiDataManager
    {
        #region Properties
        private ObservableCollection<ReadCSD_DTO> _commands;
        public ObservableCollection<ReadCSD_DTO> Commands
        {
            get => _commands;
            set
            {
                _commands = value;
                OnCommandsListChanged();
            }
        }
        private ObservableCollection<ReadCSD_DTO> _snippets;
        public ObservableCollection<ReadCSD_DTO> Snippets
        {
            get => _snippets;
            set
            {
                _snippets = value;
                OnSnippetsListChanged();
            }
        }
        private ObservableCollection<ReadCSD_DTO> _documentations;
        public ObservableCollection<ReadCSD_DTO> Documentations
        {
            get => _documentations;
            set
            {
                _documentations = value;
                OnDocumentationsListChanged();
            }
        }

        private ObservableCollection<string> _systems;
        public ObservableCollection<string> Systems
        {
            get => _systems;
            set
            {
                _systems = value;
                OnSystemListChanged();
            }
        }
        private ObservableCollection<string> _techs;
        public ObservableCollection<string> Techs
        {
            get => _techs;
            set
            {
                _techs = value;
                OnTechListChanged();
            }
        }
        private ObservableCollection<string> _langs;
        public ObservableCollection<string> Langs
        {
            get => _langs;
            set
            {
                _langs = value;
                OnLangListChanged();
            }
        }
        #endregion

        #region Events & Handler
        public event Action? CommandsListChanged;
        public event Action? SnippetsListChanged;
        public event Action? DocumentationsListChanged;

        public event Action? SystemListChanged;
        public event Action? TechListChanged;
        public event Action? LangListChanged;

        public void OnCommandsListChanged()
        {
            CommandsListChanged?.Invoke();
        }
        public void OnSnippetsListChanged()
        {
            SnippetsListChanged?.Invoke();
        }
        public void OnDocumentationsListChanged()
        {
            DocumentationsListChanged?.Invoke();
        }
        private void OnSystemListChanged()
        {
            SystemListChanged?.Invoke();
        }
        private void OnTechListChanged()
        {
            TechListChanged?.Invoke();
        }
        private void OnLangListChanged()
        {
            LangListChanged?.Invoke();
        }
        #endregion
    }
}
