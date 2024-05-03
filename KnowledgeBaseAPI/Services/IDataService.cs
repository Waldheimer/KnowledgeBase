using KnowledgeBaseAPI.Data.DTOs;

namespace KnowledgeBaseAPI.Services
{
    public interface IDataService<C,R,U,G>
    {
        string CreateEntry(C item);

        IEnumerable<R> GetEntryById(G id);
        IEnumerable<R> GetEntries();

        IEnumerable<R> GetEntriesByTech(string tech);
        IEnumerable<R> GetEntriesBySystem(string system);
        IEnumerable<R> GetEntriesByLang(string lang);

        bool UpdateEntry(U item);

        bool DeleteEntry(string item);
    }
}
