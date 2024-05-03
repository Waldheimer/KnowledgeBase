using Microsoft.EntityFrameworkCore;

namespace KnowledgeBaseAPI.Data.DataContext
{
    public class KB_DataContext : DbContext
    {
        public KB_DataContext(DbContextOptions<KB_DataContext> options) : base(options)
        {
        }

        public DbSet<Command> Commands { get; set; }
        public DbSet<Snippet> Snippets { get; set; }
        public DbSet<Documentation> Documentations { get; set; }
        public DbSet<Descriptor> Descriptors { get; set; }
        public DbSet<Description> Descriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
