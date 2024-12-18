using FormMaker.Dto;
using FormMaker.Interface;
using FormMaker.Model;
using FormMaker.Service;
using FormMaker.Util;
using Microsoft.AspNetCore.Mvc;

namespace FormMaker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Questions")]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionsController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<QuestionDto>>> GetQuestion(int id)
        {
            try
            {
                ApiResponse<QuestionDto> response = await _questionService.GetQuestionByIdAsync(id);
                if (response.Success)
                    return Ok(response);
                return NotFound(response);
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
        public async Task<ActionResult<ApiResponse<IEnumerable<QuestionDto>>>> GetAllQuestions()
        {
            try
            {
                ApiResponse<IEnumerable<QuestionDto>> response = await _questionService.GetAllQuestionsAsync();
                return Ok(response);
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
        public async Task<ActionResult<ApiResponse<QuestionDto>>> CreateQuestion([FromBody] QuestionCreateDto questionCreateDto)
        {
            try
            {
                ApiResponse<QuestionDto> response = await _questionService.CreateQuestionAsync(questionCreateDto);
                if (response.Success)
                    return CreatedAtAction(nameof(GetQuestion), new { id = response.Data.QuestionID }, response);
                return BadRequest(response);
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
        public async Task<ActionResult<ApiResponse<QuestionDto>>> UpdateQuestion([FromBody] QuestionUpdateDto questionUpdateDto)
        {
            try
            {
                ApiResponse<QuestionDto> response = await _questionService.UpdateQuestionAsync(questionUpdateDto);
                if (response.Success)
                    return Ok(response);
                return NotFound(response);
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
        public async Task<ActionResult<ApiResponse<bool>>> DeleteQuestion(int id)
        {
            try
            {
                ApiResponse<bool> response = await _questionService.DeleteQuestionAsync(id);
                if (response.Success)
                    return NoContent();
                return NotFound(response);
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

        [HttpGet("frequent")]
        public async Task<ActionResult<ApiResponse<IEnumerable<QuestionDto>>>> GetFrequentQuestions()
        {
            try
            {
                ApiResponse<IEnumerable<QuestionDto>> response = await _questionService.GetFrequentQuestionsAsync();
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

        [HttpPost("Question-to-Form")]
        public async Task<ActionResult<ApiResponse<FormQuestionProcessAllDto>>> CreateQuestionAndLinkToForm([FromBody] CreateQuestionAndLinkToFormDto createDto)
        {
            try
            {
                ApiResponse<FormQuestionProcessAllDto> response = await _questionService.CreateQuestionAndLinkToFormAsync(createDto);
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
