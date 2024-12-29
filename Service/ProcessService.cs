﻿using FormMaker.Dto;
using FormMaker.Interface;
using FormMaker.Model;
using FormMaker.Data.Context;
using Microsoft.EntityFrameworkCore;
using FormMaker.Util;
using System.Diagnostics;
using System;

namespace FormMaker.Service
{
    public class ProcessService : IProcessService
    {
        private readonly FormMakerDbContext _context;

        public ProcessService(FormMakerDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<ProcessDto>> CreateProcessAsync(ProcessCreateDto processCreateUpdateDto)
        {
            var process = new Model.Process
            {
                ProcessTitle = processCreateUpdateDto.ProcessTitle,
                ProcessDescription = processCreateUpdateDto.ProcessDescription,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            _context.Processes.Add(process);
            await _context.SaveChangesAsync();

            var processDto = new ProcessDto
            {
                ProcessID = process.ProcessID,
                ProcessTitle = process.ProcessTitle,
                ProcessDescription = process.ProcessDescription,
                CreatedAtJalali = Jalali.ToJalali(process.CreatedAt),
                UpdatedAtJalali = Jalali.ToJalali(process.UpdatedAt),
                IsDeleted = process.IsDeleted
            };

            return new ApiResponse<ProcessDto>(true, ResponseMessage.ProcessCreated, processDto, 201);
        }

        public async Task<ApiResponse<ProcessDto>> GetProcessByIdAsync(int processId)
        {
            var process = await _context.Processes
                .FirstOrDefaultAsync(p => p.ProcessID == processId && !p.IsDeleted);

            if (process == null)
            {
                return new ApiResponse<ProcessDto>(false, ResponseMessage.ProcessNotFound, null, 404);
            }

            var processDto = new ProcessDto
            {
                ProcessID = process.ProcessID,
                ProcessTitle = process.ProcessTitle,
                ProcessDescription = process.ProcessDescription,
                CreatedAtJalali = Jalali.ToJalali(process.CreatedAt),
                UpdatedAtJalali = Jalali.ToJalali(process.UpdatedAt),
                IsDeleted = process.IsDeleted
            };

            return new ApiResponse<ProcessDto>(true, ResponseMessage.ProcessRetrieved, processDto, 200);
        }

        public async Task<ApiResponse<List<ProcessDto>>> GetAllProcessesAsync()
        {
            var processes = await _context.Processes
                .Where(p => !p.IsDeleted)
                .Select(p => new ProcessDto
                {
                    ProcessID = p.ProcessID,
                    ProcessTitle = p.ProcessTitle,
                    ProcessDescription = p.ProcessDescription,
                    CreatedAtJalali = Jalali.ToJalali(p.CreatedAt),
                    UpdatedAtJalali = Jalali.ToJalali(p.UpdatedAt),
                    IsDeleted = p.IsDeleted
                })
            .ToListAsync();

            return new ApiResponse<List<ProcessDto>>(true, ResponseMessage.ProcessRetrieved, processes, 200);
        }

        public async Task<ApiResponse<ProcessDto>> UpdateProcessAsync(ProcessUpdateDto processUpdateDto)
        {
            var process = await _context.Processes
                .FirstOrDefaultAsync(p => p.ProcessID == processUpdateDto.ProcessID && !p.IsDeleted);

            if (process == null)
            {
                return new ApiResponse<ProcessDto>(false, ResponseMessage.ProcessNotFound, null, 404);
            }

            process.ProcessTitle = processUpdateDto.ProcessTitle;
            process.ProcessDescription = processUpdateDto.ProcessDescription;
            process.UpdatedAt = DateTime.UtcNow;

            _context.Processes.Update(process);
            await _context.SaveChangesAsync();

            var processDto = new ProcessDto
            {
                ProcessID = process.ProcessID,
                ProcessTitle = process.ProcessTitle,
                ProcessDescription = process.ProcessDescription,
                CreatedAtJalali = Jalali.ToJalali(process.CreatedAt),
                UpdatedAtJalali = Jalali.ToJalali(process.UpdatedAt),
                IsDeleted = process.IsDeleted
            };

            return new ApiResponse<ProcessDto>(true, ResponseMessage.ProcessUpdated, processDto, 200);
        }

        public async Task<ApiResponse<bool>> DeleteProcessAsync(int processId)
        {
            var process = await _context.Processes
                .FirstOrDefaultAsync(p => p.ProcessID == processId);

            if (process == null || process.IsDeleted)
            {
                return new ApiResponse<bool>(false, ResponseMessage.ProcessDeleted, false, 404);
            }

            process.IsDeleted = true;
            _context.Processes.Update(process);
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>(true, ResponseMessage.ProcessDeleted, true, 200);
        }

        public async Task<ApiResponse<IEnumerable<FormDto>>> GetFormsByProcessIdAsync(int processId)
        {
            // Fetch forms related to the process by ProcessID
            var forms = await _context.FormProcesses
                .Where(fp => fp.ProcessID == processId && !fp.IsDeleted && !fp.Form.IsDeleted)
                .Select(fp => new FormDto
                {
                    FormID = fp.Form.FormID,
                    FormTitle = fp.Form.FormTitle,
                    FormDescription = fp.Form.FormDescription,
                    IsFrequent = fp.Form.IsFrequent,
                    CreatedAtJalali = Jalali.ToJalali(fp.Form.CreatedAt),
                    UpdatedAtJalali = Jalali.ToJalali(fp.Form.UpdatedAt),
                    IsDeleted = fp.Form.IsDeleted
                })
                .ToListAsync();

            if (!forms.Any())
            {
                return new ApiResponse<IEnumerable<FormDto>>(false, ResponseMessage.NoFormsFoundForProcess, null, 404);
            }

            return new ApiResponse<IEnumerable<FormDto>>(true, ResponseMessage.FormRetrieved, forms, 200);
        }
    }
}
