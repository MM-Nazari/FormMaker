using FormMaker.Dto;
using FormMaker.Interface;
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
        public async Task<IActionResult> GetAllAnswerOptions()
        {
            var response = await _answerOptionService.GetAllAnswerOptionsAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAnswerOptionById(int id)
        {
            var response = await _answerOptionService.GetAnswerOptionByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnswerOption([FromBody] AnswerOptionCreateDto answerOptionCreateDto)
        {
            var response = await _answerOptionService.CreateAnswerOptionAsync(answerOptionCreateDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAnswerOption([FromBody] AnswerOptionUpdateDto answerOptionUpdateDto)
        {
            var response = await _answerOptionService.UpdateAnswerOptionAsync(answerOptionUpdateDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnswerOption(int id)
        {
            var response = await _answerOptionService.DeleteAnswerOptionAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("question/{questionId}")]
        public async Task<IActionResult> GetAnswerOptionsByQuestionId(int questionId)
        {
            var response = await _answerOptionService.GetAnswerOptionsByQuestionIdAsync(questionId);
            return StatusCode(response.StatusCode, response);
        }
    }
}
