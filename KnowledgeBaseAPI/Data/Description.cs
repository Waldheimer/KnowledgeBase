using KnowledgeBaseAPI.Data.DTOs;
using System.ComponentModel.DataAnnotations;

namespace KnowledgeBaseAPI.Data
{
    public class Description
    {
        [Key]
        public Guid ID { get; set; }
        public string DescriptionText { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;

        public static Description fromUpdateCSD_DTO(UpdateCSD_DTO dto)
        {
            return new Description { ID = dto.ID, DescriptionText = dto.Description, Version = dto.Version };
        }
        public static Description fromCreateCSD_DTO(Guid id, CreateCSD_DTO dto) 
        {
            return new Description { ID = id, DescriptionText = dto.Description };
        }
    }
}
