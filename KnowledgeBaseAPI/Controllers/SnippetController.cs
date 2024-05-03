using KnowledgeBaseAPI.Data.DTOs;
using KnowledgeBaseAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeBaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SnippetController : ControllerBase
    {
        private SnippetService _service;

        public SnippetController(SnippetService service)
        {
            _service = service;
        }

        [HttpPost]
        public ActionResult<string> CreateSnippet([FromBody] CreateCSD_DTO snippetDTO)
        {
            var success = _service.CreateEntry(snippetDTO);

            return Ok(success);
        }

        [HttpGet]
        public ActionResult<IEnumerable<ReadCSD_DTO>> GetSnippetById(Guid id)
        {
            if (id == Guid.Empty) { return Ok(_service.GetEntries()); }
            return Ok(_service.GetEntryById(id));
        }

        [HttpGet("lang/{lang}")]
        public ActionResult<IEnumerable<ReadCSD_DTO>> GetSnippetsByLang(string lang)
        {
            return Ok(_service.GetEntriesByLang(lang));
        }

        [HttpGet("tech/{tech}")]
        public ActionResult<IEnumerable<ReadCSD_DTO>> GetSnippetsByTech(string tech)
        {
            return Ok(_service.GetEntriesByTech(tech));
        }

        [HttpGet("system/{system}")]
        public ActionResult<IEnumerable<ReadCSD_DTO>> GetSnippetsBySystem(string system)
        {
            return Ok(_service.GetEntriesBySystem(system));
        }

        [HttpDelete("{id}")]
        public ActionResult<bool> DeleteSnippetByID(String id)
        {
            return Ok(_service.DeleteEntry(id));
        }

        [HttpPut("update")]
        public ActionResult<string> UpdateSnippet([FromBody] UpdateCSD_DTO snippetDTO)
        {
            var result = _service.UpdateEntry(snippetDTO);
            if (result)
            {
                return Ok("Documentation successfully updated");
            }
            else
            {
                return BadRequest("Something went wrong");
            }
        }
    }
}
