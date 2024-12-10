using FormMaker.Dto;
using FormMaker.Model;

namespace FormMaker.Interface
{
    public interface IQuestionService
    {
        Task<ApiResponse<QuestionDTO>> GetQuestionByIdAsync(int questionId);
        Task<ApiResponse<IEnumerable<QuestionDTO>>> GetAllQuestionsAsync();
        Task<ApiResponse<QuestionDTO>> CreateQuestionAsync(QuestionDTO questionDto);
        Task<ApiResponse<QuestionDTO>> UpdateQuestionAsync(int questionId, QuestionDTO questionDto);
        Task<ApiResponse<bool>> DeleteQuestionAsync(int questionId);
    }
}
