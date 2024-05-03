using KnowledgeBaseAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeBaseAPI.Controllers
{
    /// <summary>
    /// Controller for Basic Route Testing
    /// and Basic API Information Gathering
    /// </summary>
    [Route("/")]
    [ApiController]
    public class InfoController : ControllerBase
    {
        //  Setup the Configuration for requesting Information
        private readonly IConfiguration _configuration;
        private readonly InfoService _service;

        public InfoController(IConfiguration configuration, InfoService service)
        {
            _configuration = configuration;
            _service = service;
        }
        //  Check if the Server is running
        [HttpGet]
        public ActionResult getStatus()
        {
            return Ok("Server up and running");
        }
        //  Get the current API Version
        [HttpGet("/version")]
        public ActionResult<string> getVersion()
        {
            return Ok($"API currently running in Version : {_configuration.GetValue<string>("version")}");
        }
        //  Gets a List of all Systems
        [HttpGet("/systemlist")]
        public ActionResult<IEnumerable<string>> GetAllVersions()
        {
            return Ok(_service.getAllSystems());
        }
        //  Gets a List of all Technologies
        [HttpGet("/techlist")]
        public ActionResult<IEnumerable<string>> GetAllTechs()
        {
            return Ok(_service.getAllTechs());
        }
        //  Gets a List of all Languages
        [HttpGet("/langlist")]
        public ActionResult<IEnumerable<string>> GetAllLangs()
        {
            return Ok(_service.getAllLangs());
        }
    }
}
