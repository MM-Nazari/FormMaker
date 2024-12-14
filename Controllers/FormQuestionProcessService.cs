using FormMaker.Dto;
using FormMaker.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FormMaker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "FormQuestionProcesses")]
    public class FormQuestionProcessesController : ControllerBase
    {
        private readonly IFormQuestionProcessService _service;

        public FormQuestionProcessesController(IFormQuestionProcessService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFormQuestionProcess(int id)
        {
            var response = await _service.GetFormQuestionProcessAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFormQuestionProcesses()
        {
            var response = await _service.GetAllFormQuestionProcessesAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFormQuestionProcess([FromBody] FormQuestionProcessDto formQuestionProcessDto)
        {
            var response = await _service.CreateFormQuestionProcessAsync(formQuestionProcessDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFormQuestionProcess(int id, [FromBody] FormQuestionProcessDto formQuestionProcessDto)
        {
            var response = await _service.UpdateFormQuestionProcessAsync(id, formQuestionProcessDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFormQuestionProcess(int id)
        {
            var response = await _service.DeleteFormQuestionProcessAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
