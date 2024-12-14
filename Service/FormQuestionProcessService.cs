using FormMaker.Data.Context;
using FormMaker.Dto;
using FormMaker.Interface;
using FormMaker.Model;
using FormMaker.Util;
using Microsoft.EntityFrameworkCore;

namespace FormMaker.Service
{
    public class FormQuestionProcessService : IFormQuestionProcessService
    {
        private readonly FormMakerDbContext _context;

        public FormQuestionProcessService(FormMakerDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<List<FormQuestionProcessDto>>> GetAllFormQuestionProcessesAsync()
        {
            var formQuestionProcesses = await _context.FormQuestionProcesses
                .Where(fqp => !fqp.IsDeleted)
                .Select(fqp => new FormQuestionProcessDto
                {
                    FormQuestionProcessID = fqp.FormQuestionProcessID,
                    FormQuestionID = fqp.FormQuestionID,
                    FormProcessID = fqp.FormProcessID
                })
                .ToListAsync();

            return new ApiResponse<List<FormQuestionProcessDto>>(true, ResponseMessage.FormQuestionProcessRetrieved, formQuestionProcesses, 200);
        }

        public async Task<ApiResponse<FormQuestionProcessDto>> GetFormQuestionProcessAsync(int formQuestionProcessId)
        {
            var formQuestionProcess = await _context.FormQuestionProcesses
                .Where(fqp => fqp.FormQuestionProcessID == formQuestionProcessId && !fqp.IsDeleted)
                .Select(fqp => new FormQuestionProcessDto
                {
                    FormQuestionProcessID = fqp.FormQuestionProcessID,
                    FormQuestionID = fqp.FormQuestionID,
                    FormProcessID = fqp.FormProcessID
                })
                .FirstOrDefaultAsync();

            if (formQuestionProcess == null)
            {
                return new ApiResponse<FormQuestionProcessDto>(false, ResponseMessage.FormQuestionProcessNotFound, null, 404);
            }

            return new ApiResponse<FormQuestionProcessDto>(true, ResponseMessage.FormQuestionProcessRetrieved, formQuestionProcess, 200);
        }

        public async Task<ApiResponse<FormQuestionProcessDto>> CreateFormQuestionProcessAsync(FormQuestionProcessDto formQuestionProcessDto)
        {
            var formQuestionProcess = new FormQuestionProcess
            {
                FormQuestionID = formQuestionProcessDto.FormQuestionID,
                FormProcessID = formQuestionProcessDto.FormProcessID,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.FormQuestionProcesses.Add(formQuestionProcess);
            await _context.SaveChangesAsync();

            formQuestionProcessDto.FormQuestionProcessID = formQuestionProcess.FormQuestionProcessID;

            return new ApiResponse<FormQuestionProcessDto>(true, ResponseMessage.FormQuestionProcessCreated, formQuestionProcessDto, 201);
        }

        public async Task<ApiResponse<FormQuestionProcessDto>> UpdateFormQuestionProcessAsync(int formQuestionProcessId, FormQuestionProcessDto formQuestionProcessDto)
        {
            var formQuestionProcess = await _context.FormQuestionProcesses
                .FirstOrDefaultAsync(fqp => fqp.FormQuestionProcessID == formQuestionProcessId && !fqp.IsDeleted);

            if (formQuestionProcess == null)
            {
                return new ApiResponse<FormQuestionProcessDto>(false, ResponseMessage.FormQuestionProcessNotFound, null, 404);
            }

            formQuestionProcess.FormQuestionID = formQuestionProcessDto.FormQuestionID;
            formQuestionProcess.FormProcessID = formQuestionProcessDto.FormProcessID;
            formQuestionProcess.UpdatedAt = DateTime.UtcNow;

            _context.FormQuestionProcesses.Update(formQuestionProcess);
            await _context.SaveChangesAsync();

            return new ApiResponse<FormQuestionProcessDto>(true, ResponseMessage.FormQuestionProcessUpdated, formQuestionProcessDto, 200);
        }

        public async Task<ApiResponse<bool>> DeleteFormQuestionProcessAsync(int formQuestionProcessId)
        {
            var formQuestionProcess = await _context.FormQuestionProcesses
                .FirstOrDefaultAsync(fqp => fqp.FormQuestionProcessID == formQuestionProcessId && !fqp.IsDeleted);

            if (formQuestionProcess == null)
            {
                return new ApiResponse<bool>(false, ResponseMessage.FormQuestionProcessNotFound, false, 404);
            }

            formQuestionProcess.IsDeleted = true;
            formQuestionProcess.UpdatedAt = DateTime.UtcNow;

            _context.FormQuestionProcesses.Update(formQuestionProcess);
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>(true, ResponseMessage.FormQuestionProcessDeleted, true, 200);
        }
    }
}
