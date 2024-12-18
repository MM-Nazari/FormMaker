using FormMaker.Dto;
using FormMaker.Interface;
using FormMaker.Model;
using FormMaker.Util;
using Microsoft.AspNetCore.Mvc;

namespace FormMaker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "FormQuestions")]
    public class FormQuestionController : ControllerBase
    {
        private readonly IFormQuestionService _formQuestionService;

        public FormQuestionController(IFormQuestionService formQuestionService)
        {
            _formQuestionService = formQuestionService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<FormQuestionDto>>>> GetAllFormQuestions()
        {
            try
            {
                ApiResponse<IEnumerable<FormQuestionDto>> response = await _formQuestionService.GetAllFormQuestionsAsync();
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
        public async Task<ActionResult<ApiResponse<FormQuestionDto>>> GetFormQuestionById(int id)
        {
            try
            {
                ApiResponse<FormQuestionDto> response = await _formQuestionService.GetFormQuestionByIdAsync(id);
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
        public async Task<ActionResult<ApiResponse<FormQuestionDto>>> CreateFormQuestion([FromBody] FormQuestionCreateDto formQuestionCreateDto)
        {
            try
            {
                ApiResponse<FormQuestionDto> response = await _formQuestionService.CreateFormQuestionAsync(formQuestionCreateDto);
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
        public async Task<ActionResult<ApiResponse<FormQuestionDto>>> UpdateFormQuestion([FromBody] FormQuestionUpdateDto formQuestionUpdateDto)
        {
            try
            {
                ApiResponse<FormQuestionDto> response = await _formQuestionService.UpdateFormQuestionAsync(formQuestionUpdateDto);
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
        public async Task<ActionResult<ApiResponse<bool>>> DeleteFormQuestion(int id)
        {
            try
            {
                ApiResponse<bool> response = await _formQuestionService.DeleteFormQuestionAsync(id);
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

        [HttpGet("{formId}/questions")]
        public async Task<ActionResult<ApiResponse<IEnumerable<QuestionDto>>>> GetQuestionsByFormId(int formId)
        {
            try
            {
                ApiResponse<IEnumerable<QuestionDto>> response = await _formQuestionService.GetQuestionsByFormIdAsync(formId);
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
