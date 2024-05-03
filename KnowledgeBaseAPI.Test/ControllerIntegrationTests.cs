using KnowledgeBaseAPI.Data.DataContext;
using KnowledgeBaseAPI.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WPF_Navigation.Manager;
using WPF_Navigation.Models;
using WPF_Navigation.Query;
using WPF_Navigation.Services;
//using System.Web.Http.Controllers;

namespace KnowledgeBaseAPI.Test
{
    public class ControllerIntegrationTests
    {
        ServiceCollection services;
        ServiceProvider provider;
        CommandService cservice;
        ApiQuery query;

        private readonly Guid validGUID = Guid.Parse("677b1733-e73e-4228-bb02-c29f370c625d");
        private readonly Guid invalidGUID = Guid.Parse("677b1733-e73e-4228-bb02-010101010101");

        [SetUp]
        public void Setup()
        {
            services = new ServiceCollection();
            services.AddDbContext<KB_DataContext>(options =>
            {
                options.UseSqlServer("Server=127.0.0.1,1433;Database=KnowledgeBase;User ID=sa;Password=Ax7bch81v;TrustServerCertificate=True;");
            });
            services.AddSingleton<ApiQuery>();
            services.AddSingleton<ApiDataService>();
            services.AddSingleton<ApiDataManager>();
            services.AddSingleton<HttpClient>();

            provider = services.BuildServiceProvider();
            query = provider.GetRequiredService<ApiQuery>();
        }
        #region ApiQuery Testing
        [Test]
        public async Task GetAllCommandsWhenHitGetByIdEnpointWithoutId()
        {
            var res = await query.GetAllCommandsAsync();
            Console.WriteLine(res.Count());
            Assert.IsNotNull(res);
        }
        [Test]
        public async Task GetSingleCommandWhenGivenValidGuid()
        {
            var vres = await query.GetCommandByIdAsync(validGUID);
            var ires = await query.GetCommandByIdAsync(invalidGUID);
            
            Assert.IsNotNull(vres);
            Assert.That(vres.ID, Is.EqualTo(validGUID));
            Assert.That(ires.ID, Is.EqualTo(Guid.Empty));
        }
        [Test]
        public async Task GetListOfCommandsWhenValidSystemGiven()
        {
            var res = await query.GetCommandsBySystemAsync("Windows");
            var ires = await query.GetCommandsBySystemAsync("asdasf");

            Assert.IsNotNull(res);
            Assert.IsNotEmpty(res);
            Assert.IsEmpty(ires);
        }
        [Test]
        public async Task GetListOfCommandsWhenValidTechGiven()
        {
            var res = await query.GetCommandsByTechAsync("Git");
            var ires = await query.GetCommandsByTechAsync("asdasf");

            Assert.IsNotNull(res);
            Assert.IsNotEmpty(res);
            Assert.IsEmpty(ires);
        }
        [Test]
        public async Task GetListOfCommandsWhenValidLangGiven()
        {
            var res = await query.GetCommandsByLangAsync("bash");
            var ires = await query.GetCommandsByTechAsync("Gasddsaf");

            Assert.IsNotNull(res);
            Assert.IsNotEmpty(res);
            Assert.IsEmpty(ires);
        }
        #endregion

        #region ApiDataService Testing

        [Test]
        public void CommandInDbWhenCreatedByService()
        {

        }

        #endregion

        #region ApiDataManager Testing



        #endregion

        [TearDown] 
        public void TearDown() { provider.Dispose(); }







    }
}
