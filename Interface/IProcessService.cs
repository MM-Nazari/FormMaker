using FormMaker.Dto;
using FormMaker.Model;

namespace FormMaker.Interface
{
    public interface IProcessService
    {
        Task<ApiResponse<ProcessDto>> CreateProcessAsync(ProcessCreateDto processCreateDto);
        Task<ApiResponse<ProcessDto>> GetProcessByIdAsync(int processId);
        Task<ApiResponse<List<ProcessDto>>> GetAllProcessesAsync();
        Task<ApiResponse<ProcessDto>> UpdateProcessAsync(ProcessUpdateDto processUpdateDto);
        Task<ApiResponse<bool>> DeleteProcessAsync(int processId);
        Task<ApiResponse<IEnumerable<FormDto>>> GetFormsByProcessIdAsync(int processId);
    }
}
