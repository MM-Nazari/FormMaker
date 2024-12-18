using FormMaker.Dto;
using FormMaker.Interface;
using FormMaker.Model;
using FormMaker.Util;
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
            try
            {
                ApiResponse<FormQuestionProcessDto> response = await _formQuestionProcessService.GetFormQuestionProcessAsync(id);
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception ex)
            {
                ApiResponse<string> errorResponse = new ApiResponse<string>(
                    false,
                    ResponseMessage.InternalServerError,
                    ex.Message,
                    StatusCodes.Status500InternalServerError
                );
                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }

        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<FormQuestionProcessDto>>>> GetAllFormQuestionProcesses()
        {
            try
            {
                ApiResponse<List<FormQuestionProcessDto>> response = await _formQuestionProcessService.GetAllFormQuestionProcessesAsync();
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception ex)
            {
                ApiResponse<string> errorResponse = new ApiResponse<string>(
                    false,
                    ResponseMessage.InternalServerError,
                    ex.Message,
                    StatusCodes.Status500InternalServerError
                );
                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }

        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<FormQuestionProcessDto>>> CreateFormQuestionProcess([FromBody] FormQuestionProcessCreateDto formQuestionProcessCreateDto)
        {
            try
            {
                ApiResponse<FormQuestionProcessDto> response = await _formQuestionProcessService.CreateFormQuestionProcessAsync(formQuestionProcessCreateDto);
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception ex)
            {
                ApiResponse<string> errorResponse = new ApiResponse<string>(
                    false,
                    ResponseMessage.InternalServerError,
                    ex.Message,
                    StatusCodes.Status500InternalServerError
                );
                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }

        }

        [HttpPut]
        public async Task<ActionResult<ApiResponse<FormQuestionProcessDto>>> UpdateFormQuestionProcess([FromBody] FormQuestionProcessUpdateDto formQuestionProcessUpdateDto)
        {
            try
            {
                ApiResponse<FormQuestionProcessDto> response = await _formQuestionProcessService.UpdateFormQuestionProcessAsync(formQuestionProcessUpdateDto);
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception ex)
            {
                ApiResponse<string> errorResponse = new ApiResponse<string>(
                    false,
                    ResponseMessage.InternalServerError,
                    ex.Message,
                    StatusCodes.Status500InternalServerError
                );
                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteFormQuestionProcess(int id)
        {
            try
            {
                ApiResponse<bool> response = await _formQuestionProcessService.DeleteFormQuestionProcessAsync(id);
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception ex)
            {
                ApiResponse<string> errorResponse = new ApiResponse<string>(
                    false,
                    ResponseMessage.InternalServerError,
                    ex.Message,
                    StatusCodes.Status500InternalServerError
                );
                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }

        }

        [HttpGet("{processId}/answers")]
        public async Task<ActionResult<ApiResponse<IEnumerable<AnswerDto>>>> GetAnswersByProcessId(int processId)
        {
            try
            {
                ApiResponse<IEnumerable<AnswerDto>> response = await _formQuestionProcessService.GetAnswersByProcessIdAsync(processId);
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception ex)
            {
                ApiResponse<string> errorResponse = new ApiResponse<string>(
                    false,
                    ResponseMessage.InternalServerError,
                    ex.Message,
                    StatusCodes.Status500InternalServerError
                );
                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }

        }
    }
}
