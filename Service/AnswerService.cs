using FormMaker.Data.Context;
using FormMaker.Dto;
using FormMaker.Interface;
using FormMaker.Model;
using FormMaker.Util;
using Microsoft.EntityFrameworkCore;

namespace FormMaker.Service
{
    public class AnswerService : IAnswerService
    {
        private readonly FormMakerDbContext _context;

        public AnswerService(FormMakerDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<IEnumerable<AnswerDto>>> GetAllAnswersAsync()
        {
            var answers = await _context.Answers
                .Where(a => !a.IsDeleted)
                .Select(a => new AnswerDto
                {
                    AnswerID = a.AnswerID,
                    FormQuestionProcessID = a.FormQuestionProcessID,
                    AnswerText = a.AnswerText,
                    AnswerOptionID = a.AnswerOptionID,
                    FilePath = a.FilePath,
                    IsCaptchaSolved = a.IsCaptchaSolved,
                    CaptchaAnswer = a.CaptchaAnswer,
                    CreatedAtJalali = Jalali.ToJalali(a.CreatedAt),
                    UpdatedAtJalali = Jalali.ToJalali(a.UpdatedAt),
                    IsDeleted = a.IsDeleted
                })
                .ToListAsync();

            return new ApiResponse<IEnumerable<AnswerDto>>(true, ResponseMessage.AnswerRetrieved, answers, 200);
        }

        public async Task<ApiResponse<AnswerDto>> GetAnswerByIdAsync(int id)
        {
            var answer = await _context.Answers
                .Where(a => a.AnswerID == id && !a.IsDeleted)
                .Select(a => new AnswerDto
                {
                    AnswerID = a.AnswerID,
                    FormQuestionProcessID = a.FormQuestionProcessID,
                    AnswerText = a.AnswerText,
                    AnswerOptionID = a.AnswerOptionID,
                    FilePath = a.FilePath,
                    IsCaptchaSolved = a.IsCaptchaSolved,
                    CaptchaAnswer = a.CaptchaAnswer,
                    CreatedAtJalali = Jalali.ToJalali(a.CreatedAt),
                    UpdatedAtJalali = Jalali.ToJalali(a.UpdatedAt),
                    IsDeleted = a.IsDeleted
                })
                .FirstOrDefaultAsync();

            if (answer == null)
                return new ApiResponse<AnswerDto>(false, ResponseMessage.AnswerNotFound, null, 404);

            return new ApiResponse<AnswerDto>(true, ResponseMessage.AnswerRetrieved, answer, 200);
        }

        public async Task<ApiResponse<AnswerDto>> CreateAnswerAsync(AnswerCreateDto answerCreateDto)
        {
            var answer = new Answer
            {
                FormQuestionProcessID = answerCreateDto.FormQuestionProcessID,
                AnswerText = answerCreateDto.AnswerText,
                AnswerOptionID = answerCreateDto.AnswerOptionID,
                FilePath = answerCreateDto.FilePath,
                IsCaptchaSolved = answerCreateDto.IsCaptchaSolved,
                CaptchaAnswer = answerCreateDto.CaptchaAnswer,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            _context.Answers.Add(answer);
            await _context.SaveChangesAsync();

            var answerDto = new AnswerDto
            {
                AnswerID = answer.AnswerID,
                FormQuestionProcessID = answer.FormQuestionProcessID,
                AnswerText = answer.AnswerText,
                AnswerOptionID = answer.AnswerOptionID,
                FilePath = answer.FilePath,
                IsCaptchaSolved = answer.IsCaptchaSolved,
                CaptchaAnswer = answer.CaptchaAnswer,
                CreatedAtJalali = Jalali.ToJalali(answer.CreatedAt),
                UpdatedAtJalali = Jalali.ToJalali(answer.UpdatedAt),
                IsDeleted = answer.IsDeleted
            };

            return new ApiResponse<AnswerDto>(true, ResponseMessage.AnswerCreated, answerDto, 201);
        }

        public async Task<ApiResponse<AnswerDto>> UpdateAnswerAsync(AnswerUpdateDto answerUpdateDto)
        {
            var answer = await _context.Answers.FirstOrDefaultAsync(a => a.AnswerID == answerUpdateDto.AnswerID);

            if (answer == null)
                return new ApiResponse<AnswerDto>(false, ResponseMessage.AnswerNotFound, null, 404);

            answer.AnswerText = answerUpdateDto.AnswerText;
            answer.AnswerOptionID = answerUpdateDto.AnswerOptionID;
            answer.FilePath = answerUpdateDto.FilePath;
            answer.IsCaptchaSolved = answerUpdateDto.IsCaptchaSolved;
            answer.CaptchaAnswer = answerUpdateDto.CaptchaAnswer;
            answer.UpdatedAt = DateTime.UtcNow;

            _context.Answers.Update(answer);
            await _context.SaveChangesAsync();

            var answerDto = new AnswerDto
            {
                AnswerID = answer.AnswerID,
                FormQuestionProcessID = answer.FormQuestionProcessID,
                AnswerText = answer.AnswerText,
                AnswerOptionID = answer.AnswerOptionID,
                FilePath = answer.FilePath,
                IsCaptchaSolved = answer.IsCaptchaSolved,
                CaptchaAnswer = answer.CaptchaAnswer,
                CreatedAtJalali = Jalali.ToJalali(answer.CreatedAt),
                UpdatedAtJalali = Jalali.ToJalali(answer.UpdatedAt),
                IsDeleted = answer.IsDeleted
            };

            return new ApiResponse<AnswerDto>(true, ResponseMessage.AnswerUpdated, answerDto, 200);
        }

        public async Task<ApiResponse<bool>> DeleteAnswerAsync(int id)
        {
            var answer = await _context.Answers.FirstOrDefaultAsync(a => a.AnswerID == id);

            if (answer == null)
                return new ApiResponse<bool>(false, ResponseMessage.AnswerNotFound, false, 404);

            answer.IsDeleted = true;
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>(true, ResponseMessage.AnswerDeleted, true, 200);
        }
    }
}
