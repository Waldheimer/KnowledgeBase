using KnowledgeBaseAPI.Data;
using KnowledgeBaseAPI.Data.DTOs;
using KnowledgeBaseAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace KnowledgeBaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandController : ControllerBase
    {
        private CommandService _service;

        public CommandController(CommandService service)
        {
            _service = service;
        }

        [HttpPost]
        public ActionResult<string> CreateCommand([FromBody] CreateCSD_DTO commandDTO)
        {
            var success = _service.CreateEntry(commandDTO);

            return Ok(success);
        }

        [HttpGet]
        public ActionResult<IEnumerable<ReadCSD_DTO>> GetCommandById(Guid id)
        {
            if(id == Guid.Empty) { return Ok(_service.GetEntries()); }
            var result = _service.GetEntryById(id);
            if(result.IsNullOrEmpty()) 
            {
                return Ok(new List<ReadCSD_DTO>());
            }
            return Ok(_service.GetEntryById(id));
        }

        [HttpGet("lang/{lang}")]
        public ActionResult<IEnumerable<ReadCSD_DTO>> GetCommandsByLang(string lang) 
        { 
            return Ok(_service.GetEntriesByLang(lang));
        }

        [HttpGet("tech/{tech}")]
        public ActionResult<IEnumerable<ReadCSD_DTO>> GetCommandsByTech(string tech)
        {
            return Ok(_service.GetEntriesByTech(tech));
        }

        [HttpGet("system/{system}")]
        public ActionResult<IEnumerable<ReadCSD_DTO>> GetCommandsBySystem(string system)
        {
            return Ok(_service.GetEntriesBySystem(system));
        }

        [HttpDelete("{id}")]
        public ActionResult<bool> DeleteCommandByID(string id)
        {
            return Ok(_service.DeleteEntry(id));
        }

        [HttpPut("update")]
        public ActionResult<string> UpdateCommand([FromBody] UpdateCSD_DTO commandDTO)
        {
            var result = _service.UpdateEntry(commandDTO);
            if (result)
            {
                return Ok("Command successfully updated");
            }
            else
            {
                return BadRequest("Something went wrong");
            }
        }
    }
}
