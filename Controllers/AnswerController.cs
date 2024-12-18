using FormMaker.Dto;
using FormMaker.Interface;
using FormMaker.Model;
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
        public async Task<ActionResult<ApiResponse<IEnumerable<AnswerDto>>>> GetAllAnswers()
        {
            ApiResponse<IEnumerable<AnswerDto>> response = await _answerService.GetAllAnswersAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<AnswerDto>>> GetAnswerById(int id)
        {
            ApiResponse<AnswerDto> response = await _answerService.GetAnswerByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<AnswerDto>>> CreateAnswer([FromBody] AnswerCreateDto answerCreateDto)
        {
            ApiResponse<AnswerDto> response = await _answerService.CreateAnswerAsync(answerCreateDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        public async Task<ActionResult<ApiResponse<AnswerDto>>> UpdateAnswer([FromBody] AnswerUpdateDto answerUpdateDto)
        {
            var response = await _answerService.UpdateAnswerAsync(answerUpdateDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteAnswer(int id)
        {
            ApiResponse<bool> response = await _answerService.DeleteAnswerAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
