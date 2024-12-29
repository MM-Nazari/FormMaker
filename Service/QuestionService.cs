using FormMaker.Data.Context;
using FormMaker.Dto;
using FormMaker.Interface;
using FormMaker.Model;
using FormMaker.Util;
using Microsoft.EntityFrameworkCore;

namespace FormMaker.Service
{
    public class QuestionService : IQuestionService
    {
        private readonly FormMakerDbContext _context;

        public QuestionService(FormMakerDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<QuestionDto>> GetQuestionByIdAsync(int questionId)
        {
            var question = await _context.Questions
                .Where(q => q.QuestionID == questionId)
                .Include(q => q.FormQuestions)
                .ThenInclude(fq => fq.FormQuestionProcesses)
                .ThenInclude(fqp => fqp.FormProcess)
                .Select(q => new QuestionDto
                {
                    FormIDs = q.FormQuestions
                        .Where(fq => !fq.IsDeleted)
                        .Select(fq => fq.FormID)
                        .Distinct()
                        .ToList(),
                    ProcessIDs = q.FormQuestions
                        .Where(fq => !fq.IsDeleted)
                        .SelectMany(fq => fq.FormQuestionProcesses
                            .Where(fqp => !fqp.IsDeleted)
                            .Select(fqp => fqp.FormProcess.ProcessID))
                        .Distinct()
                        .ToList(),
                    QuestionID = q.QuestionID,
                    QuestionTitle = q.QuestionTitle,
                    QuestionType = q.QuestionType,
                    ValidationRule = q.ValidationRule,
                    IsFrequent = q.IsFrequent,
                    CreatedAtJalali = Jalali.ToJalali(q.CreatedAt),
                    UpdatedAtJalali = Jalali.ToJalali(q.UpdatedAt),
                    IsDeleted = q.IsDeleted
                })
                .FirstOrDefaultAsync();

            if (question == null)
            {
                return new ApiResponse<QuestionDto>(false, ResponseMessage.QuestionNotFound, null, 404);
            }

            return new ApiResponse<QuestionDto>(true, ResponseMessage.QuestionRetrieved, question, 200);
        }

        public async Task<ApiResponse<IEnumerable<QuestionDto>>> GetAllQuestionsAsync()
        {
            var questions = await _context.Questions
                .Include(q => q.FormQuestions)
                .ThenInclude(fq => fq.FormQuestionProcesses)
                .ThenInclude(fqp => fqp.FormProcess)
                .Select(q => new QuestionDto
                {
                    FormIDs = q.FormQuestions
                        .Where(fq => !fq.IsDeleted)
                        .Select(fq => fq.FormID)
                        .Distinct()
                        .ToList(),
                    ProcessIDs = q.FormQuestions
                        .Where(fq => !fq.IsDeleted)
                        .SelectMany(fq => fq.FormQuestionProcesses
                            .Where(fqp => !fqp.IsDeleted)
                            .Select(fqp => fqp.FormProcess.ProcessID))
                        .Distinct()
                        .ToList(),
                    QuestionID = q.QuestionID,
                    QuestionTitle = q.QuestionTitle,
                    QuestionType = q.QuestionType,
                    ValidationRule = q.ValidationRule,
                    IsFrequent = q.IsFrequent,
                    CreatedAtJalali = Jalali.ToJalali(q.CreatedAt),
                    UpdatedAtJalali = Jalali.ToJalali(q.UpdatedAt),
                    IsDeleted = q.IsDeleted
                })
                .ToListAsync();

            return new ApiResponse<IEnumerable<QuestionDto>>(true, ResponseMessage.QuestionRetrieved, questions, 200);
        }

        public async Task<ApiResponse<QuestionDto>> CreateQuestionAsync(QuestionCreateDto questionCreateDto)
        {
            var question = new Question
            {
                QuestionTitle = questionCreateDto.QuestionTitle,
                QuestionType = questionCreateDto.QuestionType,
                ValidationRule = questionCreateDto.ValidationRule,
                IsFrequent = questionCreateDto.IsFrequent,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            _context.Questions.Add(question);
            await _context.SaveChangesAsync();

            var questionDto = new QuestionDto
            {
                QuestionID = question.QuestionID,
                QuestionTitle = question.QuestionTitle,
                QuestionType = question.QuestionType,
                ValidationRule = question.ValidationRule,
                IsFrequent = question.IsFrequent,
                CreatedAtJalali = Jalali.ToJalali(question.CreatedAt),
                UpdatedAtJalali = Jalali.ToJalali(question.UpdatedAt),
                IsDeleted = question.IsDeleted
            };

            return new ApiResponse<QuestionDto>(true, ResponseMessage.QuestionCreated, questionDto, 201);
        }

        public async Task<ApiResponse<QuestionDto>> UpdateQuestionAsync(QuestionUpdateDto questionUpdateDto)
        {
            var question = await _context.Questions
                .Include(q => q.FormQuestions)
                .ThenInclude(fq => fq.FormQuestionProcesses)
                .ThenInclude(fqp => fqp.FormProcess)
                .FirstOrDefaultAsync(q => q.QuestionID == questionUpdateDto.QuestionID); ;

            if (question == null)
            {
                return new ApiResponse<QuestionDto>(false, ResponseMessage.QuestionNotFound, null, 404);
            }

            question.QuestionTitle = questionUpdateDto.QuestionTitle;
            question.QuestionType = questionUpdateDto.QuestionType;
            question.ValidationRule = questionUpdateDto.ValidationRule;
            question.IsFrequent = questionUpdateDto.IsFrequent;
            question.UpdatedAt = DateTime.UtcNow;

            _context.Questions.Update(question);
            await _context.SaveChangesAsync();

            var questionDto = new QuestionDto
            {
                FormIDs = question.FormQuestions
                    .Where(fq => !fq.IsDeleted)
                    .Select(fq => fq.FormID)
                    .Distinct()
                    .ToList(),
                ProcessIDs = question.FormQuestions
                    .Where(fq => !fq.IsDeleted)
                    .SelectMany(fq => fq.FormQuestionProcesses
                        .Where(fqp => !fqp.IsDeleted)
                        .Select(fqp => fqp.FormProcess.ProcessID))
                    .Distinct()
                    .ToList(),
                QuestionID = question.QuestionID,
                QuestionTitle = question.QuestionTitle,
                QuestionType = question.QuestionType,
                ValidationRule = question.ValidationRule,
                IsFrequent = question.IsFrequent,
                CreatedAtJalali = Jalali.ToJalali(question.CreatedAt),
                UpdatedAtJalali = Jalali.ToJalali(question.UpdatedAt),
                IsDeleted = question.IsDeleted
            };

            return new ApiResponse<QuestionDto>(true, ResponseMessage.QuestionUpdated, questionDto, 200);
        }

        public async Task<ApiResponse<bool>> DeleteQuestionAsync(int questionId)
        {
            var question = await _context.Questions
                .FirstOrDefaultAsync(q => q.QuestionID == questionId);

            if (question == null)
            {
                return new ApiResponse<bool>(false, ResponseMessage.QuestionNotFound, false, 404);
            }

            question.IsDeleted = true;
            _context.Questions.Update(question);
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>(true, ResponseMessage.QuestionDeleted, true, 200);
        }

        public async Task<ApiResponse<IEnumerable<QuestionDto>>> GetFrequentQuestionsAsync()
        {
            var questions = await _context.Questions
                .Where(q => q.IsFrequent && !q.IsDeleted)
                .Include(q => q.FormQuestions)
                    .ThenInclude(fq => fq.FormQuestionProcesses)
                    .ThenInclude(fqp => fqp.FormProcess)
                .Select(q => new QuestionDto
                {
                    FormIDs = q.FormQuestions
                        .Where(fq => !fq.IsDeleted)
                        .Select(fq => fq.FormID)
                        .Distinct()
                        .ToList(),
                    ProcessIDs = q.FormQuestions
                        .Where(fq => !fq.IsDeleted)
                        .SelectMany(fq => fq.FormQuestionProcesses
                            .Where(fqp => !fqp.IsDeleted)
                            .Select(fqp => fqp.FormProcess.ProcessID))
                        .Distinct()
                        .ToList(),
                    QuestionID = q.QuestionID,
                    QuestionTitle = q.QuestionTitle,
                    ValidationRule = q.ValidationRule,
                    IsFrequent = q.IsFrequent,
                    CreatedAtJalali = Jalali.ToJalali(q.CreatedAt),
                    UpdatedAtJalali = Jalali.ToJalali(q.UpdatedAt),
                    IsDeleted = q.IsDeleted
                })
                .ToListAsync();

            return new ApiResponse<IEnumerable<QuestionDto>>(true, ResponseMessage.QuestionRetrieved, questions, 200);
        }

        public async Task<ApiResponse<FormQuestionProcessAllDto>> CreateQuestionAndLinkToFormAsync(CreateQuestionAndLinkToFormDto createDto)
        {

                // Step 1: Create the question
                var question = new Question
                {
                    QuestionTitle = createDto.QuestionTitle,
                    QuestionType = createDto.QuestionType,
                    ValidationRule = createDto.ValidationRule,
                    IsFrequent = createDto.IsFrequent,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    IsDeleted = false
                };

                _context.Questions.Add(question);
                await _context.SaveChangesAsync();

            var questionOrderExists = await _context.FormQuestions
                .AnyAsync(fq => fq.FormID == createDto.FormID && fq.QuestionOrder == createDto.QuestionOrder && !fq.IsDeleted);

            if (questionOrderExists)
            {
                return new ApiResponse<FormQuestionProcessAllDto>(
                    false,
                    ResponseMessage.QuestionOrderDuplicateForForm,
                    null,
                    StatusCodes.Status400BadRequest
                );
            }

            // Step 2: Link the question to the form 
            var formQuestion = new FormQuestion
                {
                    FormID = createDto.FormID,
                    QuestionID = question.QuestionID,
                    QuestionOrder = createDto.QuestionOrder,
                    IsRequired = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    IsDeleted = false
                };

                _context.FormQuestions.Add(formQuestion);
                await _context.SaveChangesAsync();

                // Retrieve the FormProcess based on FormID and ProcessID
                var formProcess = await _context.FormProcesses
                    .Where(fp => fp.FormID == createDto.FormID && fp.ProcessID == createDto.ProcessID)
                    .FirstOrDefaultAsync();

                if (formProcess == null)
                {
                    return new ApiResponse<FormQuestionProcessAllDto>(
                        false,
                        ResponseMessage.FormProcessNotFound,
                        null,
                        StatusCodes.Status404NotFound
                    );
                }

                // Step 3: Link the formQuestion to the process
                var formQuestionProcess = new FormQuestionProcess
                {
                    FormQuestionID = formQuestion.FormQuestionID,
                    FormProcessID = formProcess.FormProcessID,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    IsDeleted = false
                };

                _context.FormQuestionProcesses.Add(formQuestionProcess);
                await _context.SaveChangesAsync();

                // Step 4: Prepare the response DTO for FormQuestionProcess
                var formQuestionProcessAllDto = new FormQuestionProcessAllDto
                {
                    FormQuestionProcessID = formQuestionProcess.FormQuestionProcessID,
                    FormID = createDto.FormID,
                    ProcessID = createDto.ProcessID,
                    QuestionID = question.QuestionID,
                    QuestionTitle = question.QuestionTitle,
                    QuestionType = question.QuestionType,
                    CreatedAtJalali = Jalali.ToJalali(formQuestionProcess.CreatedAt),
                    UpdatedAtJalali = Jalali.ToJalali(formQuestionProcess.UpdatedAt),
                    IsDeleted = formQuestionProcess.IsDeleted
                };

                return new ApiResponse<FormQuestionProcessAllDto>(true, ResponseMessage.QuestionLinkedForn, formQuestionProcessAllDto, 201);


        }
    }
}
