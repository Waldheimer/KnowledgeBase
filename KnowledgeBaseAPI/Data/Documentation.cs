using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using KnowledgeBaseAPI.Data.DTOs;

namespace KnowledgeBaseAPI.Data
{
    public class Documentation
    {
        [Key]
        public Guid Descriptor { get; set; }
        public string DocumentationText { get; set; } = string.Empty;
        public static Documentation fromUpdateCSD_DTO(UpdateCSD_DTO dto)
        {
            return new Documentation { Descriptor = dto.ID, DocumentationText = dto.Command };
        }
        public static Documentation fromCreateCSD_DTO(Guid id, CreateCSD_DTO dto)
        {
            return new Documentation { Descriptor = id, DocumentationText = dto.Command };

        }
    }
}
