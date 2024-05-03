using KnowledgeBaseAPI.Data.DTOs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KnowledgeBaseAPI.Data
{
    public class Descriptor
    {
        [Key]
        public Guid ID { get; set; }
        public string System {  get; set; } = string.Empty;
        public string Tech { get; set; } = string.Empty;
        public string Lang { get; set; } = string.Empty;
        public int Description { get; set; }

        public static Descriptor fromUpdateCSD_DTO(UpdateCSD_DTO dto)
        {
            return new Descriptor { ID = dto.ID, System = dto.System, Tech = dto.Tech, Lang = dto.Lang };
        }
        public static Descriptor fromCreateCSD_DTO(Guid id,CreateCSD_DTO dto)
        {
            return new Descriptor
            {
                ID = id,
                System = dto.System,
                Tech = dto.Tech,
                Lang = dto.Lang

            };
        }
    }
}
