using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using KnowledgeBaseAPI.Data.DTOs;

namespace KnowledgeBaseAPI.Data
{
    public class Snippet
    {
        [Key]
        public Guid Descriptor { get; set; }
        public string CommandText { get; set; } = string.Empty;
        public static Snippet fromUpdateCSD_DTO(UpdateCSD_DTO dto)
        {
            return new Snippet { Descriptor = dto.ID, CommandText = dto.Command };
        }
        public static Snippet fromCreateCSD_DTO(Guid id,CreateCSD_DTO dto)
        {
            return new Snippet { Descriptor = id, CommandText = dto.Command };
        }
    }
}
