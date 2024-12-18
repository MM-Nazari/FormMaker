using FormMaker.Dto;
using FormMaker.Interface;
using FormMaker.Model;
using FormMaker.Util;
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
            try
            {
                ApiResponse<IEnumerable<FormProcessDto>> response = await _formProcessService.GetAllFormProcessesAsync();
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

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<AnswerDto>>> GetFormProcessById(int id)
        {
            try
            {
                ApiResponse<FormProcessDto> response = await _formProcessService.GetFormProcessByIdAsync(id);
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
        public async Task<ActionResult<ApiResponse<FormProcessDto>>> CreateFormProcess([FromBody] FormProcessCreateDto formProcessCreateDto)
        {
            try
            {
                ApiResponse<FormProcessDto> response = await _formProcessService.CreateFormProcessAsync(formProcessCreateDto);
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
        public async Task<ActionResult<ApiResponse<FormProcessDto>>> UpdateFormProcess([FromBody] FormProcessUpdateDto formProcessUpdateDto)
        {
            try
            {
                var response = await _formProcessService.UpdateFormProcessAsync(formProcessUpdateDto);
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
        public async Task<ActionResult<ApiResponse<bool>>> DeleteFormProcess(int id)
        {
            try
            {
                ApiResponse<bool> response = await _formProcessService.DeleteFormProcessAsync(id);
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

        [HttpGet("{processId}/forms")]
        public async Task<IActionResult> GetFormsByProcessId(int processId)
        {
            try
            {
                var response = await _formProcessService.GetFormsByProcessIdAsync(processId);
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
