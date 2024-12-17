using FormMaker.Util;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FormMaker.Dto
{
    public class AnswerOptionDto
    {
        public int OptionID { get; set; }
        public int QuestionID { get; set; }
        public string OptionText { get; set; }
        public int Priority { get; set; }

        public string CreatedAtJalali { get; set; }
        public string UpdatedAtJalali { get; set; }

        public bool IsDeleted { get; set; } = false;

    }
}
