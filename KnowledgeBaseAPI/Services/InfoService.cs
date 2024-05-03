using KnowledgeBaseAPI.Data.DataContext;

namespace KnowledgeBaseAPI.Services
{
    public class InfoService
    {
        private readonly KB_DataContext _dataContext;
        public InfoService(KB_DataContext context) { _dataContext = context; }

        public IEnumerable<string> getAllSystems()
        {
            return (from data in _dataContext.Descriptors select data.System).Distinct();
        }
        public IEnumerable<string> getAllTechs()
        {
            return (from data in _dataContext.Descriptors select data.Tech).Distinct();
        }
        public IEnumerable<string> getAllLangs()
        {
            return (from data in _dataContext.Descriptors select data.Lang).Distinct();
        }

    }
}
