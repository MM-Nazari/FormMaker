using FormMaker.Dto;
using FormMaker.Model;

namespace FormMaker.Interface
{
    public interface IFormQuestionService
    {
        Task<ApiResponse<IEnumerable<FormQuestionDto>>> GetAllFormQuestionsAsync();
        Task<ApiResponse<FormQuestionDto>> GetFormQuestionByIdAsync(int formQuestionId);
        Task<ApiResponse<FormQuestionDto>> CreateFormQuestionAsync(FormQuestionDto formQuestionDto);
        Task<ApiResponse<FormQuestionDto>> UpdateFormQuestionAsync(int formQuestionId, FormQuestionDto formQuestionDto);
        Task<ApiResponse<bool>> DeleteFormQuestionAsync(int formQuestionId);
    }
}
