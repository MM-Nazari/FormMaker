using FormMaker.Dto;
using FormMaker.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FormMaker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "FormProcessess")]
    public class FormProcessController : ControllerBase
    {
        private readonly IFormProcessService _formProcessService;

        public FormProcessController(IFormProcessService formProcessService)
        {
            _formProcessService = formProcessService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFormProcesses()
        {
            var response = await _formProcessService.GetAllFormProcessesAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFormProcessById(int id)
        {
            var response = await _formProcessService.GetFormProcessByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFormProcess([FromBody] FormProcessDTO formProcessDto)
        {
            var response = await _formProcessService.CreateFormProcessAsync(formProcessDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFormProcess(int id, [FromBody] FormProcessDTO formProcessDto)
        {
            var response = await _formProcessService.UpdateFormProcessAsync(id, formProcessDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFormProcess(int id)
        {
            var response = await _formProcessService.DeleteFormProcessAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
