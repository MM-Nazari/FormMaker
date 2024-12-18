using FormMaker.Dto;
using FormMaker.Interface;
using FormMaker.Model;
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
            ApiResponse<IEnumerable<AnswerOptionDto>> response = await _answerOptionService.GetAllAnswerOptionsAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<AnswerOptionDto>>> GetAnswerOptionById(int id)
        {
            ApiResponse<AnswerOptionDto> response = await _answerOptionService.GetAnswerOptionByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<AnswerOptionDto>>> CreateAnswerOption([FromBody] AnswerOptionCreateDto answerOptionCreateDto)
        {
            ApiResponse<AnswerOptionDto> response = await _answerOptionService.CreateAnswerOptionAsync(answerOptionCreateDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        public async Task<ActionResult<ApiResponse<AnswerOptionDto>>> UpdateAnswerOption([FromBody] AnswerOptionUpdateDto answerOptionUpdateDto)
        {
            ApiResponse<AnswerOptionDto> response = await _answerOptionService.UpdateAnswerOptionAsync(answerOptionUpdateDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteAnswerOption(int id)
        {
            ApiResponse<bool> response = await _answerOptionService.DeleteAnswerOptionAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("question/{questionId}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<AnswerOptionDto>>>> GetAnswerOptionsByQuestionId(int questionId)
        {
            ApiResponse<IEnumerable<AnswerOptionDto>> response = await _answerOptionService.GetAnswerOptionsByQuestionIdAsync(questionId);
            return StatusCode(response.StatusCode, response);
        }
    }
}
