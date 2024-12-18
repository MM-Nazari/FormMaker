using FormMaker.Dto;
using FormMaker.Interface;
using FormMaker.Model;
using FormMaker.Service;
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
            ApiResponse<QuestionDto> response = await _questionService.GetQuestionByIdAsync(id);
            if (response.Success)
                return Ok(response);
            return NotFound(response);
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<QuestionDto>>>> GetAllQuestions()
        {
            ApiResponse<IEnumerable<QuestionDto>> response = await _questionService.GetAllQuestionsAsync();
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<QuestionDto>>> CreateQuestion([FromBody] QuestionCreateDto questionCreateDto)
        {
            ApiResponse<QuestionDto> response = await _questionService.CreateQuestionAsync(questionCreateDto);
            if (response.Success)
                return CreatedAtAction(nameof(GetQuestion), new { id = response.Data.QuestionID }, response);
            return BadRequest(response);
        }

        [HttpPut]
        public async Task<ActionResult<ApiResponse<QuestionDto>>> UpdateQuestion([FromBody] QuestionUpdateDto questionUpdateDto)
        {
            ApiResponse<QuestionDto> response = await _questionService.UpdateQuestionAsync(questionUpdateDto);
            if (response.Success)
                return Ok(response);
            return NotFound(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteQuestion(int id)
        {
            ApiResponse<bool> response = await _questionService.DeleteQuestionAsync(id);
            if (response.Success)
                return NoContent();
            return NotFound(response);
        }

        [HttpGet("frequent")]
        public async Task<ActionResult<ApiResponse<IEnumerable<QuestionDto>>>> GetFrequentQuestions()
        {
            ApiResponse<IEnumerable<QuestionDto>> response = await _questionService.GetFrequentQuestionsAsync();
            return StatusCode(response.StatusCode, response);
        }

    }
}
