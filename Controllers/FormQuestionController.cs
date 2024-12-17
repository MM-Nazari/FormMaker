using FormMaker.Dto;
using FormMaker.Interface;
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
        public async Task<IActionResult> GetAllFormQuestions()
        {
            var response = await _formQuestionService.GetAllFormQuestionsAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFormQuestionById(int id)
        {
            var response = await _formQuestionService.GetFormQuestionByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFormQuestion([FromBody] FormQuestionCreateDto formQuestionCreateDto)
        {
            var response = await _formQuestionService.CreateFormQuestionAsync(formQuestionCreateDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFormQuestion([FromBody] FormQuestionUpdateDto formQuestionUpdateDto)
        {
            var response = await _formQuestionService.UpdateFormQuestionAsync(formQuestionUpdateDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFormQuestion(int id)
        {
            var response = await _formQuestionService.DeleteFormQuestionAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
