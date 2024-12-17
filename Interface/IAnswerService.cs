using FormMaker.Dto;
using FormMaker.Model;

namespace FormMaker.Interface
{
    public interface IAnswerService
    {
        Task<ApiResponse<IEnumerable<AnswerDto>>> GetAllAnswersAsync();
        Task<ApiResponse<AnswerDto>> GetAnswerByIdAsync(int id);
        Task<ApiResponse<AnswerDto>> CreateAnswerAsync(AnswerCreateDto answerCraeteDto);
        Task<ApiResponse<AnswerDto>> UpdateAnswerAsync(AnswerUpdateDto answerUpdateDto);
        Task<ApiResponse<bool>> DeleteAnswerAsync(int id);
    }
}
