namespace KnowledgeBase.WPF.Models
{
    public class Command
    {
        public Guid Descriptor { get; set; }
        public string CommandText { get; set; } = string.Empty;
    }
    public class Snippet
    {
        public Guid Descriptor { get; set; }
        public string CommandText { get; set; } = string.Empty;
    }
    public class Documentation
    {
        public Guid Descriptor { get; set; }
        public string DocumentationText { get; set; } = string.Empty;
    }
    public class Descriptor
    {
        public Guid ID { get; set; }
        public string System { get; set; } = string.Empty;
        public string Tech { get; set; } = string.Empty;
        public string Lang { get; set; } = string.Empty;
    }
    public class Description
    {
        public Guid ID { get; set; }
        public string DescriptionText { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
    }
}
