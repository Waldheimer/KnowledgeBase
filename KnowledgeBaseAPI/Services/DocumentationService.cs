using KnowledgeBaseAPI.Data;
using KnowledgeBaseAPI.Data.DataContext;
using KnowledgeBaseAPI.Data.DTOs;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeBaseAPI.Services
{
    public class DocumentationService : IDataService<CreateCSD_DTO, ReadCSD_DTO, UpdateCSD_DTO, Guid>
    {
        private readonly KB_DataContext _context;

        public DocumentationService(KB_DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates the Documentation, the Descriptor and the Description and connects 
        /// them together with ONE Guid
        /// </summary>
        /// <param name="item">
        /// Contains all information to create Documentation and Descriptor with optional Description
        /// Description can be added later on
        /// </param>
        /// <returns>true/false depending on success</returns>
        public string CreateEntry(CreateCSD_DTO item)
        {
            Guid guid = Guid.NewGuid();
            Description description = Description.fromCreateCSD_DTO(guid, item);
            Descriptor descriptor = Descriptor.fromCreateCSD_DTO(guid, item);
            Documentation documentation = Documentation.fromCreateCSD_DTO(guid, item);
            try
            {
                _context.Descriptors.Add(descriptor);
                _context.Documentations.Add(documentation);
                _context.Descriptions.Add(description);
                _context.SaveChanges();
                return descriptor.ID.ToString();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the Documentation with Decriptor and Description with given ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// A ReadCSD_DTO that's a combined Documentation/Descriptor/Description
        /// with matching ID
        /// </returns>
        public IEnumerable<ReadCSD_DTO> GetEntryById(Guid id)
        {
            var documentation = _context.Documentations.First(c => c.Descriptor == id);
            var descriptor = _context.Descriptors.First(c => c.ID == id);
            var description = _context.Descriptions.First(c => c.ID == id);
            ReadCSD_DTO result = new ReadCSD_DTO
            {
                Command = documentation.DocumentationText,
                Description = description.DescriptionText,
                System = descriptor.System,
                Tech = descriptor.Tech,
                Lang = descriptor.Lang,
                Version = description.Version
            };
            return new List<ReadCSD_DTO> { result };
        }
        /// <summary>
        /// Just return all Documentations if no Guid is given
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ReadCSD_DTO> GetEntries()
        {
            return from documentation in _context.Documentations
                   from descriptor in _context.Descriptors
                   from description in _context.Descriptions
                   where descriptor.ID.Equals(documentation.Descriptor) && description.ID.Equals(documentation.Descriptor)
                   select new ReadCSD_DTO
                   {
                       Command = documentation.DocumentationText,
                       Description = description.DescriptionText,
                       System = descriptor.System,
                       Tech = descriptor.Tech,
                       Lang = descriptor.Lang,
                       Version = description.Version
                   };
        }
        /// <summary>
        /// Gets all Documentations with Decriptors and Descriptions by 'Lang'-Attribute
        /// </summary>
        /// <param name="lang">The specified Lang(guage) Attribute in the Descriptor</param>
        /// <returns>
        /// A List of ReadCSD_DTOs which are combined Documentation/Descriptor/Description
        /// with matching ID
        /// </returns>
        public IEnumerable<ReadCSD_DTO> GetEntriesByLang(string lang)
        {
            var results = (from ds in _context.Descriptors
                           where ds.Lang.Equals(lang)
                           from desc in _context.Descriptions
                           where desc.ID.Equals(ds.ID)
                           from data in _context.Documentations
                           where data.Descriptor.Equals(ds.ID)
                           select new ReadCSD_DTO
                           {
                               Command = data.DocumentationText,
                               Lang = ds.Lang,
                               System = ds.System,
                               Tech = ds.Tech,
                               Description = desc.DescriptionText,
                               Version = desc.Version
                           }).ToList();
            return results;
        }

        /// <summary>
        /// Gets all Documentations with Decriptors and Descriptions by 'System'-Attribute
        /// </summary>
        /// <param name="system">The specified System Attribute in the Descriptor</param>
        /// <returns>
        /// A List of ReadCommandDTOs which are combined Documentation/Descriptor/Description
        /// with matching ID
        /// </returns>
        public IEnumerable<ReadCSD_DTO> GetEntriesBySystem(string system)
        {
            var results = (from ds in _context.Descriptors
                           where ds.System.Equals(system)
                           from desc in _context.Descriptions
                           where desc.ID.Equals(ds.ID)
                           from data in _context.Documentations
                           where data.Descriptor.Equals(ds.ID)
                           select new ReadCSD_DTO
                           {
                               Command = data.DocumentationText,
                               Lang = ds.Lang,
                               System = ds.System,
                               Tech = ds.Tech,
                               Description = desc.DescriptionText,
                               Version = desc.Version
                           }).ToList();
            return results;
        }

        /// <summary>
        /// Gets all Documentations with Decriptors and Descriptions by 'Tech'-Attribute
        /// </summary>
        /// <param name="tech">The specified Tech Attribute in the Descriptor</param>
        /// <returns>
        /// A List of ReadCSC_DTOs which are combined Documentations/Descriptor/Description
        /// with matching ID
        /// </returns>
        public IEnumerable<ReadCSD_DTO> GetEntriesByTech(string tech)
        {
            var results = (from ds in _context.Descriptors
                           where ds.Tech.Equals(tech)
                           from desc in _context.Descriptions
                           where desc.ID.Equals(ds.ID)
                           from data in _context.Documentations
                           where data.Descriptor.Equals(ds.ID)
                           select new ReadCSD_DTO
                           {
                               Command = data.DocumentationText,
                               Lang = ds.Lang,
                               System = ds.System,
                               Tech = ds.Tech,
                               Description = desc.DescriptionText,
                               Version = desc.Version
                           }).ToList();
            return results;
        }

        /// <summary>
        /// Updates a Documentation and/or the corresponding Descriptor/Description
        /// </summary>
        /// <param name="item"></param>
        /// <returns>UpdateSuccess</returns>
        public bool UpdateEntry(UpdateCSD_DTO item)
        {
            bool result = false;
            var documentation = Documentation.fromUpdateCSD_DTO(item);
            var descriptor = Descriptor.fromUpdateCSD_DTO(item);
            var description = Description.fromUpdateCSD_DTO(item);

            try
            {
                _context.Update<Documentation>(documentation);
                _context.Update<Descriptor>(descriptor);
                _context.Update<Description>(description);
                _context.SaveChanges();

                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return result;
        }

        /// <summary>
        /// Deletes the Documentation/Descriptor/Description with given ID from the DB
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Deletion Success</returns>
        public bool DeleteEntry(string id)
        {
            bool result = false;
            try
            {
                _context.Remove<Documentation>(_context.Documentations.First(c => c.Descriptor.ToString().Equals(id)));
                _context.Remove<Descriptor>(_context.Descriptors.First(c => c.ID.ToString().Equals(id)));
                _context.Remove<Description>(_context.Descriptions.First(c => c.ID.ToString().Equals(id)));
                _context.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return result;
        }
    }
}
