using FormMaker.Dto;
using FormMaker.Interface;
using FormMaker.Model;
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
        public async Task<ActionResult<ApiResponse<IEnumerable<FormProcessDto>>>> GetAllFormProcesses()
        {
            ApiResponse<IEnumerable<FormProcessDto>> response = await _formProcessService.GetAllFormProcessesAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<AnswerDto>>> GetFormProcessById(int id)
        {
            ApiResponse<FormProcessDto> response = await _formProcessService.GetFormProcessByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<FormProcessDto>>> CreateFormProcess([FromBody] FormProcessCreateDto formProcessCreateDto)
        {
            ApiResponse<FormProcessDto> response = await _formProcessService.CreateFormProcessAsync(formProcessCreateDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        public async Task<ActionResult<ApiResponse<FormProcessDto>>> UpdateFormProcess([FromBody] FormProcessUpdateDto formProcessUpdateDto)
        {
            var response = await _formProcessService.UpdateFormProcessAsync(formProcessUpdateDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteFormProcess(int id)
        {
            ApiResponse<bool> response = await _formProcessService.DeleteFormProcessAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{processId}/forms")]
        public async Task<IActionResult> GetFormsByProcessId(int processId)
        {
            var response = await _formProcessService.GetFormsByProcessIdAsync(processId);
            return StatusCode(response.StatusCode, response);
        }
    }
}
