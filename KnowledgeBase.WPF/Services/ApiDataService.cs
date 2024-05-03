
using System.Collections.ObjectModel;
using KnowledgeBase.WPF.Manager;
using KnowledgeBase.WPF.Models;
using KnowledgeBase.WPF.Query;

namespace KnowledgeBase.WPF.Services
{
    public class ApiDataService
    {
        private readonly ApiQuery _query;
        private readonly ApiDataManager _dataManager;
        public ApiDataService(ApiDataManager dataManager, ApiQuery query)
        {
            _query = query;
            _dataManager = dataManager;
        }

        #region Commands
        public async Task<string> CreateCommand(Command c, Descriptor d, Description desc)
        {
            string ID = await _query.CreateCommandAsync(c,d,desc);
            if(ID != null)
            {
                _dataManager.Commands.Add(new ReadCSD_DTO()
                {
                    ID = Guid.Parse(ID), Command = c.CommandText, System = d.System, Tech = d.Tech, Lang = d.Lang,
                    Description = desc.DescriptionText, Version = desc.Version
                });
                return ID;
            }
            return string.Empty;
        }
        public async Task<string> DeleteCommand(Guid id)
        {
            return await _query.DeleteCommandAsync(id);
        }
        public async Task<string> UpdateCommandAsync(UpdateCSD_DTO dto)
        {
            return await _query.UpdateCommandAsync(dto);
        }
        public async Task<IEnumerable<ReadCSD_DTO>> GetAllCommands() 
        { 
            var Commands = await _query.GetAllCommandsAsync();
            _dataManager.Commands = new ObservableCollection<ReadCSD_DTO>(Commands);
            return Commands;
        }

        #endregion

        #region Snippets
        public async Task<IEnumerable<ReadCSD_DTO>> GetAllSnippets()
        {
            var Snippets = await _query.GetAllSnippetsAsync();
            _dataManager.Snippets = new ObservableCollection<ReadCSD_DTO>(Snippets);
            return Snippets;
        }
        #endregion

        #region Documentations
        public async Task<IEnumerable<ReadCSD_DTO>> GetAllDocumentations()
        {
            var Docs = await _query.GetAllDocumentationsAsync();
            _dataManager.Documentations = new ObservableCollection<ReadCSD_DTO>(Docs);
            return Docs;
        }
        #endregion

        #region Infos
        public async Task GetAllSystems()
        {
            var Systems = await _query.GetAllSystemsAsync();
            _dataManager.Systems = new ObservableCollection<string>(Systems);
            return;
        }
        public async Task GetAllTechs()
        {
            var Techs = await _query.GetAllTechsAsync();
            _dataManager.Techs = new ObservableCollection<string>(Techs);
            return;
        }
        public async Task GetAllLangs()
        {
            var Langs = await _query.GetAllLangsAsync();
            _dataManager.Langs = new ObservableCollection<string>(Langs);
            return;
        }

        #endregion

    }




}
