using FormMaker.Dto;
using FormMaker.Model;

namespace FormMaker.Interface
{
    public interface IProcessService
    {
        Task<ApiResponse<ProcessDto>> CreateProcessAsync(ProcessCreateUpdateDto processCreateDto);
        Task<ApiResponse<ProcessDto>> GetProcessByIdAsync(int processId);
        Task<ApiResponse<List<ProcessDto>>> GetAllProcessesAsync();
        Task<ApiResponse<ProcessDto>> UpdateProcessAsync(int processId, ProcessCreateUpdateDto processCreateUpdateDto);
        Task<ApiResponse<bool>> DeleteProcessAsync(int processId);
    }
}
