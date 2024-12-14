using FormMaker.Dto;
using FormMaker.Model;

namespace FormMaker.Interface
{
    public interface IFormService
    {
        Task<ApiResponse<IEnumerable<FormDto>>> GetAllFormsAsync();
        Task<ApiResponse<FormDto>> GetFormByIdAsync(int formId);
        Task<ApiResponse<FormDto>> CreateFormAsync(FormDto formDto);
        Task<ApiResponse<FormDto>> UpdateFormAsync(int formId, FormDto formDto);
        Task<ApiResponse<bool>> DeleteFormAsync(int formId);
    }
}
