﻿using FormMaker.Data.Context;
using FormMaker.Dto;
using FormMaker.Interface;
using FormMaker.Model;
using FormMaker.Util;
using Microsoft.EntityFrameworkCore;

namespace FormMaker.Service
{
    public class AnswerOptionService : IAnswerOptionService
    {
        private readonly FormMakerDbContext _context;

        public AnswerOptionService(FormMakerDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<IEnumerable<AnswerOptionDto>>> GetAllAnswerOptionsAsync()
        {
            var answerOptions = await _context.AnswerOptions
                .Where(ao => !ao.IsDeleted)
                .Select(ao => new AnswerOptionDto
                {
                    OptionID = ao.OptionID,
                    QuestionID = ao.QuestionID,
                    OptionText = ao.OptionText,
                    Priority = ao.Priority,
                    CreatedAtJalali = Jalali.ToJalali(ao.CreatedAt),
                    UpdatedAtJalali = Jalali.ToJalali(ao.UpdatedAt),
                    IsDeleted = ao.IsDeleted
                })
                .ToListAsync();

            return new ApiResponse<IEnumerable<AnswerOptionDto>>(true, ResponseMessage.AnswerOptionRetrieved, answerOptions, 200);
        }

        public async Task<ApiResponse<AnswerOptionDto>> GetAnswerOptionByIdAsync(int id)
        {
            var answerOption = await _context.AnswerOptions
                .Where(ao => ao.OptionID == id && !ao.IsDeleted)
                .Select(ao => new AnswerOptionDto
                {
                    OptionID = ao.OptionID,
                    QuestionID = ao.QuestionID,
                    OptionText = ao.OptionText,
                    Priority = ao.Priority,
                    CreatedAtJalali = Jalali.ToJalali(ao.CreatedAt),
                    UpdatedAtJalali = Jalali.ToJalali(ao.UpdatedAt),
                    IsDeleted = ao.IsDeleted
                })
                .FirstOrDefaultAsync();

            if (answerOption == null)
                return new ApiResponse<AnswerOptionDto>(false, ResponseMessage.AnswerOptionNotFound, null, 404);

            return new ApiResponse<AnswerOptionDto>(true, ResponseMessage.AnswerOptionRetrieved, answerOption, 200);
        }

        public async Task<ApiResponse<AnswerOptionDto>> CreateAnswerOptionAsync(AnswerOptionCreateDto answerOptionCreateDto)
        {
            // Check if the priority already exists for the given QuestionID
            var isDuplicatePriority = await _context.AnswerOptions
                .AnyAsync(ao => ao.QuestionID == answerOptionCreateDto.QuestionID
                             && ao.Priority == answerOptionCreateDto.Priority
                             && !ao.IsDeleted);

            if (isDuplicatePriority)
            {
                return new ApiResponse<AnswerOptionDto>(false,
                    ResponseMessage.PriorityIsDuplicate,
                    null,
                    400);
            }

            var answerOption = new AnswerOption
            {
                QuestionID = answerOptionCreateDto.QuestionID,
                OptionText = answerOptionCreateDto.OptionText,
                Priority = answerOptionCreateDto.Priority,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            _context.AnswerOptions.Add(answerOption);
            await _context.SaveChangesAsync();

            var answerOptionDto = new AnswerOptionDto
            {
                OptionID = answerOption.OptionID,
                QuestionID = answerOption.QuestionID,
                OptionText = answerOption.OptionText,
                Priority = answerOption.Priority,
                CreatedAtJalali = Jalali.ToJalali(answerOption.CreatedAt),
                UpdatedAtJalali = Jalali.ToJalali(answerOption.UpdatedAt),
                IsDeleted = answerOption.IsDeleted
            };

            return new ApiResponse<AnswerOptionDto>(true, ResponseMessage.AnswerOptionCreated, answerOptionDto, 201);
        }

        public async Task<ApiResponse<AnswerOptionDto>> UpdateAnswerOptionAsync(AnswerOptionUpdateDto answerOptionUpdateDto)
        {
            var answerOption = await _context.AnswerOptions.FirstOrDefaultAsync(ao => ao.OptionID == answerOptionUpdateDto.OptionID && !ao.IsDeleted);

            if (answerOption == null)
                return new ApiResponse<AnswerOptionDto>(false, ResponseMessage.AnswerOptionNotFound, null, 404);


            answerOption.OptionText = answerOptionUpdateDto.OptionText;
            answerOption.Priority = answerOptionUpdateDto.Priority;
            answerOption.UpdatedAt = DateTime.UtcNow;

            _context.AnswerOptions.Update(answerOption);
            await _context.SaveChangesAsync();

            var answerOptionDto = new AnswerOptionDto
            {
                OptionID = answerOption.OptionID,
                QuestionID = answerOption.QuestionID,
                OptionText = answerOption.OptionText,
                Priority = answerOption.Priority,
                CreatedAtJalali = Jalali.ToJalali(answerOption.CreatedAt),
                UpdatedAtJalali = Jalali.ToJalali(answerOption.UpdatedAt),
                IsDeleted = answerOption.IsDeleted
            };

            return new ApiResponse<AnswerOptionDto>(true, ResponseMessage.AnswerOptionUpdated, answerOptionDto, 200);
        }

        public async Task<ApiResponse<bool>> DeleteAnswerOptionAsync(int id)
        {
            var answerOption = await _context.AnswerOptions.FirstOrDefaultAsync(ao => ao.OptionID == id && !ao.IsDeleted);

            if (answerOption == null)
                return new ApiResponse<bool>(false, ResponseMessage.AnswerOptionNotFound, false, 404);

            answerOption.IsDeleted = true;
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>(true, ResponseMessage.AnswerOptionDeleted, true, 200);
        }

        public async Task<ApiResponse<IEnumerable<AnswerOptionDto>>> GetAnswerOptionsByQuestionIdAsync(int questionId)
        {
            var answerOptions = await _context.AnswerOptions
                .Where(ao => ao.QuestionID == questionId && !ao.IsDeleted)
                .Select(ao => new AnswerOptionDto
                {
                    OptionID = ao.OptionID,
                    QuestionID = ao.QuestionID,
                    OptionText = ao.OptionText,
                    Priority = ao.Priority,
                    CreatedAtJalali = Jalali.ToJalali(ao.CreatedAt),
                    UpdatedAtJalali = Jalali.ToJalali(ao.UpdatedAt),
                    IsDeleted = ao.IsDeleted
                })
                .ToListAsync();

            if (!answerOptions.Any())
            {
                return new ApiResponse<IEnumerable<AnswerOptionDto>>(false, ResponseMessage.AnswerOptionNotFound, null, 404);
            }

            return new ApiResponse<IEnumerable<AnswerOptionDto>>(true, ResponseMessage.AnswerOptionRetrieved, answerOptions, 200);
        }
    }
}
