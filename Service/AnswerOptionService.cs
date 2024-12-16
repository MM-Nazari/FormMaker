using FormMaker.Data.Context;
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
                    UpdatedAtJalali = Jalali.ToJalali(ao.UpdatedAt)
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
                    UpdatedAtJalali = Jalali.ToJalali(ao.UpdatedAt)
                })
                .FirstOrDefaultAsync();

            if (answerOption == null)
                return new ApiResponse<AnswerOptionDto>(false, ResponseMessage.AnswerOptionNotFound, null, 404);

            return new ApiResponse<AnswerOptionDto>(true, ResponseMessage.AnswerOptionRetrieved, answerOption, 200);
        }

        public async Task<ApiResponse<AnswerOptionDto>> CreateAnswerOptionAsync(AnswerOptionCreateDto answerOptionCreateDto)
        {
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

            //answerOptionDto.OptionID = answerOption.OptionID;
            //answerOptionDto.CreatedAtJalali = Jalali.ToJalali(answerOption.CreatedAt);
            //answerOptionDto.UpdatedAtJalali = Jalali.ToJalali(answerOption.UpdatedAt);

            return new ApiResponse<AnswerOptionDto>(true, ResponseMessage.AnswerOptionCreated, null, 201);
        }

        public async Task<ApiResponse<AnswerOptionDto>> UpdateAnswerOptionAsync(int id, AnswerOptionUpdateDto answerOptionUpdateDto)
        {
            var answerOption = await _context.AnswerOptions.FirstOrDefaultAsync(ao => ao.OptionID == id && !ao.IsDeleted);

            if (answerOption == null)
                return new ApiResponse<AnswerOptionDto>(false, ResponseMessage.AnswerOptionNotFound, null, 404);


            answerOption.OptionText = answerOptionUpdateDto.OptionText;
            answerOption.Priority = answerOptionUpdateDto.Priority;
            answerOption.UpdatedAt = DateTime.UtcNow;

            _context.AnswerOptions.Update(answerOption);
            await _context.SaveChangesAsync();

            return new ApiResponse<AnswerOptionDto>(true, ResponseMessage.AnswerOptionUpdated, null, 200);
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
    }
}
