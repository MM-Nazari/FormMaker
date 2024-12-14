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
        public async Task<IActionResult> CreateFormQuestion([FromBody] FormQuestionDto formQuestionDto)
        {
            var response = await _formQuestionService.CreateFormQuestionAsync(formQuestionDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFormQuestion(int id, [FromBody] FormQuestionDto formQuestionDto)
        {
            var response = await _formQuestionService.UpdateFormQuestionAsync(id, formQuestionDto);
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
