using FormMaker.Dto;
using FormMaker.Model;

namespace FormMaker.Interface
{
    public interface IFormQuestionService
    {
        Task<ApiResponse<IEnumerable<FormQuestionDto>>> GetAllFormQuestionsAsync();
        Task<ApiResponse<FormQuestionDto>> GetFormQuestionByIdAsync(int formQuestionId);
        Task<ApiResponse<FormQuestionDto>> CreateFormQuestionAsync(FormQuestionCreateDto formQuestionCreateDto);
        Task<ApiResponse<FormQuestionDto>> UpdateFormQuestionAsync(FormQuestionUpdateDto formQuestionUpdateDto);
        Task<ApiResponse<bool>> DeleteFormQuestionAsync(int formQuestionId);
    }
}
