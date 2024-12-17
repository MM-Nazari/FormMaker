using FormMaker.Dto;
using FormMaker.Interface;
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
        public async Task<IActionResult> GetQuestion(int id)
        {
            var response = await _questionService.GetQuestionByIdAsync(id);
            if (response.Success)
                return Ok(response);
            return NotFound(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllQuestions()
        {
            var response = await _questionService.GetAllQuestionsAsync();
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuestion([FromBody] QuestionCreateDto questionCreateDto)
        {
            var response = await _questionService.CreateQuestionAsync(questionCreateDto);
            if (response.Success)
                return CreatedAtAction(nameof(GetQuestion), new { id = response.Data.QuestionID }, response);
            return BadRequest(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateQuestion([FromBody] QuestionUpdateDto questionUpdateDto)
        {
            var response = await _questionService.UpdateQuestionAsync(questionUpdateDto);
            if (response.Success)
                return Ok(response);
            return NotFound(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            var response = await _questionService.DeleteQuestionAsync(id);
            if (response.Success)
                return NoContent();
            return NotFound(response);
        }

        [HttpGet("frequent")]
        public async Task<IActionResult> GetFrequentQuestions()
        {
            var response = await _questionService.GetFrequentQuestionsAsync();
            return StatusCode(response.StatusCode, response);
        }

    }
}
