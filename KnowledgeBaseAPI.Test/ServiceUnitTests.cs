using System.Net.Http;
using System.Net.Http.Json;
using KnowledgeBaseAPI.Data.DataContext;
using KnowledgeBaseAPI.Data.DTOs;
using KnowledgeBaseAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace KnowledgeBaseAPI.Test
{
    public class ServiceUnitTests
    {
        ServiceCollection services;
        ServiceProvider provider;
        CommandService cservice;

        [SetUp]
        public void Setup()
        {
            services = new ServiceCollection();
            services.AddDbContext<KB_DataContext>(options =>
            {
                options.UseSqlServer("Server=127.0.0.1,1433;Database=KnowledgeBase;User ID=sa;Password=Ax7bch81v;TrustServerCertificate=True;");
            });
            services.AddScoped<CommandService>();
            services.AddScoped<DocumentationService>();
            services.AddScoped<SnippetService>();
            services.AddSingleton<HttpClient>();

            provider = services.BuildServiceProvider();
            cservice = provider.GetRequiredService<CommandService>();
        }

        [Test]
        public void ReturnsValidGUID_OnSuccessfulCommandCreation()
        {
            CreateCSD_DTO dto = new CreateCSD_DTO
            {
                Command = "TestCommand",
                System = "TestSystem",
                Tech = "TestTech",
                Lang = "TestLang",
                Description = "none",
                Version = "0.0.0-alpha.test"
            };

            var result = cservice.CreateEntry(dto);
            var validGuid = Guid.TryParse(result, out Guid id);

            Console.WriteLine(result);

            Assert.IsNotNull(result);
            Assert.IsNotEmpty(id.ToString());
            Assert.That(result, Is.EqualTo(id.ToString()));
        }
        [Test]
        public void ReturnsListOfCommandsWhenNoIdGiven()
        {
            var result = cservice.GetEntries();

            Console.WriteLine(result.Count());
            Assert.NotNull(result);
            Assert.That(result.Count, Is.AtLeast(1));
        }
        [Test]
        public void ReturnsCommandWhenValidIdGiven()
        {
            string id = "277ffca3-dadf-4852-80f8-0ce10e27d31a";
            var result = cservice.GetEntryById(Guid.Parse(id));

            foreach (var item in result)
            {
                Console.WriteLine(item.Command);
            }
            Assert.NotNull(result);
            Assert.That(result is IEnumerable<ReadCSD_DTO>);
        }


        [TearDown]
        public void End()
        {
            services.Clear();
            
            provider.Dispose();
        }
    }
}