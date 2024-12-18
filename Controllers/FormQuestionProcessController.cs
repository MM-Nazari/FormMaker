using FormMaker.Dto;
using FormMaker.Interface;
using FormMaker.Model;
using Microsoft.AspNetCore.Mvc;

namespace FormMaker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "FormQuestionProcesses")]
    public class FormQuestionProcessesController : ControllerBase
    {
        private readonly IFormQuestionProcessService _formQuestionProcessService;

        public FormQuestionProcessesController(IFormQuestionProcessService service)
        {
            _formQuestionProcessService = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<FormQuestionProcessDto>>> GetFormQuestionProcess(int id)
        {
            ApiResponse<FormQuestionProcessDto> response = await _formQuestionProcessService.GetFormQuestionProcessAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<FormQuestionProcessDto>>>> GetAllFormQuestionProcesses()
        {
            ApiResponse<List<FormQuestionProcessDto>> response = await _formQuestionProcessService.GetAllFormQuestionProcessesAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<FormQuestionProcessDto>>> CreateFormQuestionProcess([FromBody] FormQuestionProcessCreateDto formQuestionProcessCreateDto)
        {
            ApiResponse<FormQuestionProcessDto> response = await _formQuestionProcessService.CreateFormQuestionProcessAsync(formQuestionProcessCreateDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        public async Task<ActionResult<ApiResponse<FormQuestionProcessDto>>> UpdateFormQuestionProcess([FromBody] FormQuestionProcessUpdateDto formQuestionProcessUpdateDto)
        {
            ApiResponse<FormQuestionProcessDto> response = await _formQuestionProcessService.UpdateFormQuestionProcessAsync(formQuestionProcessUpdateDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteFormQuestionProcess(int id)
        {
            ApiResponse<bool> response = await _formQuestionProcessService.DeleteFormQuestionProcessAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{processId}/answers")]
        public async Task<ActionResult<ApiResponse<IEnumerable<FormQuestionProcessDto>>>> GetAnswersByProcessId(int processId)
        {
            ApiResponse<IEnumerable<FormQuestionProcessDto>> response = await _formQuestionProcessService.GetAnswersByProcessIdAsync(processId);
            return StatusCode(response.StatusCode, response);
        }
    }
}
