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
                    FormID = a.FormQuestionProcess.FormProcess.FormID,
                    ProcessID = a.FormQuestionProcess.FormProcess.ProcessID,
                    QuestionID = a.FormQuestionProcess.FormQuestion.QuestionID,
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
                    FormID = a.FormQuestionProcess.FormProcess.FormID,
                    ProcessID = a.FormQuestionProcess.FormProcess.ProcessID,
                    QuestionID = a.FormQuestionProcess.FormQuestion.QuestionID,
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

            var formQuestion = await _context.FormQuestions
                .Where(fq => fq.QuestionID == answerCreateDto.QuestionID)
                .FirstOrDefaultAsync();

            if (formQuestion == null)
            {
                return new ApiResponse<AnswerDto>(false, ResponseMessage.FormQuestionNotFound, null, 404);
            }

            var formProcess = await _context.FormProcesses
                .Where(fp => fp.FormID == answerCreateDto.FormID)
                .FirstOrDefaultAsync();

            if (formProcess == null)
            {
                return new ApiResponse<AnswerDto>(false, ResponseMessage.FormProcessNotFound, null, 404);
            }

            var formQuestionProcess = await _context.FormQuestionProcesses
                .Where(fqp => fqp.FormQuestionID == formQuestion.FormQuestionID &&
                              fqp.FormProcessID == formProcess.FormProcessID &&
                              !fqp.IsDeleted)
                .FirstOrDefaultAsync();

            if (formQuestionProcess == null)
            {
                return new ApiResponse<AnswerDto>(false, ResponseMessage.FormQuestionProcessNotFound, null, 404);
            }

            var answer = new Answer
            {
                FormQuestionProcessID = formQuestionProcess.FormQuestionProcessID,
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
                FormID = formQuestion.FormID,
                ProcessID = formProcess.ProcessID,
                QuestionID = formQuestion.QuestionID,
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
            var answer = await _context.Answers
                .FirstOrDefaultAsync(a => a.AnswerID == answerUpdateDto.AnswerID && !a.IsDeleted);

            if (answer == null)
                return new ApiResponse<AnswerDto>(false, ResponseMessage.AnswerNotFound, null, 404);

            var formQuestionProcess = await _context.FormQuestionProcesses
                .Where(fqp => fqp.FormQuestionProcessID == answer.FormQuestionProcessID && !fqp.IsDeleted)
                .FirstOrDefaultAsync();

            if (formQuestionProcess == null)
                return new ApiResponse<AnswerDto>(false, ResponseMessage.FormQuestionProcessNotFound, null, 404);

            var formProcess = await _context.FormProcesses
                .Where(fp => fp.FormProcessID == formQuestionProcess.FormProcessID)
                .FirstOrDefaultAsync();

            if (formProcess == null)
            {
                return new ApiResponse<AnswerDto>(false, ResponseMessage.FormProcessNotFound, null, 404);
            }

            var formQuestion = await _context.FormQuestions
                .Where(fq => fq.FormQuestionID == formQuestionProcess.FormQuestionID)
                .FirstOrDefaultAsync();

            if (formQuestion == null)
            {
                return new ApiResponse<AnswerDto>(false, ResponseMessage.FormQuestionNotFound, null, 404);
            }


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
                FormID = formQuestion.FormID,
                ProcessID = formProcess.ProcessID,
                QuestionID = formQuestion.QuestionID,
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

            var answer = await _context.Answers
                .FirstOrDefaultAsync(a => a.AnswerID == id && !a.IsDeleted);

            if (answer == null)
                return new ApiResponse<bool>(false, ResponseMessage.AnswerNotFound, false, 404);

            answer.IsDeleted = true;
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>(true, ResponseMessage.AnswerDeleted, true, 200);
        }

    }
}
