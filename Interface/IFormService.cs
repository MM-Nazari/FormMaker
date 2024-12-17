using FormMaker.Dto;
using FormMaker.Model;

namespace FormMaker.Interface
{
    public interface IFormService
    {
        Task<ApiResponse<IEnumerable<FormDto>>> GetAllFormsAsync();
        Task<ApiResponse<FormDto>> GetFormByIdAsync(int formId);
        Task<ApiResponse<FormDto>> CreateFormAsync(FormCreateDto formCreateDto);
        Task<ApiResponse<FormDto>> UpdateFormAsync(FormUpdateDto formUpdateDto);
        Task<ApiResponse<bool>> DeleteFormAsync(int formId);
        Task<ApiResponse<IEnumerable<FormDto>>> GetFrequentFormsAsync();
    }
}
