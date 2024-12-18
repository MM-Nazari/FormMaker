using FormMaker.Dto;
using FormMaker.Interface;
using FormMaker.Model;
using FormMaker.Util;
using Microsoft.AspNetCore.Mvc;

namespace FormMaker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "AnswerOptions")]
    public class AnswerOptionController : ControllerBase
    {
        private readonly IAnswerOptionService _answerOptionService;

        public AnswerOptionController(IAnswerOptionService answerOptionService)
        {
            _answerOptionService = answerOptionService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<IEnumerable<AnswerOptionDto>>>> GetAllAnswerOptions()
        {
            try
            {
                ApiResponse<IEnumerable<AnswerOptionDto>> response = await _answerOptionService.GetAllAnswerOptionsAsync();
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
        public async Task<ActionResult<ApiResponse<AnswerOptionDto>>> GetAnswerOptionById(int id)
        {
            try
            {
                ApiResponse<AnswerOptionDto> response = await _answerOptionService.GetAnswerOptionByIdAsync(id);
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
        public async Task<ActionResult<ApiResponse<AnswerOptionDto>>> CreateAnswerOption([FromBody] AnswerOptionCreateDto answerOptionCreateDto)
        {
            try
            {
                ApiResponse<AnswerOptionDto> response = await _answerOptionService.CreateAnswerOptionAsync(answerOptionCreateDto);
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
        public async Task<ActionResult<ApiResponse<AnswerOptionDto>>> UpdateAnswerOption([FromBody] AnswerOptionUpdateDto answerOptionUpdateDto)
        {
            try
            {
                ApiResponse<AnswerOptionDto> response = await _answerOptionService.UpdateAnswerOptionAsync(answerOptionUpdateDto);
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
        public async Task<ActionResult<ApiResponse<bool>>> DeleteAnswerOption(int id)
        {
            try
            {
                ApiResponse<bool> response = await _answerOptionService.DeleteAnswerOptionAsync(id);
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

        [HttpGet("question/{questionId}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<AnswerOptionDto>>>> GetAnswerOptionsByQuestionId(int questionId)
        {
            try
            {
                ApiResponse<IEnumerable<AnswerOptionDto>> response = await _answerOptionService.GetAnswerOptionsByQuestionIdAsync(questionId);
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
