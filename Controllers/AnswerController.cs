using FormMaker.Dto;
using FormMaker.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FormMaker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Answers")]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerService _answerService;

        public AnswerController(IAnswerService answerService)
        {
            _answerService = answerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAnswers()
        {
            var response = await _answerService.GetAllAnswersAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAnswerById(int id)
        {
            var response = await _answerService.GetAnswerByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnswer([FromBody] AnswerCreateDto answerCreateDto)
        {
            var response = await _answerService.CreateAnswerAsync(answerCreateDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAnswer([FromBody] AnswerUpdateDto answerUpdateDto)
        {
            var response = await _answerService.UpdateAnswerAsync(answerUpdateDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnswer(int id)
        {
            var response = await _answerService.DeleteAnswerAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
