using FormMaker.Dto;
using FormMaker.Model;

namespace FormMaker.Interface
{
    public interface IQuestionService
    {
        Task<ApiResponse<QuestionDto>> GetQuestionByIdAsync(int questionId);
        Task<ApiResponse<IEnumerable<QuestionDto>>> GetAllQuestionsAsync();
        Task<ApiResponse<QuestionDto>> CreateQuestionAsync(QuestionCreateDto questionCreateDto);
        Task<ApiResponse<QuestionDto>> UpdateQuestionAsync(QuestionUpdateDto questionUpdateDto);
        Task<ApiResponse<bool>> DeleteQuestionAsync(int questionId);
    }
}
