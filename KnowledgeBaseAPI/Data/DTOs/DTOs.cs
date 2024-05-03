namespace KnowledgeBaseAPI.Data.DTOs
{
    /// <summary>
    /// DTO for Creating/Reading/Updating Commands/Snippets/Documentations
    /// </summary>

    public class CreateCSD_DTO
    {       
        public string Command { get; set; } = string.Empty;
        public string System { get; set; } = string.Empty;
        public string Tech { get; set; } = string.Empty;
        public string Lang { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Version { get; set; } = "1.0.0";
    }
    public class ReadCSD_DTO : CreateCSD_DTO
    {
        public Guid ID { get; set; }
    }
    public class UpdateCSD_DTO : ReadCSD_DTO
    {
        public Guid ID { get; set; }
    }
}
