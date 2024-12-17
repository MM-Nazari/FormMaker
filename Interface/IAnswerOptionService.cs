using FormMaker.Dto;
using FormMaker.Model;

namespace FormMaker.Interface
{
    public interface IAnswerOptionService
    {
        Task<ApiResponse<IEnumerable<AnswerOptionDto>>> GetAllAnswerOptionsAsync();
        Task<ApiResponse<AnswerOptionDto>> GetAnswerOptionByIdAsync(int id);
        Task<ApiResponse<AnswerOptionDto>> CreateAnswerOptionAsync(AnswerOptionCreateDto answerOptionCreateDto);
        Task<ApiResponse<AnswerOptionDto>> UpdateAnswerOptionAsync(AnswerOptionUpdateDto answerOptionUpdateDto);
        Task<ApiResponse<bool>> DeleteAnswerOptionAsync(int id);
        Task<ApiResponse<IEnumerable<AnswerOptionDto>>> GetAnswerOptionsByQuestionIdAsync(int questionId);
    }
}
