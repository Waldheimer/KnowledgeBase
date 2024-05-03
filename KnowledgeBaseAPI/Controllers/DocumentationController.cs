using KnowledgeBaseAPI.Data.DTOs;
using KnowledgeBaseAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeBaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentationController : ControllerBase
    {
        private DocumentationService _service;

        public DocumentationController(DocumentationService service)
        {
            _service = service;
        }

        [HttpPost]
        public ActionResult<string> CreateDocumentation([FromBody] CreateCSD_DTO documentationDTO)
        {
            var success = _service.CreateEntry(documentationDTO);

            return Ok(success);
        }

        [HttpGet]
        public ActionResult<IEnumerable<ReadCSD_DTO>> GetDocumentationById(Guid id)
        {
            if (id == Guid.Empty) { return Ok(_service.GetEntries()); }
            return Ok(_service.GetEntryById(id));
        }

        [HttpGet("lang/{lang}")]
        public ActionResult<IEnumerable<ReadCSD_DTO>> GetDocumentationsByLang(string lang)
        {
            return Ok(_service.GetEntriesByLang(lang));
        }

        [HttpGet("tech/{tech}")]
        public ActionResult<IEnumerable<ReadCSD_DTO>> GetDocumentationByTech(string tech)
        {
            return Ok(_service.GetEntriesByTech(tech));
        }

        [HttpGet("system/{system}")]
        public ActionResult<IEnumerable<ReadCSD_DTO>> GetDocumentationsBySystem(string system)
        {
            return Ok(_service.GetEntriesBySystem(system));
        }

        [HttpDelete("{id}")]
        public ActionResult<bool> DeleteDocumentationByID(string id)
        {
            return Ok(_service.DeleteEntry(id));
        }

        [HttpPut("update")]
        public ActionResult<string> UpdateDocumentation([FromBody] UpdateCSD_DTO documentationDTO)
        {
            var result = _service.UpdateEntry(documentationDTO);
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
