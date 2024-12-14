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
                    CaptchaAnswer = a.CaptchaAnswer
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
                    CaptchaAnswer = a.CaptchaAnswer
                })
                .FirstOrDefaultAsync();

            if (answer == null)
                return new ApiResponse<AnswerDto>(false, ResponseMessage.AnswerNotFound, null, 404);

            return new ApiResponse<AnswerDto>(true, ResponseMessage.AnswerRetrieved, answer, 200);
        }

        public async Task<ApiResponse<AnswerDto>> CreateAnswerAsync(AnswerDto answerDto)
        {
            var answer = new Answer
            {
                FormQuestionProcessID = answerDto.FormQuestionProcessID,
                AnswerText = answerDto.AnswerText,
                AnswerOptionID = answerDto.AnswerOptionID,
                FilePath = answerDto.FilePath,
                IsCaptchaSolved = answerDto.IsCaptchaSolved,
                CaptchaAnswer = answerDto.CaptchaAnswer,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            _context.Answers.Add(answer);
            await _context.SaveChangesAsync();

            answerDto.AnswerID = answer.AnswerID;

            return new ApiResponse<AnswerDto>(true, ResponseMessage.AnswerCreated, answerDto, 201);
        }

        public async Task<ApiResponse<AnswerDto>> UpdateAnswerAsync(int id, AnswerDto answerDto)
        {
            var answer = await _context.Answers.FirstOrDefaultAsync(a => a.AnswerID == id);

            if (answer == null)
                return new ApiResponse<AnswerDto>(false, ResponseMessage.AnswerNotFound, null, 404);

            answer.AnswerText = answerDto.AnswerText;
            answer.AnswerOptionID = answerDto.AnswerOptionID;
            answer.FilePath = answerDto.FilePath;
            answer.IsCaptchaSolved = answerDto.IsCaptchaSolved;
            answer.CaptchaAnswer = answerDto.CaptchaAnswer;
            answer.UpdatedAt = DateTime.UtcNow;

            _context.Answers.Update(answer);
            await _context.SaveChangesAsync();

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
