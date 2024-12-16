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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAnswerOption(int id, [FromBody] AnswerOptionUpdateDto answerOptionUpdateDto)
        {
            var response = await _answerOptionService.UpdateAnswerOptionAsync(id, answerOptionUpdateDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnswerOption(int id)
        {
            var response = await _answerOptionService.DeleteAnswerOptionAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
