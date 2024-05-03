using KnowledgeBaseAPI.Data.DTOs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace KnowledgeBaseAPI.Data
{
    public class Command
    {
        [Key]
        public Guid Descriptor { get; set; }
        public string CommandText { get; set; } = string.Empty;

        public static Command fromUpdateCSD_DTO(UpdateCSD_DTO dto)
        {
            return new Command { Descriptor = dto.ID, CommandText = dto.Command };
        }
        public static Command fromCreateCSD_DTO(Guid id, CreateCSD_DTO dto)
        {
            return new Command { Descriptor = id, CommandText = dto.Command };
        }
    }

}
