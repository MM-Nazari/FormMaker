using FormMaker.Dto;
using FormMaker.Interface;
using FormMaker.Model;
using FormMaker.Util;
using FormMaker.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace FormMaker.Service
{
    public class FormProcessService : IFormProcessService
    {
        private readonly FormMakerDbContext _context;

        public FormProcessService(FormMakerDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<IEnumerable<FormProcessDTO>>> GetAllFormProcessesAsync()
        {
            var formProcesses = await _context.FormProcesses
                .Where(fp => !fp.IsDeleted)
                .Select(fp => new FormProcessDTO
                {
                    FormProcessID = fp.FormProcessID,
                    FormID = fp.FormID,
                    ProcessID = fp.ProcessID,
                    Stage = fp.Stage,
                    CreatedAt = fp.CreatedAt,
                    UpdatedAt = fp.UpdatedAt
                })
                .ToListAsync();

            return new ApiResponse<IEnumerable<FormProcessDTO>>(true, ResponseMessage.FormProcessRetrieved, formProcesses, 200);
        }

        public async Task<ApiResponse<FormProcessDTO>> GetFormProcessByIdAsync(int formProcessId)
        {
            var formProcess = await _context.FormProcesses
                .Where(fp => fp.FormProcessID == formProcessId && !fp.IsDeleted)
                .Select(fp => new FormProcessDTO
                {
                    FormProcessID = fp.FormProcessID,
                    FormID = fp.FormID,
                    ProcessID = fp.ProcessID,
                    Stage = fp.Stage,
                    CreatedAt = fp.CreatedAt,
                    UpdatedAt = fp.UpdatedAt
                })
                .FirstOrDefaultAsync();

            if (formProcess == null)
            {
                return new ApiResponse<FormProcessDTO>(false, ResponseMessage.FormProcessNotFound, null, 404);
            }

            return new ApiResponse<FormProcessDTO>(true, ResponseMessage.FormProcessRetrieved, formProcess, 200);
        }

        public async Task<ApiResponse<FormProcessDTO>> CreateFormProcessAsync(FormProcessDTO formProcessDto)
        {
            var formProcess = new FormProcess
            {
                FormID = formProcessDto.FormID,
                ProcessID = formProcessDto.ProcessID,
                Stage = formProcessDto.Stage,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            _context.FormProcesses.Add(formProcess);
            await _context.SaveChangesAsync();

            formProcessDto.FormProcessID = formProcess.FormProcessID;
            return new ApiResponse<FormProcessDTO>(true, ResponseMessage.FormProcessCreated, formProcessDto, 201);
        }

        public async Task<ApiResponse<FormProcessDTO>> UpdateFormProcessAsync(int formProcessId, FormProcessDTO formProcessDto)
        {
            var formProcess = await _context.FormProcesses
                .FirstOrDefaultAsync(fp => fp.FormProcessID == formProcessId && !fp.IsDeleted);

            if (formProcess == null)
            {
                return new ApiResponse<FormProcessDTO>(false, ResponseMessage.FormProcessNotFound, null, 404);
            }

            formProcess.FormID = formProcessDto.FormID;
            formProcess.ProcessID = formProcessDto.ProcessID;
            formProcess.Stage = formProcessDto.Stage;
            formProcess.UpdatedAt = DateTime.UtcNow;

            _context.FormProcesses.Update(formProcess);
            await _context.SaveChangesAsync();

            return new ApiResponse<FormProcessDTO>(true, ResponseMessage.FormProcessUpdated, formProcessDto, 200);
        }

        public async Task<ApiResponse<bool>> DeleteFormProcessAsync(int formProcessId)
        {
            var formProcess = await _context.FormProcesses
                .FirstOrDefaultAsync(fp => fp.FormProcessID == formProcessId && !fp.IsDeleted);

            if (formProcess == null)
            {
                return new ApiResponse<bool>(false, ResponseMessage.FormProcessNotFound, false, 404);
            }

            formProcess.IsDeleted = true;
            formProcess.UpdatedAt = DateTime.UtcNow;

            _context.FormProcesses.Update(formProcess);
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>(true, ResponseMessage.FormProcessDeleted, true, 200);
        }
    }
}
